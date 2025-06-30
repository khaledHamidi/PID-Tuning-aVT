// AsyncLogger.cs
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Stabilization
{
    public class AsyncLogger : IDisposable
    {
        private readonly BlockingCollection<(double T, int V1, double V2, double V3)> _queue;
        private readonly StreamWriter _writer;
        private readonly Task _worker;
        private readonly Stopwatch _sw;
        private readonly string _filePath;

        public AsyncLogger(string dataName)
        {
            Directory.CreateDirectory(dataName);
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            _filePath = Path.Combine(dataName, $"{dataName}_{timestamp}.log");
            _writer = new StreamWriter(new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            { AutoFlush = true };

            _queue = new BlockingCollection<(double, int, double, double)>();
            _sw = Stopwatch.StartNew();

            _worker = Task.Run(() =>
            {
                foreach (var item in _queue.GetConsumingEnumerable())
                    _writer.WriteLine($"{item.T:F5};{item.V1};{item.V2:R};{item.V3:R}");
            });
        }

        // الآن نحسب الزمن داخلية لا يمرر من الخارج
        public void Record(int value1, double value2, double value3)
        {
            if (!_queue.IsAddingCompleted)
            {
                double t = _sw.Elapsed.TotalSeconds;
                _queue.Add((t, value1, value2, value3));
            }
        }

        public void Finish(bool isUsable)
        {
            _queue.CompleteAdding();
            _worker.Wait();
            _writer.Dispose();

            var suffix = isUsable ? "_usable" : "_unusable";
            var dir = Path.GetDirectoryName(_filePath);
            var name = Path.GetFileNameWithoutExtension(_filePath) + suffix + ".log";
            File.Move(_filePath, Path.Combine(dir, name));
        }

        public void Dispose()
        {
            if (!_queue.IsAddingCompleted) Finish(true);
        }
    }
}

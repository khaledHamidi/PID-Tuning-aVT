// AsyncLogger.cs
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Stabilization
{
    public class AsyncLogger : IDisposable
    {
        private readonly BlockingCollection<(long T, int V1, double V2, int V3)> _queue;
        private readonly StreamWriter _writer;
        private readonly Task _worker;
        private readonly Stopwatch _sw;
        private readonly string _filePath;

        public AsyncLogger(string dataName)
        {
            Directory.CreateDirectory(dataName);
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            _filePath = Path.Combine(dataName, $"{dataName}_{timestamp}.csv");
            _writer = new StreamWriter(new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            { AutoFlush = true };
            _queue = new BlockingCollection<(long, int, double, int)>();
            _sw = Stopwatch.StartNew();

            _worker = Task.Run(() =>
            {
                foreach (var item in _queue.GetConsumingEnumerable())
                    _writer.WriteLine($"{item.T};{item.V1};{item.V2.ToString("F2", CultureInfo.InvariantCulture)};{item.V3}");
            });
        }

        // الآن نحسب الزمن داخلية لا يمرر من الخارج
        public void Record(int setpoint, double angle, int output,long milis)
        {
            if (!_queue.IsAddingCompleted)
            {
                double milis2 = _sw.Elapsed.TotalSeconds;
                _queue.Add((milis, setpoint, angle, output));
            }
        }

        public void Finish(bool isUsable)
        {
            _queue.CompleteAdding();
            _worker.Wait();
            _writer.Dispose();

            var suffix = isUsable ? "_usable" : "_unusable";
            var dir = Path.GetDirectoryName(_filePath);
            var name = Path.GetFileNameWithoutExtension(_filePath) + suffix + ".csv";
            File.Move(_filePath, Path.Combine(dir, name));
        }

        public void Dispose()
        {
            if (!_queue.IsAddingCompleted) Finish(true);
        }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace Stabilization
{
    public partial class Plot : Form
    {
        public Plot()
        {
            InitializeComponent();
            // start postion 0,0
            this.StartPosition = FormStartPosition.Manual;
            // this size is the same to screen hieght and width/2
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height);
            // beng to front not topmost.
            this.Width = 635;
            this.Height = 768;
            this.Location = new Point(0, 0);


            // set the location to the right side of the screen
        }

        private void Plot_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Prevent the form from closing
        }
    }
}

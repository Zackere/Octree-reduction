using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Octree_gui {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            trackBar1_ValueChanged(null, null);

            mainDirectBitmap = new DirectBitmap(pictureBox1.Width - pictureBox1.Padding.Horizontal, pictureBox1.Height - pictureBox1.Padding.Vertical);
            pictureBox1.Image = mainDirectBitmap.Bitmap;
            reducedAfterDirectBitmap = new DirectBitmap(mainDirectBitmap.Width, mainDirectBitmap.Height);
            pictureBox2.Image = reducedAfterDirectBitmap.Bitmap;
            octreeReduceAfter = new Octree();
            reducedOnInsertBitmap = new DirectBitmap(mainDirectBitmap.Width, mainDirectBitmap.Height);
            pictureBox3.Image = reducedOnInsertBitmap.Bitmap;
            octreeReduceOnInsert = new Octree();
        }

        private Octree octreeReduceAfter, octreeReduceOnInsert;
        private DirectBitmap mainDirectBitmap;
        private DirectBitmap reducedAfterDirectBitmap, reducedOnInsertBitmap;
        private bool enableButtons = false;
        private BackgroundWorker t1, t2;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (!button1.Enabled) {
                MessageBox.Show("Work in progress. Please Wait.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK) {
                using var Image = new Bitmap(open.FileName);
                mainDirectBitmap.Dispose();
                mainDirectBitmap = new DirectBitmap(Image.Width, Image.Height);
                for (int i = 0; i < Image.Width; ++i)
                    for (int j = 0; j < Image.Height; ++j)
                        mainDirectBitmap.SetPixel(i, j, Image.GetPixel(i, j));
                pictureBox1.Image = mainDirectBitmap.Bitmap;

                reducedAfterDirectBitmap = new DirectBitmap(mainDirectBitmap.Width, mainDirectBitmap.Height);
                pictureBox2.Image = reducedAfterDirectBitmap.Bitmap;

                reducedOnInsertBitmap = new DirectBitmap(mainDirectBitmap.Width, mainDirectBitmap.Height);
                pictureBox3.Image = reducedOnInsertBitmap.Bitmap;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            octreeReduceAfter.Clear();
            octreeReduceOnInsert.Clear();
            uint max_colors = 7 + (uint)Math.Pow(1.5, trackBar1.Value);
            button1.Enabled = button2.Enabled = enableButtons = false;
            if (t1 != null)
                t1.Dispose();
            t1 = new BackgroundWorker();
            t1.DoWork += (_, __) => {
                for (int i = 0; i < mainDirectBitmap.Width; ++i)
                    for (int j = 0; j < mainDirectBitmap.Height; ++j)
                        octreeReduceAfter.InsertColor(mainDirectBitmap.GetPixel(i, j));
                octreeReduceAfter.Reduce(max_colors);
                for (int i = 0; i < mainDirectBitmap.Width; ++i)
                    for (int j = 0; j < mainDirectBitmap.Height; ++j)
                        reducedAfterDirectBitmap.SetPixel(i, j, Color.FromArgb((int)octreeReduceAfter.FromPallete(mainDirectBitmap.GetPixel(i, j))));
            };
            t1.RunWorkerCompleted += (_, __) => {
                Refresh();
                button1.Enabled = button2.Enabled = ControlBox = enableButtons;
                enableButtons = true;
            };
            t1.WorkerSupportsCancellation = true;
            if (t2 != null)
                t2.Dispose();
            t2 = new BackgroundWorker();
            t2.DoWork += (_, __) => {
                for (int i = 0; i < mainDirectBitmap.Width; ++i)
                    for (int j = 0; j < mainDirectBitmap.Height; ++j) {
                        octreeReduceOnInsert.InsertColor(mainDirectBitmap.GetPixel(i, j));
                        octreeReduceOnInsert.Reduce(max_colors);
                    }
                for (int i = 0; i < mainDirectBitmap.Width; ++i)
                    for (int j = 0; j < mainDirectBitmap.Height; ++j)
                        reducedOnInsertBitmap.SetPixel(i, j, Color.FromArgb((int)octreeReduceOnInsert.FromPallete(mainDirectBitmap.GetPixel(i, j))));
            };
            t2.RunWorkerCompleted += (_, __) => {
                Refresh();
                button1.Enabled = button2.Enabled = enableButtons;
                enableButtons = true;
            };
            t1.RunWorkerAsync();
            t2.RunWorkerAsync();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e) {
            StringBuilder sb = new StringBuilder();
            sb.Append("Reduce to ");
            sb.Append(7 + (uint)Math.Pow(1.5, trackBar1.Value));
            sb.Append(" color(s)");
            button1.Text = sb.ToString();
        }
    }
}

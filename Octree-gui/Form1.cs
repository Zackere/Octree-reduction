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

            StringBuilder sb = new StringBuilder();
            sb.Append("Reduce to ");
            sb.Append(Math.Pow(2, trackBar1.Value));
            sb.Append(" color(s)");
            button1.Text = sb.ToString();

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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            octreeReduceAfter.Clear();
            octreeReduceOnInsert.Clear();
            uint max_colors = (uint)Math.Pow(2, trackBar1.Value);
            Thread t1 = new Thread(() => {
                for (int i = 0; i < mainDirectBitmap.Width; ++i)
                    for (int j = 0; j < mainDirectBitmap.Height; ++j)
                        octreeReduceAfter.InsertColor(mainDirectBitmap.GetPixel(i, j));
                octreeReduceAfter.Reduce(max_colors);
                for (int i = 0; i < mainDirectBitmap.Width; ++i)
                    for (int j = 0; j < mainDirectBitmap.Height; ++j)
                        reducedAfterDirectBitmap.SetPixel(i, j, Color.FromArgb((int)octreeReduceAfter.FromPallete(mainDirectBitmap.GetPixel(i, j))));
            });
            Thread t2 = new Thread(() => {
                for (int i = 0; i < mainDirectBitmap.Width; ++i)
                    for (int j = 0; j < mainDirectBitmap.Height; ++j) {
                        octreeReduceOnInsert.InsertColor(mainDirectBitmap.GetPixel(i, j));
                        octreeReduceOnInsert.Reduce(max_colors);
                    }
                for (int i = 0; i < mainDirectBitmap.Width; ++i)
                    for (int j = 0; j < mainDirectBitmap.Height; ++j)
                        reducedOnInsertBitmap.SetPixel(i, j, Color.FromArgb((int)octreeReduceOnInsert.FromPallete(mainDirectBitmap.GetPixel(i, j))));
            });
            t1.Start();
            t2.Start();
            t2.Join();
            Refresh();
            t1.Join();
            Refresh();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e) {
            StringBuilder sb = new StringBuilder();
            sb.Append("Reduce to ");
            sb.Append(Math.Pow(2, trackBar1.Value));
            sb.Append(" color(s)");
            button1.Text = sb.ToString();
        }
    }
}

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
            reducedDirectBitmap = new DirectBitmap(mainDirectBitmap.Width, mainDirectBitmap.Height);
            pictureBox2.Image = reducedDirectBitmap.Bitmap;
            octree = new Octree();
        }

        private Octree octree;
        private DirectBitmap mainDirectBitmap;
        private DirectBitmap reducedDirectBitmap;

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

                reducedDirectBitmap = new DirectBitmap(mainDirectBitmap.Width, mainDirectBitmap.Height);
                pictureBox2.Image = reducedDirectBitmap.Bitmap;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            octree.Clear();
            for (int i = 0; i < mainDirectBitmap.Width; ++i)
                for (int j = 0; j < mainDirectBitmap.Height; ++j)
                    octree.InsertColor(mainDirectBitmap.GetPixel(i, j));
            octree.Reduce((uint)Math.Pow(2, trackBar1.Value));
            for (int i = 0; i < mainDirectBitmap.Width; ++i)
                for (int j = 0; j < mainDirectBitmap.Height; ++j)
                    reducedDirectBitmap.SetPixel(i, j, Color.FromArgb((int)octree.FromPallete(mainDirectBitmap.GetPixel(i, j))));
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

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Octree_gui {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            octree = new Octree();
            octree.InsertColor(Color.Red);
        }

        private Octree octree;
    }
}

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Octree_gui {
    public class Octree {
        public Octree() {
            OctreePtr = CreateOctree();
        }
        ~Octree() {
            DestroyOctree(OctreePtr);
        }

        public void InsertColor(Color color) {
            InsertColor(OctreePtr, color.ToArgb());
        }

        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateOctree();
        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DestroyOctree(IntPtr octree);
        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void InsertColor(IntPtr octree, int color);

        private IntPtr OctreePtr;
    }
}

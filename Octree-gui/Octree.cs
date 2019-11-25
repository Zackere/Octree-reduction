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
            InsertColor(OctreePtr, (uint)color.ToArgb());
        }
        public void Reduce(uint max_colors) {
            Reduce(OctreePtr, max_colors);
        }
        public uint FromPallete(Color color) {
            return FromPallete(OctreePtr, (uint)color.ToArgb());
        }

        public void Clear() {
            Clear(OctreePtr);
        }

        public void SetOptimizationLevel(uint level) {
            SetOptimizationLevel(OctreePtr, level);
        }

        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateOctree();
        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DestroyOctree(IntPtr octree);
        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void InsertColor(IntPtr octree, uint color);
        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Reduce(IntPtr octree, uint max_colors);
        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint FromPallete(IntPtr octree, uint color);
        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint Clear(IntPtr octree);
        [DllImport("Octree.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint SetOptimizationLevel(IntPtr octree, uint level);

        private IntPtr OctreePtr;
    }
}

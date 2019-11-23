#pragma once

#include <cstdint>

#define OCTREE_API __declspec(dllexport)

namespace gk3 {
class Octree;
}

extern "C" {
OCTREE_API gk3::Octree* __stdcall CreateOctree();
OCTREE_API void __stdcall DestroyOctree(gk3::Octree* octree);
OCTREE_API void __stdcall InsertColor(gk3::Octree* octree, uint32_t color);
}
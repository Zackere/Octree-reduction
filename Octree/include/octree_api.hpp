#pragma once

#include <cstdint>

#define OCTREE_API __declspec(dllexport)

namespace gk3 {
class Octree;
}

extern "C" {
OCTREE_API gk3::Octree* CreateOctree();
OCTREE_API void DestroyOctree(gk3::Octree* octree);
OCTREE_API void InsertColor(gk3::Octree* octree, uint32_t color);
}
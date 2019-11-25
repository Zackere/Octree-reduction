#pragma once

#include <cstdint>

#define OCTREE_API __declspec(dllexport)

namespace gk3 {
class Octree;
}

extern "C" {
OCTREE_API gk3::Octree* __cdecl CreateOctree();
OCTREE_API void __cdecl DestroyOctree(gk3::Octree* octree);
OCTREE_API void __cdecl InsertColor(gk3::Octree* octree, uint32_t color);
OCTREE_API uint64_t __cdecl Reduce(gk3::Octree* octree, unsigned max_colors);
OCTREE_API uint32_t __cdecl FromPallete(gk3::Octree* octree,
                                        unsigned max_colors);
OCTREE_API void __cdecl Clear(gk3::Octree* octree);
}

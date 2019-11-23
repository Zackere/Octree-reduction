#include "../include/octree_api.hpp"

#include "../include/octree.hpp"

gk3::Octree* __cdecl CreateOctree() {
  return new gk3::Octree;
}

void __cdecl DestroyOctree(gk3::Octree* octree) {
  delete octree;
}

void __cdecl InsertColor(gk3::Octree* octree, uint32_t color) {
  octree->InsertColor(color);
}

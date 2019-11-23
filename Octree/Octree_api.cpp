#include "Octree_api.hpp"

#include "./Octree.hpp"

gk3::Octree* CreateOctree() {
  return new gk3::Octree();
}

void DestroyOctree(gk3::Octree* octree) {
  delete octree;
}

void InsertColor(gk3::Octree* octree, uint32_t color) {

}

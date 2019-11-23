#include "../include/octree.hpp"

namespace gk3 {
namespace {
extern "C" uint8_t __cdecl GetIndex(uint8_t iteration, uint32_t color);
}  // namespace
Octree::Octree() : root_(new OctreeNode) {}

void Octree::InsertColor(uint32_t color /*in ARGB format*/) {
  uint8_t index = 0;
  auto* cur_node = root_.get();
  for (int8_t i = 7; i >= 0; --i) {
    index = GetIndex(i, color);
    if (!cur_node->children[index])
      cur_node->children[index] = std::make_unique<OctreeNode>();
    cur_node = cur_node->children[index].get();
  }
  ++cur_node->refs;
  cur_node->b += color & 0xFF;
  color >>= 8;
  cur_node->g += color & 0xFF;
  color >>= 8;
  cur_node->r += color & 0xFF;
}
}  // namespace gk3
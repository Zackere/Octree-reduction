#include "../include/octree.hpp"

namespace gk3 {
namespace {
extern "C" uint8_t __cdecl GetIndex(uint8_t iteration, uint32_t color);
}  // namespace
Octree::Octree() : root_(new OctreeNode) {
  nodes_on_level_[0].insert(root_.get());
}

void Octree::InsertColor(uint32_t color /*in ARGB format*/) {
  uint8_t index = 0;
  auto* cur_node = root_.get();
  for (uint8_t i = 1; i < 8; ++i) {
    index = GetIndex(8 - i, color);
    if (!cur_node->children[index]) {
      nodes_on_level_[i].insert(
          (cur_node->children[index] = std::make_unique<OctreeNode>()).get());
      cur_node->children[index]->level = i;
    }
    cur_node = cur_node->children[index].get();
  }
  last_nonempty_set_ = 7;
  index = GetIndex(0, color);
  if (!cur_node->children[index]) {
    cur_node->children[index] = std::make_unique<OctreeNode>();
    cur_node->children[index]->level = 8;
    ++number_of_leaves_;
  }
  cur_node = cur_node->children[index].get();

  ++cur_node->refs;
  cur_node->b += color & 0xFF;
  color >>= 8;
  cur_node->g += color & 0xFF;
  color >>= 8;
  cur_node->r += color & 0xFF;
}

void Octree::Reduce(unsigned max_colors) {
  if (max_colors == 0)
    return;
  while (number_of_leaves_ > max_colors) {
    auto* min_refs = *nodes_on_level_[last_nonempty_set_].begin();
    for (auto* node : nodes_on_level_[last_nonempty_set_])
      if (min_refs->refs > node->refs)
        min_refs = node;
    number_of_leaves_ -= min_refs->Reduce();
    nodes_on_level_[last_nonempty_set_].erase(min_refs);
    if (nodes_on_level_[last_nonempty_set_].empty())
      --last_nonempty_set_;
  }
}

int Octree::OctreeNode::Reduce() {
  int ret = 0;
  for (auto& child : children) {
    if (child.get()) {
      refs += child->refs;
      r += child->r;
      g += child->g;
      b += child->b;
      child.reset();
      ++ret;
    }
  }
  return ret - 1;
}
}  // namespace gk3
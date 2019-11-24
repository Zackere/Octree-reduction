#pragma once

#include <cstdint>
#include <memory>
#include <set>

namespace gk3 {
class Octree {
 public:
  Octree();
  void InsertColor(uint32_t color);
  void Reduce(unsigned max_colors);
  uint32_t FromPallete(uint32_t color);
  void Clear();

 private:
  static constexpr unsigned kMaxDepth = 6;
  struct OctreeNode {
    uint64_t refs = 0, level = 0;
    uint64_t r = 0, g = 0, b = 0;
    std::unique_ptr<OctreeNode> children[8] = {nullptr};
    int Reduce();
    uint64_t ChildrenRefSum();
  };
  std::unique_ptr<OctreeNode> root_ = nullptr;
  std::set<OctreeNode*> nodes_on_level_[kMaxDepth];
  uint8_t last_nonempty_set_ = 0;
  uint64_t number_of_leaves_ = 0;
};
}  // namespace gk3

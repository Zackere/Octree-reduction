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

 private:
  struct OctreeNode {
    uint64_t refs = 0, level = 0;
    uint64_t r = 0, g = 0, b = 0;
    std::unique_ptr<OctreeNode> children[8] = {nullptr};
    int Reduce();
  };
  std::unique_ptr<OctreeNode> root_ = nullptr;
  std::set<OctreeNode*> nodes_on_level_[8];
  uint8_t last_nonempty_set_ = 0;
  uint64_t number_of_leaves_ = 0;
};
}  // namespace gk3

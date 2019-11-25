#pragma once

#include <cstdint>
#include <iostream>
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
  // kMaxDepth <= kNumBitsPerByte
  static constexpr unsigned kMaxDepth = 7;
  static constexpr unsigned kNumBitsPerByte = 8;
  struct OctreeNode {
    uint64_t refs = 0, level = 0;
    uint64_t r = 0, g = 0, b = 0;
    std::unique_ptr<OctreeNode> children[kNumBitsPerByte] = {nullptr};
    int Reduce();
    uint64_t children_ref_sum;
    uint64_t ChildrenRefSumRecursive();
  };

  void CalculateChildrenRefSums();

  std::unique_ptr<OctreeNode> root_ = nullptr;
  std::set<OctreeNode*> nodes_on_level_[kMaxDepth];
  uint8_t last_nonempty_set_ = 0;
  uint64_t number_of_leaves_ = 0;
};
}  // namespace gk3

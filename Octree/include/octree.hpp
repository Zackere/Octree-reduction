#pragma once

#include <cstdint>
#include <memory>

namespace gk3 {
class Octree {
 public:
  Octree();
  void InsertColor(uint32_t color);

 private:
  struct OctreeNode {
    uint64_t refs = 0;
    uint64_t r = 0, g = 0, b = 0;
    std::unique_ptr<OctreeNode> children[8] = {nullptr};
  };
  std::unique_ptr<OctreeNode> root_ = nullptr;
};
}  // namespace gk3

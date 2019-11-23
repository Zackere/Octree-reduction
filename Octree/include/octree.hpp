#pragma once

#include <cstdint>

namespace gk3 {
class Octree {
 public:
  Octree();
  void InsertColor(uint32_t color);

 private:
  class OctreeNode {};
};
}  // namespace gk3

#include "../include/octree.hpp"

namespace gk3 {
namespace {
extern "C" uint8_t GetNthBit(int number, int n);
}  // namespace
Octree::Octree() {}

void Octree::InsertColor(uint32_t color) {
  auto ret = GetNthBit(2, 0);
  ret = GetNthBit(2, 1);
  ret = GetNthBit(2, 2);
}
}  // namespace gk3
#include "../include/octree.hpp"

namespace gk3 {
namespace {
extern "C" uint8_t __cdecl GetNthBit(uint32_t number, uint8_t n);
}  // namespace
Octree::Octree() {}

void Octree::InsertColor(uint32_t color) {
  auto ret = GetNthBit(2, 0);
  ret = GetNthBit(2, 1);
  ret = GetNthBit(2, 2);
}
}  // namespace gk3
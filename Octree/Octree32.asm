.model flat
.code

; uint8_t __cdecl GetNthBit(uint32_t number, uint8_t n);
_GetNthBit proc
    mov eax, [esp + 4]
    mov cl, [esp + 8]
    shr eax, cl
    and eax, 1
    ret
_GetNthBit endp

end
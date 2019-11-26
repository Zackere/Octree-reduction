.model flat
.code

; uint8_t __cdecl GetIndex(uint8_t iteration, uint32_t number)
_GetIndex proc
    mov ecx, [esp + 4]
    mov edx, [esp + 8]
    add ecx, 16
    xor eax, eax
    bt edx, ecx
    adc eax, eax
    sub ecx, 8
    bt edx, ecx
    adc eax, eax
    sub ecx, 8
    bt edx, ecx
    adc eax, eax
    ret
_GetIndex endp

end
.code

; uint8_t __cdecl GetNthBit(uint32_t number, uint8_t n);
;                                    RCX             RDX
GetNthBit proc
    mov eax, ecx
    mov cl, dl
    shr eax, cl
    and eax, 1
    ret
GetNthBit endp

end
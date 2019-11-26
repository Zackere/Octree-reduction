.code

; uint8_t __cdecl GetIndex(uint8_t iteration, uint32_t color);
;                                  RCX                 RDX
GetIndex proc
    add rcx, 16
    xor rax, rax
    bt rdx, rcx
    adc rax, rax
    sub rcx, 8
    bt rdx, rcx
    adc rax, rax
    sub rcx, 8
    bt rdx, rcx
    adc rax, rax
    ret
GetIndex endp

end
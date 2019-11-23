.code

; uint8_t __cdecl GetNthBit(uint8_t n, uint32_t number)
;                                   RCX         RDX
GetNthBit proc
    mov rax, rdx
    shr rax, cl
    and rax, 1
    ret
GetNthBit endp

; uint8_t __cdecl GetIndex(uint8_t iteration, uint32_t color);
;                                  RCX                 RDX
GetIndex proc
    add rcx, 16
    xor r8, r8
    call GetNthBit
    or r8, rax
    shl r8, 1
    sub rcx, 8
    call GetNthBit
    or r8, rax
    shl r8, 1
    sub rcx, 8
    call GetNthBit
    or rax, r8
    ret
GetIndex endp

end
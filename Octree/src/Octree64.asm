.code

; uint8_t __cdecl GetIndex(uint8_t iteration, uint32_t color);
;                                  RCX                 RDX
GetIndex proc
    add rcx, 16
    xor r8, r8
    mov rax, rdx
    shr rax, cl
    and rax, 1
    or r8, rax
    shl r8, 1
    sub rcx, 8
    mov rax, rdx
    shr rax, cl
    and rax, 1
    or r8, rax
    shl r8, 1
    sub rcx, 8
    mov rax, rdx
    shr rax, cl
    and rax, 1
    or rax, r8
    ret
GetIndex endp

end
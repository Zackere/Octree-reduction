.code

; uint8_t GetNthBit(int number, int n)
;                   RCX,        RDX
GetNthBit proc
    mov rax, rcx
    mov cl, dl
    shr rax, cl
    and rax, 1
    ret
GetNthBit endp

end
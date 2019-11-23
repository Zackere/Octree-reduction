.model flat
.code

; uint8_t GetNthBit(int number, int n)
_GetNthBit proc
    push ebp
    mov ebp, esp
    mov eax, [ebp + 8]
    mov cl, [ebp + 16]
    shr eax, cl
    and eax, 1
    pop ebp
    ret
_GetNthBit endp

end
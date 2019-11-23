.model flat
.code

; uint8_t __cdecl GetNthBit(uint8_t n, uint32_t number)
;                                   ECX         EDX
_GetNthBit proc
    mov eax, edx
    shr eax, cl
    and eax, 1
    ret
_GetNthBit endp

; uint8_t __cdecl GetIndex(uint8_t iteration, uint32_t number)
_GetIndex proc
    mov ecx, [esp + 4]
    mov edx, [esp + 8]
    add ecx, 16
    push ebx
    xor ebx, ebx
    call _GetNthBit
    or ebx, eax
    shl ebx, 1
    sub ecx, 8
    call _GetNthBit
    or ebx, eax
    shl ebx, 1
    sub ecx, 8
    call _GetNthBit
    or eax, ebx
    pop ebx
    ret
_GetIndex endp

end
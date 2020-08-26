#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
#Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; Remaping
; CapsLock::Ctrl

; VIM Keybinding for windows
$^+h::
    Send, {Left down}{Left up}
    Return

$^j::
    Send, {Down down}{Down up}
    Return

$^k::
    Send, {Up down}{Up up}
    Return

$^+l::
    Send, {Right down}{Right up}
    Return
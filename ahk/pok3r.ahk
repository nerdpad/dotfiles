;created by Nibiria for the /r/MechanicalKeyboards subreddit
;thanks to /u/radiantcabbage for the many many corrections along the way
; http://www.vortexgear.tw/db/upload/webdata4/6vortex_20166523361966663.pdf

#NoEnv
#Warn
SendMode Input
SetWorkingDir %A_ScriptDir%

;define boolean for arrows
arrowbind := false
 
;Media Controls
CapsLock & w::
AppsKey & w::Send {Media_Play_Pause}
CapsLock & q::
AppsKey & q::Send {Media_Prev}
CapsLock & e::
AppsKey & e::Send {Media_Next}
CapsLock & s::
AppsKey & s::Send {Volume_Down}
CapsLock & d::
AppsKey & d::Send {Volume_Up}
CapsLock & f::
AppsKey & f::Send {Volume_Mute}


;Fn General keys
CapsLock & 1::
AppsKey & 1::Send {F1}
CapsLock & 2::
AppsKey & 2::Send {F2}
CapsLock & 3::
AppsKey & 3::Send {F3}
CapsLock & 4::
AppsKey & 4::Send {F4}
CapsLock & 5::
AppsKey & 5::Send {F5}
CapsLock & 6::
AppsKey & 6::Send {F6}
CapsLock & 7::
AppsKey & 7::Send {F7}
CapsLock & 8::
AppsKey & 8::Send {F8}
CapsLock & 9::
AppsKey & 9::Send {F9}
CapsLock & 0::
AppsKey & 0::Send {F10}
CapsLock & -::
AppsKey & -::Send {F11}
CapsLock & =::
AppsKey & =::Send {F12}

;Directional Keys
CapsLock & i::
AppsKey & i::Send {up}
CapsLock & j::
AppsKey & j::Send {left}
CapsLock & k::
AppsKey & k::Send {down}
CapsLock & l::
AppsKey & l::Send {right}

;Navigation Cluster
CapsLock & h::
AppsKey & h:: Send {Home}
CapsLock & n::
AppsKey & n:: Send {End}
CapsLock & u::
AppsKey & u:: Send {PgUp}
CapsLock & o::
AppsKey & o:: Send {PgDn}
CapsLock & BS::
AppsKey & BS:: Send {Del}
CapsLock & '::
AppsKey & '::Send {Del}
CapsLock & `;::
AppsKey & `;::Send {Ins}


;PrintScreen etc.
CapsLock & p::
AppsKey & p::Send {PrintScreen}
CapsLock & [::
AppsKey & [::Send {ScrollLock}
CapsLock & ]::
AppsKey & ]::Send {Pause}
CapsLock & z::
AppsKey & z:: Send {AppsKey}

;Disabling keys: TKL set
Up::return
Down::return
Left::return
Right::return

Del::return
Ins::return
Home::return
End::return
PgUp::return
PgDn::return

ScrollLock::return
PrintScreen::return
Pause::return

F1::return
F2::return
F3::return
F4::return
F5::return
F6::return
F7::return
F8::return
F9::return
F10::return
F11::return
F12::return

;Disabling keys: Numpad
Numpad0::return
Numpad1::return
Numpad2::return
Numpad3::return
Numpad4::return
Numpad5::return
Numpad6::return
Numpad7::return
Numpad8::return
Numpad9::return
NumpadDot::return

NumLock::return
NumpadDiv::return
NumpadMult::return
NumpadAdd::return
NumpadSub::return
NumpadEnter::return





;Arrows From Hell
<#>!space::arrowbind := !arrowbind
 
#if arrowbind
    RShift::Send {up}
    Rwin::Send {left}
    AppsKey::Send {down}
    RCtrl::Send {right}
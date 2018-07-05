#singleinstance force

#persistent
#InstallKeybdHook
#InstallMouseHook

message=
(
The window behing this message is the Key History Window

type a few keys '123'
               and then hit F5

Do you see them in the key history?

press the mouse button
               then hit F5 again

do you see what button was pushed?

The hotkey F1 will copy the test to the clipboard

)

KeyHistory ; Display the history info in a window.

msgbox %message%

#KeyHistory 100

;KeyHistory ; Display the history info in a window.


esc::exitapp   ; press escape key to exit script

f1::
WinGetText, clipboard, ahk_class AutoHotkey
return

/*

>>>>>>>>>>( Window Title & Class )<<<<<<<<<<<
C:\Users\Lee\Documents\Autohotkey\key_history.ahk - AutoHotkey v1.0.48.05
ahk_class AutoHotkey

>>>>>>>>>>>>( Mouse Position )<<<<<<<<<<<<<
On Screen:	484, 334  (less often used)
In Active Window:	493, 343

>>>>>>>>>( Now Under Mouse Cursor )<<<<<<<<
ClassNN:	Edit1
Text:	
*/
Set WshShell = CreateObject("WScript.Shell" ) 
  WshShell.Run """C:\code\dotfiles\ahk\AutoHotkeyScript.exe""", 0 'Must quote command if it has spaces; must escape quotes
  Set WshShell = Nothing
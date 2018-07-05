Set WshShell = CreateObject("WScript.Shell" ) 
  WshShell.Run """C:\Users\zzakaria\AppData\Local\Mizage LLC\Divvy\Divvy.exe""", 0 'Must quote command if it has spaces; must escape quotes
  Set WshShell = Nothing
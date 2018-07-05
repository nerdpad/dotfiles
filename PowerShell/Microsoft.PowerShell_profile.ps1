# General Configurations

# Turn of bell
Set-PSReadlineOption -BellStyle None

# SSH
Start-SshAgent >$null
Add-SshKey (Resolve-Path ~/.ssh/id_rsa) >$null

# alias
. $PSScriptRoot\ps\aliases.ps1

# color directory listing
Set-Alias ls Get-ChildItemColor -option AllScope -Force
Set-Alias dir Get-ChildItemColor -option AllScope -Force


# prompt
# . $PSScriptRoot\ps\prompt.ps1

# theme

# change the prompt foreground color
# $GitPromptSettings.DefaultForegroundColor = "DarkCyan"
# make the prompt span two lines and change the prompt character to `$`
$GitPromptSettings.DefaultPromptSuffix = '`n$(''$'' * ($nestedPromptLevel + 1)) '
# prefix the prompt with "username@hostname"
$GitPromptSettings.DefaultPromptPrefix = '$env:USERNAME@$(hostname) '


# Import Modules
Import-Module Get-ChildItemColor
Import-Module posh-git
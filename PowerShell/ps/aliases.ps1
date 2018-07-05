# git alias
Import-Module git-aliases -DisableNameChecking

New-Alias cl clear

# goto the code directory
function Goto-Code() {
  cd c:\code
}

function Goto-Canonical() {
  cd c:\code\airw\canonical
}

function Goto-AWCode() {
  cd c:\code\airw
}

New-Alias c Goto-Code
New-Alias aw Goto-AWCode
New-Alias canonical Goto-Canonical

# docker
function Delete-DanglingDockerImages() {
  docker rmi $(docker images -f “dangling=true” -q)
}

function Delete-AllDockerContainers() {
  docker stop $(docker ps -a -q)
  docker rm $(docker ps -a -q)
}

function Delete-AllDockerImages() {
  docker rmi -f $(docker images -q)
}

function Docker-CleanUp() {
  Delete-AllDockerContainers
  Delete-AllDockerImages
}

New-Alias ddi Delete-DanglingDockerImages
New-Alias dcr Delete-AllDockerContainers
New-Alias drmi Delete-AllDockerImages
New-Alias dcu Docker-CleanUp
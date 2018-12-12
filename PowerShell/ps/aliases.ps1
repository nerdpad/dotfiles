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

# Astro Air
function BuildAstroAir-Watch() {
	npm run build:dev -- --watch -op "C:\\code\\airw\\canonical\\Console\\Web\\src\\WebSln\\WanderingWiFi.AirWatch.Console.Web\\Scripts\\AstroAir\\dist"
}

function BuildAstroAir() {
	npm run build:dev -- -op "C:\\code\\airw\\canonical\\Console\\Web\\src\\WebSln\\WanderingWiFi.AirWatch.Console.Web\\Scripts\\AstroAir\\dist"
}

New-Alias baaw BuildAstroAir-Watch
New-Alias baa BuildAstroAir

# Git
function Assume-UnChanged([string]$file) {
	git update-index --assume-unchanged $file
}

function NoAssume-UnChanged([string]$file) {
	git update-index --no-assume-unchanged $file
}

function ListAssume-UnChanged() {
	git ls-files -v | Select-String -Pattern "^H\^"
}


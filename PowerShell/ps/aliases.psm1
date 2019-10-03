# git alias
Import-Module git-aliases -DisableNameChecking

function todo() {
	todolist $args
}

function t() {
	todolist $args
}

function ta() {
	todolist a $args
}

function tl() {
	todolist l $args
}

function tlp() {
	todolist l by p
}

function tlc() {
	todolist l by c
}

function tld() {
	todolist l due $args
}

function tc() {
	todolist complete $args
}

function tuc() {
	todolist uncomplete $args
}

function td() {
	todolist archive $args
}

function tud() {
	todolist unarchive $args
}

function tweb() {
	todolist web
}

function cl() {
	clear
}

function c() {
  cd c:\code
}

function aw() {
  cd c:\code\airw
}

function canonical() {
  cd c:\code\airw\canonical
}

# bash simulation
function which() {
	Get-Command $args
}

function mklink() {
	cmd /c mklink $args
}

# docker
function drmi() {
	docker rmi $args
}

function drmid() {
  docker rmi $(docker images -f “dangling=true” -q)
}

function drma() {
  docker stop $(docker ps -a -q)
  docker rm $(docker ps -a -q)
}

function drmiaf() {
  docker rmi -f $(docker images -q)
}

function dcleanup() {
  drma
  drmiaf
}

# Astro Air
function baaw() {
	npm run build:dev -- --watch --output-path="C:\\code\\airw\\canonical\\Console\\Web\\src\\WebSln\\WanderingWiFi.AirWatch.Console.Web\\Scripts\\AstroAir\\dist"
}

function baa() {
	npm run build:dev -- --output-path="C:\\code\\airw\\canonical\\Console\\Web\\src\\WebSln\\WanderingWiFi.AirWatch.Console.Web\\Scripts\\AstroAir\\dist"
}

# Git
function gs {
	git status $args
}

# Basleine
function pbs() {
	Invoke-WebRequest 'https://zzakaria-w03.vmware.com/BaselineService/ping'
}

# Export all aliases
$FunctionsToExport = @(
	# misc
	'todo',
	't',
	'tl',
	'ta',
	'tlp',
	'tlc',
	'tld',
	'tc',
	'tuc',
	'td',
	'tud',
	'tweb',
	'cl',
	'c',
	'which',
	'mklink',
	'aw',
	'canonical',
	'baa',
	'baaw',
	
	# git
	'gs',
	
	# local
	'pbs',
	
	# docker
	'drmi',
	'drmid',
	'drma',
	'drmiaf',
	'dcleanup'
)

Export-ModuleMember -Function $FunctionsToExport
# git aliases
alias gs='git s'
alias glog="git l"
alias gdaf='git apply --ignore-space-change --ignore-whitespace'

function g() {
    if [[ $# > 0 ]]; then
        # if there are arguments, send them to git
        git $@
    else
        # otherwise, run git status
        git s
    fi
}

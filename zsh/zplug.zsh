#!/bin/sh

# only source zplug on initial load
if [ -z ${RELOAD} ]; then
    if ! [ type "zplug" > /dev/null 2>&1 ]; then
        source ~/.zplug/init.zsh
    fi

    zplug 'zplug/zplug', hook-build:'zplug --self-manage'
    zplug 'zsh-users/zsh-syntax-highlighting', defer:2
    zplug 'zsh-users/zsh-autosuggestions'
    zplug 'akoenig/npm-run.plugin.zsh'
    zplug 'yonchu/grunt-zsh-completion'
    zplug 'plugins/yarn', from:oh-my-zsh, as:plugin
    zplug 'plugins/docker', from:oh-my-zsh, as:plugin
    zplug 'plugins/ssh-agent', from:oh-my-zsh, ignore:oh-my-zsh.sh
    zplug 'djui/alias-tips'

    # export NVM_LAZY_LOAD=true
    export NVM_AUTO_USE=false
    zplug 'lukechilds/zsh-nvm'

    # git
    zplug 'lib/git', from:oh-my-zsh
    zplug 'plugins/git', from:oh-my-zsh

    # Install plugins if there are plugins that have not been installed
    if ! zplug check; then
        printf "Install? [y/N]: "
        if read -q; then
            echo; zplug install
        fi
    fi

    zplug load
fi


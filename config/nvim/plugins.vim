" check whether vim-plug is installed and install it if necessary
let plugpath = expand('<sfile>:p:h'). '/autoload/plug.vim'
if !filereadable(plugpath)
    if executable('curl')
        let plugurl = 'https://raw.githubusercontent.com/junegunn/vim-plug/master/plug.vim'
        call system('curl -fLo ' . shellescape(plugpath) . ' --create-dirs ' . plugurl)
        if v:shell_error
            echom "Error downloading vim-plug. Please install it manually.\n"
            exit
        endif
    else
        echom "vim-plug not installed. Please install it manually or install curl.\n"
        exit
    endif
endif

call plug#begin('~/.config/nvim/plugged')

" colorschemes
Plug 'nerdpad/dracula-vim'
" Plug 'joshdick/onedark.vim'
Plug 'chriskempson/base16-vim'
Plug 'w0ng/vim-hybrid'
Plug 'rakr/vim-one'

" utilities
Plug '/usr/local/opt/fzf' | Plug 'junegunn/fzf.vim' " fuzzy file finder and so much more
Plug 'mileszs/ack.vim' " search inside files using ack. Same as command line ack utility, but use :Ack
Plug 'scrooloose/nerdtree', { 'on': ['NERDTreeToggle', 'NERDTreeFind', 'NERDTreeTabsToggle', 'NERDTreeTabsFind', 'NERDTreeClose'] } | Plug 'Xuyuanp/nerdtree-git-plugin' | Plug 'jistr/vim-nerdtree-tabs' | Plug 'ryanoasis/vim-devicons'  " file drawer
Plug 'jiangmiao/auto-pairs'
Plug 'tpope/vim-commentary' " comment stuff out
Plug 'tpope/vim-unimpaired' " mappings which are simply short normal mode aliases for commonly used ex commands
Plug 'tpope/vim-ragtag' " endings for html, xml, etc. - ehances surround
Plug 'tpope/vim-surround' " mappings to easily delete, change and add such surroundings in pairs, such as quotes, parens, etc.
Plug 'tpope/vim-obsession' " Update VIM Session file
" Plug 'dhruvasagar/vim-prosession'
Plug 'benmills/vimux' " tmux integration for vim
Plug 'vim-airline/vim-airline' " fancy statusline
Plug 'vim-airline/vim-airline-themes' " themes for vim-airline
Plug 'w0rp/ale' " Asynchonous linting engine
Plug 'tpope/vim-fugitive' " amazing git wrapper for vim
Plug 'tpope/vim-rhubarb' " hub extension for fugitive
Plug 'tpope/vim-repeat' " enables repeating other supported plugins with the . command
Plug 'tomtom/tlib_vim' " utility functions for vim
Plug 'MarcWeber/vim-addon-mw-utils' " interpret a file by function and cache file automatically
Plug 'editorconfig/editorconfig-vim' " .editorconfig support
Plug 'MarcWeber/vim-addon-mw-utils' " interpret a file by function and cache file automatically
Plug 'tomtom/tlib_vim' " utility functions for vim
Plug 'sotte/presenting.vim', { 'for': 'markdown' } " a simple tool for presenting slides in vim based on text files
Plug 'tpope/vim-dispatch' " asynchronous build and test dispatcher
Plug 'tpope/vim-vinegar' " netrw helper
Plug 'AndrewRadev/splitjoin.vim' " single/multi line code handler: gS - split one line into multiple, gJ - combine multiple lines into one
Plug 'vim-scripts/matchit.zip' " extended % matching
Plug 'tpope/vim-sleuth' " detect indent style (tabs vs. spaces)
Plug 'sickill/vim-pasta' " context-aware pasting
" Plug 'webdevel/tabulous' " better tabline, and tab labels
Plug 'junegunn/vim-easy-align' " Better alignment
Plug 'airblade/vim-gitgutter' " Show git changes in gutter
Plug 'junegunn/limelight.vim' " Hyperfocus-writing in Vim
Plug 'junegunn/goyo.vim' " Distraction-free writing in Vim
Plug 'mhinz/vim-startify' " The fancy start screen for Vim
Plug 'craigemery/vim-autotag'

" completion
Plug 'SirVer/ultisnips'
Plug 'honza/vim-snippets'
Plug 'ternjs/tern_for_vim', { 'for': ['javascript', 'javascript.jsx'], 'do': 'npm install' }
Plug 'Valloric/YouCompleteMe', { 'do': 'python3 ./install.py --clang-completer --tern-completer' }

" html / templates
Plug 'mattn/emmet-vim', { 'for': ['html', 'javascript.jsx'] } " emmet support for vim - easily create markdup wth CSS-like syntax
Plug 'gregsexton/MatchTag', { 'for': 'html' } " match tags in html, similar to paren support
Plug 'othree/html5.vim', { 'for': 'html' } " html5 support
Plug 'mustache/vim-mustache-handlebars' " mustach support
Plug 'digitaltoad/vim-pug', { 'for': ['jade', 'pug'] } " pug / jade support

" JavaScript
Plug 'pangloss/vim-javascript', { 'for': ['javascript', 'javascript.jsx', 'html'] }
Plug 'moll/vim-node', { 'for': ['javascript', 'javascript.jsx'] } " node support
Plug 'mxw/vim-jsx', { 'for': ['javascript.jsx', 'javascript'] } " JSX support
Plug 'maksimr/vim-jsbeautify', { 'for': ['javascript', 'javascript.jsx', 'jsx', 'html', 'css', 'json'], 'do': 'git submodule update --init --recursive' } " Code formatting

" TypeScript
Plug 'leafgarland/typescript-vim', { 'for': 'typescript' } " typescript support

" Elm
Plug 'lambdatoast/elm.vim', { 'for': 'elm' }

" Ruby
Plug 'tpope/vim-endwise', { 'for': 'ruby' } " automatically add end in ruby

" CoffeeScript
Plug 'kchmck/vim-coffee-script', { 'for': 'coffeescript' }

" styles
Plug 'wavded/vim-stylus', { 'for': ['stylus', 'markdown'] } " markdown support
Plug 'groenewege/vim-less', { 'for': 'less' } " less support
Plug 'hail2u/vim-css3-syntax', { 'for': 'css' } " CSS3 syntax support
Plug 'cakebaker/scss-syntax.vim', { 'for': 'scss' } " sass scss syntax support

" Misc scripts
Plug 'vim-scripts/applescript.vim', { 'for': 'applescript' } " applescript syntax support

" markdown
Plug 'itspriddle/vim-marked', { 'for': 'markdown', 'on': 'MarkedOpen' } " Open markdown files in Marked.app - mapped to <leader>m
Plug 'tpope/vim-markdown', { 'for': 'markdown' } " markdown support

" language-specific plugins
Plug 'elzr/vim-json', { 'for': 'json' } " JSON support
Plug 'Shougo/vimproc.vim', { 'do': 'make' } " interactive command execution in vim
Plug 'fatih/vim-go', { 'for': 'go' } " go support
Plug 'timcharper/textile.vim', { 'for': 'textile' } " textile support

call plug#end()

source ~/.config/nvim/plugins.vim

" Section General {{{

" Abbreviations
abbr funciton function
abbr teh the
abbr tempalte template
abbr fitler filter
abbr cosnt const

set nocompatible            " not compatible with vi
set autoread                " detect when a file is changed

set history=1000            " change history to 1000
set textwidth=180

set backupdir=~/.vim-tmp,~/.tmp,~/tmp,/var/tmp,/tmp
set directory=~/.vim-tmp,~/.tmp,~/tmp,/var/tmp,/tmp

let g:python_host_prog = '/usr/local/bin/python'
let g:python3_host_prog = '/usr/local/bin/python3'

if (has('nvim'))
	" show results of substition as they're happening
	" but don't open a split
	set inccommand=nosplit
endif

" }}}

" Section User Interface {{{

" switch cursor to line when in insert mode, and block when not
set guicursor=n-v-c:block,i-ci-ve:ver25,r-cr:hor20,o:hor50
  \,a:blinkwait700-blinkoff400-blinkon250-Cursor/lCursor
  \,sm:block-blinkwait175-blinkoff150-blinkon175

if &term =~ '256color'
    " disable background color erase
    set t_ut=
endif

" enable 24 bit color support if supported
if (has('mac') && has("termguicolors"))
    set termguicolors
endif

let g:onedark_termcolors=256
let g:onedark_terminal_italics=1
let g:dracula_terminal_italics=1
let g:hybrid_custom_term_colors = 1
" let g:hybrid_reduced_contrast = 1 " Remove this line if using the default palette.

syntax on
set t_Co=256                  " Explicitly tell vim that the terminal supports 256 colors
set background=dark           " Enable dark background
colorscheme hybrid           " Set the colorscheme

" make the highlighting of tabs and other non-text less annoying
highlight SpecialKey ctermbg=none ctermfg=8
highlight NonText ctermbg=none ctermfg=8

" make comments and HTML attributes italic
highlight Comment cterm=italic
highlight htmlArg cterm=italic
highlight xmlAttrib cterm=italic
highlight Normal ctermbg=none

set number                  " show line numbers
" set relativenumber          " show relative line numbers

set wrap                    " turn on line wrapping
set wrapmargin=4            " wrap lines when coming within n characters from side
set linebreak               " set soft wrapping
set showbreak=…             " show ellipsis at breaking

set autoindent              " automatically set indent of new line
set smartindent
set breakindent

" toggle invisible characters
set list
set listchars=tab:→\ ,eol:¬,trail:⋅,extends:❯,precedes:❮
set showbreak=↪

" highlight conflicts
match ErrorMsg '^\(<\|=\|>\)\{7\}\([^=].\+\)\?$'

" make backspace behave in a sane manner
set backspace=indent,eol,start

" Tab control
set expandtab               " insert spaces for <Tab>
set smarttab                " tab respects 'tabstop', 'shiftwidth', and 'softtabstop'
set tabstop=2               " the visible width of tabs
set softtabstop=2           " edit as if the tabs are 4 characters wide
set shiftwidth=2            " number of spaces to use for indent and unindent
set shiftround              " round indent to a multiple of 'shiftwidth'
set completeopt+=longest

" code folding settings
set foldmethod=syntax       " fold based on indent
set foldnestmax=10          " deepest fold is 10 levels
set nofoldenable            " don't fold by default
set foldlevel=1

set clipboard=unnamed

set ttyfast                 " faster redrawing
set diffopt+=vertical
set laststatus=2            " show the satus line all the time
set so=7                    " set 7 lines to the cursors - when moving vertical
set wildmenu                " enhanced command line completion
set hidden                  " current buffer can be put into background
set showcmd                 " show incomplete commands
set noshowmode              " don't show which mode disabled for PowerLine
set wildmode=list:longest   " complete files like a shell
set scrolloff=3             " lines of text around cursor
set shell=$SHELL
set cmdheight=1             " command bar height
set title                   " set terminal title

" Searching
set ignorecase              " case insensitive searching
set smartcase               " case-sensitive if expresson contains a capital letter
set hlsearch                " highlight search results
set incsearch               " set incremental search, like modern browsers
set nolazyredraw            " don't redraw while executing macros

set magic                   " Set magic on, for regex

set showmatch               " show matching braces
set mat=2                   " how many tenths of a second to blink

" error bells
set noerrorbells
set visualbell
set t_vb=
set tm=500

if has('mouse')
  set mouse=a
  " set ttymouse=xterm2
endif

" }}}

" Section Mappings {{{

" set a map leader for more key combos
let mapleader = ','

" remap esc
inoremap jk <esc>

" wipout buffer
nmap <silent> <leader>b :bw<cr>

" shortcut to save
nmap <leader>, :w<cr>

" set paste toggle
set pastetoggle=<leader>v

" toggle paste mode
" map <leader>v :set paste!<cr>

" edit ~/.config/nvim/init.vim
map <leader>ev :e! ~/.config/nvim/init.vim<cr>
" edit gitconfig
map <leader>eg :e! ~/.gitconfig<cr>
" edit ~/.config/nvim/plugins.vim
map <leader>evp :e! ~/.config/nvim/plugins.vim<cr>

" clear highlighted search
noremap <space> :set hlsearch! hlsearch?<cr>

" activate spell-checking alternatives
nmap ;s :set invspell spelllang=en<cr>
imap <F7> <Esc>1z=`]a
nmap <F7> <Esc>[s1z=

" markdown to html
nmap <leader>md :%!markdown --html4tags <cr>

" remove extra whitespace
nmap <leader><space> :%s/\s\+$<cr>
nmap <leader><space><space> :%s/\n\{2,}/\r\r/g<cr>


nmap <leader>l :set list!<cr>

" Textmate style indentation
vmap <leader>[ <gv
vmap <leader>] >gv
nmap <leader>[ <<
nmap <leader>] >>

" Add ;, to EOL (for coding)
function! ToggleEndChar(charToMatch)
    s/\v(.)$/\=submatch(1)==a:charToMatch ? '' : submatch(1).a:charToMatch
endfunction
" Add semicolor to EOL
nnoremap <silent> ;; mz:call ToggleEndChar(';')<CR>`z
" Add comma to EOL
nnoremap <silent> ;, mz:call ToggleEndChar(',')<CR>`z

" switch between current and last buffer
nmap <leader>. <c-^>

" enable . command in visual mode
vnoremap . :normal .<cr>

map <silent> <C-h> :call functions#WinMove('h')<cr>
map <silent> <C-j> :call functions#WinMove('j')<cr>
map <silent> <C-k> :call functions#WinMove('k')<cr>
map <silent> <C-l> :call functions#WinMove('l')<cr>

map <leader>wc :wincmd q<cr>

" toggle cursor line
nnoremap <leader>i :set cursorline!<cr>
" set cursorline

" scroll the viewport faster
nnoremap <C-e> 3<C-e>
nnoremap <C-y> 3<C-y>

" moving up and down work as you would expect
nnoremap <silent> j gj
nnoremap <silent> k gk
nnoremap <silent> ^ g^
nnoremap <silent> $ g$

" search for word under the cursor
nnoremap <leader>/ "fyiw :/<c-r>f<cr>

" inoremap <tab> <c-r>=Smart_TabComplete()<CR>

map <leader>r :call RunCustomCommand()<cr>
" map <leader>s :call SetCustomCommand()<cr>
let g:silent_custom_command = 0

" helpers for dealing with other people's code
nmap \t :set ts=2 sts=2 sw=2 noet<cr>
nmap \s :set ts=2 sts=2 sw=2 et<cr>

nnoremap <silent> <leader>u :call functions#HtmlUnEscape()<cr>

" EasyAlign
nmap ga <Plug>(EasyAlign)
xmap ga <Plug>(EasyAlign)
nmap <silent> <Leader>a mz<Plug>(EasyAlign)ip=`z
" nmap <silent> <Leader>a: mz<Plug>(EasyAlign)ip:`z

" }}}

" Section AutoGroups {{{
" file type specific settings
augroup configgroup
    autocmd!

    " automatically resize panes on resize
    autocmd VimResized * exe 'normal! \<c-w>='
    autocmd BufWritePost .vimrc,.vimrc.local,init.vim source %
    autocmd BufWritePost .vimrc.local source %
    " save all files on focus lost, ignoring warnings about untitled buffers
    autocmd FocusLost * silent! wa

    " make quickfix windows take all the lower section of the screen
    " when there are multiple windows open
    autocmd FileType qf wincmd J
    autocmd FileType qf nmap q :q<cr>

    autocmd BufNewFile,BufReadPost *.md set filetype=markdown
    let g:markdown_fenced_languages = ['css', 'javascript', 'js=javascript', 'json=javascript', 'stylus', 'html']

    " autocmd! BufEnter * call functions#ApplyLocalSettings(expand('<afile>:p:h'))

    autocmd BufNewFile,BufRead,BufWrite *.md syntax match Comment /\%^---\_.\{-}---$/

    autocmd! BufWritePost * Neomake

    " JsBeautify Shortcuts
    """""""""""""""""""""""""""""""""""""
    " autoformat code
    " format entire file
    autocmd FileType javascript noremap <buffer> <leader>f :call JsBeautify()<cr>
    autocmd FileType json noremap <buffer> <leader>f :call JsonBeautify()<cr>
    autocmd FileType jsx noremap <buffer> <leader>f :call JsxBeautify()<cr>
    autocmd FileType html noremap <buffer> <leader>f :call HtmlBeautify()<cr>
    autocmd FileType css noremap <buffer> <leader>f :call CSSBeautify()<cr>
    " format selection
    autocmd FileType javascript vnoremap <buffer> <leader>rf :call RangeJsBeautify()<cr>
    autocmd FileType json vnoremap <buffer> <leader>rf :call RangeJsonBeautify()<cr>
    autocmd FileType jsx vnoremap <buffer> <leader>rf :call RangeJsxBeautify()<cr>
    autocmd FileType html vnoremap <buffer> <leader>rf :call RangeHtmlBeautify()<cr>
    autocmd FileType css vnoremap <buffer> <leader>rf :call RangeCSSBeautify()<cr>
augroup END

" }}}

" Section Plugins {{{

" Beautify
let g:editorconfig_Beautifier = '~/.editorconfig'

" Toggle NERDTree
nmap <silent> <leader>k :NERDTreeTabsToggle<cr>
" expand to the path of the file in the current buffer
nmap <silent> <leader>y :NERDTreeTabsFind<cr>

let NERDTreeShowHidden=1
let NERDTreeDirArrowExpandable = '▷'
let NERDTreeDirArrowCollapsible = '▼'

" FZF
"""""""""""""""""""""""""""""""""""""
let g:fzf_layout = { 'down': '~25%' }

if isdirectory(".git")
    " if in a git project, use :GFiles
    nmap <silent> <leader>t :GFiles<cr>
else
    " otherwise, use :FZF
    nmap <silent> <leader>t :FZF<cr>
endif

nmap <silent> <leader>w :Window<cr>
nmap <silent> <leader>r :Buffers<cr>
nmap <silent> <leader>e :FZF<cr>
nmap <leader><tab> <plug>(fzf-maps-n)
xmap <leader><tab> <plug>(fzf-maps-x)
omap <leader><tab> <plug>(fzf-maps-o)

" Insert mode completion
imap <c-x><c-k> <plug>(fzf-complete-word)
imap <c-x><c-f> <plug>(fzf-complete-path)
imap <c-x><c-j> <plug>(fzf-complete-file-ag)
imap <c-x><c-l> <plug>(fzf-complete-line)

nnoremap <silent> <Leader>C :call fzf#run({
\   'source':
\     map(split(globpath(&rtp, "colors/*.vim"), "\n"),
\         "substitute(fnamemodify(v:val, ':t'), '\\..\\{-}$', '', '')"),
\   'sink':    'colo',
\   'options': '+m',
\   'left':    30
\ })<CR>

command! FZFMru call fzf#run({
\  'source':  v:oldfiles,
\  'sink':    'e',
\  'options': '-m -x +s',
\  'down':    '40%'})

command! -bang -nargs=* Find call fzf#vim#grep(
	\ 'rg --column --line-number --no-heading --follow --color=always '.<q-args>, 1,
	\ <bang>0 ? fzf#vim#with_preview('up:60%') : fzf#vim#with_preview('right:50%:hidden', '?'), <bang>0)


" Fugitive Shortcuts
"""""""""""""""""""""""""""""""""""""
nmap <silent> <leader>gs :Gstatus<cr>
nmap <leader>ge :Gedit<cr>
nmap <silent><leader>gr :Gread<cr>
nmap <silent><leader>gb :Gblame<cr>

nmap <leader>m :MarkedOpen!<cr>
nmap <leader>mq :MarkedQuit<cr>
nmap <leader>* *<c-o>:%s///gn<cr>

let g:neomake_javascript_jshint_maker = {
    \ 'args': ['--verbose'],
    \ 'errorformat': '%A%f: line %l\, col %v\, %m \(%t%*\d\)',
\ }

let g:neomake_typescript_tsc_maker = {
    \ 'args': ['-m', 'commonjs', '--noEmit' ],
    \ 'append_file': 0,
    \ 'errorformat':
        \ '%E%f %#(%l\,%c): error %m,' .
        \ '%E%f %#(%l\,%c): %m,' .
        \ '%Eerror %m,' .
        \ '%C%\s%\+%m'
\ }

let g:neomake_javascript_enabled_makers = ['jshint', 'eslint']

" airline options
let g:airline_powerline_fonts=1
let g:airline_left_sep=''
let g:airline_right_sep=''
let g:airline_theme='hybrid'
let g:airline#extensions#tabline#enabled = 1 " enable airline tabline
let g:airline#extensions#tabline#tab_min_count = 2 " only show tabline if tabs are being used (more than 1 tab open)
let g:airline#extensions#tabline#show_buffers = 0 " do not show open buffers in tabline
let g:airline#extensions#tabline#show_splits = 0

" don't hide quotes in json files
let g:vim_json_syntax_conceal = 0

" completion
" https://tinyurl.com/zkurrdk
let g:deoplete#enable_at_startup = 1 " enable Deoplete
call deoplete#custom#set('_', 'min_pattern_length', 1)
let g:deoplete#enable_ignore_case = 'ignorecase'
let g:deoplete#enable_smart_case = 1

let g:deoplete#omni#functions = {}
let g:deoplete#omni#functions.javascript = [
  \ 'tern#Complete',
  \ 'jspc#omni'
\]
set completeopt=longest,menuone ",preview
let g:deoplete#sources = {}
let g:deoplete#sources['javascript'] = ['file', 'ultisnips', 'ternjs']
let g:deoplete#sources['javascript.jsx'] = ['file', 'ultisnips', 'ternjs']
"Add extra filetypes
let g:tern#filetypes = [
                \ 'jsx',
                \ 'javascript.jsx',
                \ 'vue'
                \ ]
let g:tern#command = ['tern']
let g:tern#arguments = ['--persistent', '--no-port-file']

aug omnicomplete
  au!
  au FileType javascript,jsx,javascript.jsx setl omnifunc=tern#Complete
aug END

" supertab
let g:SuperTabCrMapping = 0
autocmd FileType javascript,javascript.jsx,jsx let g:SuperTabDefaultCompletionType = "<c-x><c-o>"
let g:UltiSnipsExpandTrigger="<C-j>"
inoremap <expr><TAB> pumvisible() ? "\<C-n>" : "\<TAB>"

" git gutter
let g:gitgutter_realtime = 1

" delimitMate
"""""""""""""""""""""""""""""""""""
let delimitMate_expand_cr=1                 " enable expansion of <CR>
let delimitMate_expand_space=1              " enable expansion of <Space>

" ack
" use silver searcher (ag) if available
if executable('ag')
  let g:ackprg = 'ag --vimgrep'
endif

" alias and shortcut
cnoreabbrev Ack Ack!
nnoremap <leader>s :Ack!<space><c-r>=expand("<cword>")<cr>

" move line/selection up down
let g:move_map_keys = 0
vmap <C-S-j> <Plug>MoveBlockDown
vmap <C-S-k> <Plug>MoveBlockUp
nmap <C-S-j> <Plug>MoveLineDown
nmap <C-S-k> <Plug>MoveLineUp

" Limelight, Goyo - Focus writing
let g:goyo_width=100
nmap <leader>g :Goyo<CR>

function! s:goyo_enter()
  silent !tmux set status off
  silent !tmux list-panes -F '\#F' | grep -q Z || tmux resize-pane -Z
  set noshowmode
  set noshowcmd
  set noruler
  set laststatus=0
  set scrolloff=999
  Limelight

  " Ensure :q works
  let b:quitting = 0
  let b:quitting_bang = 0
  autocmd QuitPre <buffer> let b:quitting = 1
  cabbrev <buffer> q! let b:quitting_bang = 1 <bar> q!

  " close NERDTree
  NERDTreeClose
endfunction

function! s:goyo_leave()
  silent !tmux set status on
  silent !tmux list-panes -F '\#F' | grep -q Z && tmux resize-pane -Z
  set showmode
  set showcmd
  set ruler
  set laststatus=2
  set scrolloff=5
  Limelight!

  " Quit Vim if this is the only remaining buffer
  if b:quitting && len(filter(range(1, bufnr('$')), 'buflisted(v:val)')) == 1
    if b:quitting_bang
      qa!
    else
      qa
    endif
  endif
endfunction

autocmd! User GoyoEnter nested call <SID>goyo_enter()
autocmd! User GoyoLeave nested call <SID>goyo_leave()

" }}}

" vim:foldmethod=marker:foldlevel=0

" Style overrides
" italics
highlight Comment cterm=italic
" NeoMake
" highlight NeomakeErrorSignCustom ctermfg=1 ctermbg=237
" let g:neomake_error_sign={'text': '⚠', 'texthl': 'NeomakeErrorSignCustom'}

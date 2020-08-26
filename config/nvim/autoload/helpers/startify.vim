function! helpers#startify#listcommits()
    let git = 'git -C ' . getcwd()
    if has('win32')
        let commits = systemlist(git . ' log --oneline | select -first 5')
    else
        let commits = systemlist(git . ' log --oneline | head -n5')
    endif
    let git = 'G' . git[1:]
    return map(commits, '{"line": matchstr(v:val, "\\s\\zs.*"), "cmd": "'. git .' show ". matchstr(v:val, "^\\x\\+") }')
endfunction

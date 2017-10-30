# List of plugins
set -g @plugin 'tmux-plugins/tpm'
# set -g @plugin 'tmux-plugins/tmux-sensible'
set -g @plugin 'tmux-plugins/tmux-resurrect'
set -g @plugin 'tmux-plugins/tmux-continuum'

# Configuration
##############################
# Tmux Resurrect Settings
set -g @resurrect-strategy-vim 'session'            # Fom vim
set -g @resurrect-strategy-nvim 'session'           # For neovim

set -g @continuum-boot 'on'
set -g @continuum-boot-options 'iterm,fullscreen'   # Start iterm in fullscreen
set -g @continuum-restore 'on'                      # Automatic Save and Restore TMUX Sessions
set -g @continuum-save-interval '5'
set -g @resurrect-capture-pane-contents 'on'

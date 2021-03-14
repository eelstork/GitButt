# GitButt
Lean &amp; mean Git button for Unity

## Explanation

This is a simple git integration. Its intended use is helping non-programmers use git from Unity, without worrying about 'the details'.

- **Goal** - contributors keep their local repo up to date and regularly push content to the main repo.
- **Non goal** - qualified support for branching to version control hell. Also, *by default* Git-butt won't let you push modified *.cs files.

## How-to

After installing the UPM package the little git button will appear.

- [☁] everything is nice, and you made no changes.
- [⇣] remote changes detected, press to update.
- [↑] you have made changes; press to enter a commit message and push.
- [X] git-butt is groping in the dark; use your favorite git front end and/or shout for assistance (don't submit an issue; git-button is *trying* to etch a line between what it can, or by design is not intended to, help with).

## Acknowledgements

- Toolbar Extender | https://github.com/marijnz/unity-toolbar-extender (MIT)
- Stewart McCready | https://stewmcc.com/post/git-commands-from-unity/

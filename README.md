# GitButt
Lean &amp; mean Git button for Unity

## What

Simple git integration for non-programmers; use git from Unity without worrying about 'the details'.

- **Goal** - contributors keep their local repo up to date, regularly push to the main repo.
- **Non goal** - qualified support for branching to version control hell.

Note: *By default* Git-butt won't let you push modified \*.cs files

## Release *.pkg

Grab a Unity package here:
https://github.com/eelstork/GitButt/releases/tag/0.0.1

## Via UPM [BROKEN]

Add to the Unity package manager (UPM) via:

https://github.com/eelstork/GitButt.git?path=GitButton/Assets/Package

As of writing the UPM package will install, but the little git button does not appear.

## How-to

After install, the little git button will appear at the top of the editor window.

- `( ☁ )` everything is nice, and you made no changes.
- `( ⇣ )` remote changes detected, press to update.
- `( ↑ )` you have made changes; press to enter a commit message and push.
- `( ℂ )` C# source changes; not pushing this for you.
- `( X )` git-butt is groping in the dark; use your favorite git front end and/or shout for assistance (don't submit an issue; git-button is *trying* to draw a line between what it can, or by design is not intended to, help with).

## Acknowledgements

- Toolbar Extender | https://github.com/marijnz/unity-toolbar-extender (MIT)
- Stewart McCready | https://stewmcc.com/post/git-commands-from-unity/

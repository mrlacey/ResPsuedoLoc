# Resource Pseudo-Localizer

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
![Works with Visual Studio 2022](https://img.shields.io/static/v1.svg?label=VS&message=2022&color=5F2E96)
![Visual Studio Marketplace 5 Stars](https://img.shields.io/badge/VS%20Marketplace-★★★★★-green)

[![Build](https://github.com/mrlacey/ResPsuedoLoc/actions/workflows/build.yaml/badge.svg)](https://github.com/mrlacey/ResPsuedoLoc/actions/workflows/build.yaml)
![Tests](https://gist.githubusercontent.com/mrlacey/c586ff0f495b4a8dd76ab0dbdf9c89e0/raw/ResPsuedoLoc.badge.svg)

Download the extension from the [VS Marketplace](https://marketplace.visualstudio.com/items?itemName=MattLaceyLtd.ResPseudoLoc)
or get the
[CI build](http://vsixgallery.com/extension/ResPsuedoLoc.fb9c5e68-fb3b-44f4-9412-717109dc3ba9/)

A Visual Studio extention that provides a quick way to check that all string resources are localized by "pseudo-localizing" them.

If you don't speak another language it can be tricky to verify that all UI string resources are localized correctly, this tool provides a quick way to modify all the resources so that when running the app it should be easy to recognize anything that hasn't been localized.

It works with RESX and RESW files. Right click on the resource file and select the option you want. All the entries in the file will then be modified accordingly.

![Example of context menu](./assets/rpl-contextmenu.png)

Options can be combined and toggled by repeating the action.

See the [change log](CHANGELOG.md) for changes and road map.

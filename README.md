# Resource Pseudo-Localizer

Download the extension from the [VS Marketplace](https://marketplace.visualstudio.com/items?itemName=MattLaceyLtd.ResourcePseudoLocalizer)

[![Build status](https://ci.appveyor.com/api/projects/status/kethy80vjrqfsucc?svg=true)](https://ci.appveyor.com/project/mrlacey/respsuedoloc)

A Visual Studio Extention that provides a quick way to check that all string resources are localized by pseudo-localizing them.

If you don't speak another language it can be tricky to verify that all UI string resources are localized correctly, this tool provides a quick way to modify all the resources so that when running the app it should be easy to recognize anything that hasn't been localized.

It works with RESX and RESW files. Right click on the resource file and select the option you want. All the entries in the file will then be modified accordingly.

![Example of context menu](./assets/rpl-contextmenu.png)

Options can be combined and toggled by repeating the action.

See the [change log](CHANGELOG.md) for changes and road map.

## License

[MIT](LICENSE)

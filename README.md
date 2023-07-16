# ULTRANET
The Open-Source ULTRAKILL Multiplayer Mod

## What is ULTRANET?
ULTRANET is a mod for the first-person shooter [ULTRAKILL](https://store.steampowered.com/app/1229490/ULTRAKILL). This mod allows for multiple people to play ULTRAKILL together

## Required Tools
- C#-compatible IDE (like [Visual Studio](https://visualstudio.microsoft.com) or [JetBrains Rider](https://www.jetbrains.com/rider/))
- [.NET Framework 4.7.2](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472)
- [BepInEx 5.X.X](https://github.com/BepInEx/BepInEx/releases)
- [Ultra Mod Manager](https://github.com/Temperz87/ultra-mod-manager/releases)
- [ULTRAKILL](https://store.steampowered.com/app/1229490/ULTRAKILL)

## FAQ
Q: Is this meant to be a competitor to MULTIKILL?
A: This mod is not meant to compete with MULTIKILL. It's just a fun project that I'm making and open-sourcing.

Q: Is the mod playable?
A: Not in the slightest, the mod is currently in early development though we'll try to get an Alpha out as soon as we think we're ready for it.

## Compilation & Setup
In order to build ULTRANET follow these steps:

### First time setup:
1. Add DLLs to `Libraries/ULTRAKILL` from `ULTRAKILL/ULTRAKILL_Data/Managed` (read `Libraries/ULTRAKILL/Libraries.txt`)
2. Reference the DLLs from the `Libraries` folder

### Building
1. Use the Build Feature in your IDE
2. If the compilation fails, [open an issue](https://github.com/averyocean65/ULTRANET/issues/new) or ask for help in our [Discord Server](https://discord.gg/rBvqHKhsB5)!

## ULTRAKILL
In order to use ULTRANET in ULTRAKILL follow these steps:

### Setup
1. (Optional) Create a backup of your ULTRAKILL game files
2. Load BepInEx 5.X.X into your ULTRAKILL directory
3. Install `Ultra Mod Manager` (UMM.dll) into `ULTRAKILL/BepInEx/plugins/UMM`
4. Launch ULTRAKILL to initialize UMM and BepInEx
5. Load all DotNetty, Microsoft, ULTRANET.Core and System libraries into `BepInEx/plugins/ULTRANET`
6. Load `ULTRANET.Client` into `BepInEx/UMM Mods/ULTRANET`
7. Launch ULTRAKILL and enable the mod

If you encounter any issues, [open an issue](https://github.com/averyocean65/ULTRANET/issues/new) or ask for help in our [Discord Server](https://discord.gg/rBvqHKhsB5)!

## Notices
- [Library Notice](./Library%20Notice.md)

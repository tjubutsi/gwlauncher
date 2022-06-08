
### [Releases/Download](https://github.com/GregLando113/gwlauncher/releases)

### Overview

* * *

*   Able to add accounts to a list in order to quickly launch them on demand
*   Active column shows what accounts are currently loaded in
*   Client window title renamed at launch to match the title or character name
*   Load dll plugins and texmods (see below)
*   Open source

* * *

1.  Click [download](https://github.com/GregLando113/gwlauncher/releases) below.
2.  Select the latest release.
3.  Put the executable in its own folder! It will create files (Settings.json, Accounts.json, d3d9.dll) in the same location when you launch it.
4.  Run the executable.
5.  If you do not have the [.NET 6 Desktop Runtime (x86)](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed, it will prompt you to download it.

### Usage

* * *

#### Add an Account

Right click the Main Launcher Window to reveal a context menu, then select _Add New_. The Add Account window will then pop up. Fill out all the information it asks for, then hit Add. The new account will now appear in the main form list. Double click to launch.

#### Remove an Account

Right click the Main Launcher Window to reveal a context menu, then select _Remove Selected_. The selected account on the main list will then be deleted.

#### Edit Account Information (Accounts.json)

Right click the Main Launcher Window to reveal a context menu, then select _Edit Selected_. The selected account on the main list will then be opened with a form where you can modify information. You may also modify Accounts.json directly, then click _Refresh Accounts_. Here is an example of the Accounts.json:

<pre>[
  {
    "email": "derp@derperkins.derp",
    "password": "derpyderp",
    "character": "D E R P Y",
    "gwpath": "C:\\Derp\\GW\\Gw.exe",
    "datfix": true,
    "extraargs":""
  },
  {
    "email": "derp1@derperkins.derp",
    "password": "asf",
    "character": "Derpyless Derp",
    "gwpath": "C:\\Derp\\GW\\Gw.exe",
    "datfix": true,
    "extraargs":""
  },
  {
    "email": "derp2@derperkins.derp",
    "password": "poop",
    "character": "Werpy Derp",
    "gwpath": "C:\\Derp\\GW3\\Gw.exe",
    "datfix": false,
    "extraargs":""
  }
]
</pre>

This format is fairly straight forward, modify the account info of the selected account, then save the file and re-launch GW Launcher.exe.

### Settings

There are three settings that you can change in the file Settings.json:

*	Encrypt: bool, if GW Launcher will ask you for a password and encrypt your account info.
*	CheckForUpdates: bool, if GW Launcher should check for new releases, default true
*	AutoUpdate: bool, if GW Launcher should automatically update, default false

#### GW Plugins (.dll or .tpf)

If you create a folder named "plugins", the launcher will load all .dll's or shortcuts (.lnk) to .dll's inside this folder on launch.  
Dll's placed in `gwlauncher/plugins` folder will load for every instance, dll's in the `<gw-installation-path>/plugins` folder will only load for accounts launched from that path.  
Alternatively there is also a GUI to select plugins for specific accounts when you edit the account.

Similarly, all .tpf or .zip files will be loaded as TexMod files. This means that you do not have to use uMod anymore, if you only want basic TexMod functionality.  
Plugins are loaded in the alphabetical order of their filename. Textures that are already replaced by 1_FirstTexmod.tpf cannot be replaced by subsequent 2_SecondTexmod.tpf, so make sure your texmods are named in ascending alphabetical order.

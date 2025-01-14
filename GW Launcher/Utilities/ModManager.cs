﻿using IWshRuntimeLibrary;

namespace GW_Launcher.Utilities;

internal class ModManager
{
    public static IEnumerable<string> GetPreloadDlls(string path, IReadOnlyCollection<Mod> mods)
    {
        return GetMods(path, mods).Item1;
    }

    public static IOrderedEnumerable<string> GetDlls(string path, IReadOnlyCollection<Mod> mods)
    {
        return GetMods(path, mods).Item2;
    }

    public static IOrderedEnumerable<string> GetTexmods(string path, IReadOnlyCollection<Mod> mods)
    {
        return GetMods(path, mods).Item3;
    }

    private static Tuple<IOrderedEnumerable<string>, IOrderedEnumerable<string>, IOrderedEnumerable<string>>
        GetMods(string path, IReadOnlyCollection<Mod> mods)
    {
        var directory = Path.Combine(Directory.GetCurrentDirectory(), "plugins");
        var dllsToLoad = new List<string>();
        var dllsToPreload = new List<string>();
        var texsToLoad = new List<string>();
        if (Directory.Exists(directory))
        {
            var links = Directory.GetFiles(directory, "*.lnk");
            var files = Directory.GetFiles(directory, "*.dll");
            var dlllinks = links.Select(GetShortcutPath).Where(l => l.EndsWith(".dll")).ToArray();
            var textures = Directory.GetFiles(directory, "*").Where(t => t.EndsWith(".tpf") || t.EndsWith(".zip"));

            dllsToLoad.AddRange(files);
            dllsToLoad.AddRange(dlllinks);
            texsToLoad.AddRange(textures);
        }

        if (Directory.Exists(Path.Combine(directory, "preload")))
        {
            var links = Directory.GetFiles(directory, "*.lnk");
            var files = Directory.GetFiles(directory, "*.dll");
            var dlllinks = links.Select(GetShortcutPath).Where(l => l.EndsWith(".dll")).ToArray();

            dllsToPreload.AddRange(files);
            dllsToPreload.AddRange(dlllinks);
        }

        directory = Path.Combine(Path.GetDirectoryName(path)!, "plugins");
        if (Directory.Exists(directory))
        {
            var links = Directory.GetFiles(directory, "*.lnk");
            var files = Directory.GetFiles(directory, "*.dll");
            var dlllinks = links.Select(GetShortcutPath).Where(l => l.EndsWith(".dll")).ToArray();
            var textures = Directory.GetFiles(directory, "*").Where(t => t.EndsWith(".tpf") || t.EndsWith(".zip"));

            dllsToLoad.AddRange(dlllinks);
            dllsToLoad.AddRange(files);
            texsToLoad.AddRange(textures);
        }

        if (Directory.Exists(Path.Combine(directory, "preload")))
        {
            var links = Directory.GetFiles(directory, "*.lnk");
            var files = Directory.GetFiles(directory, "*.dll");
            var dlllinks = links.Select(GetShortcutPath).Where(l => l.EndsWith(".dll")).ToArray();

            dllsToPreload.AddRange(files);
            dllsToPreload.AddRange(dlllinks);
        }

        dllsToLoad.AddRange(mods
            .Where(mod => mod.type == ModType.kModTypeDLL && mod.active && System.IO.File.Exists(mod.fileName))
            .Select(mod => mod.fileName));
        texsToLoad.AddRange(mods
            .Where(mod => mod.type == ModType.kModTypeTexmod && mod.active && System.IO.File.Exists(mod.fileName))
            .Select(mod => mod.fileName));

        if (texsToLoad.Count > 0)
        {
            dllsToLoad.RemoveAll(p => Path.GetFileName(p) == "d3d9.dll"); // don't load any other d3d9.dll
            dllsToPreload.RemoveAll(p => Path.GetFileName(p) == "d3d9.dll"); // don't load any other d3d9.dll
            dllsToPreload.Add(Path.Combine(Directory.GetCurrentDirectory(), "d3d9.dll")); // load d3d9.dll for umod
        }

        return Tuple.Create(
            dllsToPreload.Distinct().OrderBy(Path.GetFileName),
            dllsToLoad.Distinct().OrderBy(Path.GetFileName),
            texsToLoad.Distinct().OrderBy(Path.GetFileName)
        );
    }

    private static string GetShortcutPath(string path)
    {
        var shell = new WshShell();
        var lnk = (IWshShortcut) shell.CreateShortcut(path);

        return lnk.TargetPath;
    }
}

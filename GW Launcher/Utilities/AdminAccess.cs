﻿using System.Security.Principal;

namespace GW_Launcher.Utilities;

internal static class AdminAccess
{
    public static bool HasAdmin()
    {
        var pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
        var hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
        return hasAdministrativeRight;
    }

    public static bool RestartAsAdminPrompt(bool force = false)
    {
        if (HasAdmin())
        {
            return false;
        }

        // relaunch the application with admin rights
        var fileName = Environment.ProcessPath;
        var processInfo = new ProcessStartInfo
        {
            Verb = "runas",
            UseShellExecute = true,
            FileName = fileName,
            Arguments = "restart"
        };

        try
        {
            Program.shouldClose = true;
            Application.Exit();
            Process.Start(processInfo);
        }
        catch (Win32Exception)
        {
            // This will be thrown if the user cancels the prompt
            if (force)
            {
                Program.shouldClose = true;
                Application.Exit();
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}

using System;
using System.Diagnostics;

var currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);

const string DashboardLocation =
    @"C:\GitHub\code\Impulse.Dashboard\Impulse.Framework\Impulse.Dashboard\bin\Debug\net6.0-windows\Impulse.Dashboard.exe";

const string PluginDirectory = @"C:\GitHub\code\Solvle\Application\Debug\net6.0-windows";

Process.Start(DashboardLocation, $"--plugin {PluginDirectory}");
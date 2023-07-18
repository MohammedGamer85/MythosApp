﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using mythos.APIRequests;
using mythos.Services;
using mythos.ViewModels;
using mythos.Views;
using System.IO;

namespace mythos;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static ServiceProvider? Services = BuildLauncherServices(); 
    public static ServiceProvider BuildLauncherServices()
    {
        var builder = new ServiceCollection()
            .AddSingleton<MainWindow>()
            .AddSingleton<HttpCaller>()
            .AddSingleton<HttpClinetHelper>()
            .AddSingleton<CreateFiles>()
            .AddSingleton<SingleTimeStartUpFuncations>()
            .AddSingleton<FillPaths>();

        var services = builder.BuildServiceProvider();
        return services;
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainView()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();

    }
}

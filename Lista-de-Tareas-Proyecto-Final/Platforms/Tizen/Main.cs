using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Lista_de_Tareas_Proyecto_Final
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}

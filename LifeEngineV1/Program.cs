using LifeEngineV1;
using System;
using OpenTK.Windowing.Desktop;

namespace LifeEngineV1
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gws = new GameWindowSettings();
            gws.UpdateFrequency = 0 ;
            NativeWindowSettings nws = new NativeWindowSettings();
            nws.Flags = OpenTK.Windowing.Common.ContextFlags.Debug;
            nws.NumberOfSamples = 0 ; // FSAA
            nws.Title = "LifeEngine";
            nws.WindowBorder = OpenTK.Windowing.Common.WindowBorder.Fixed;

            ApplicationWindow w = new ApplicationWindow(gws, nws);
            w.Run();
            w.Dispose();
        }
    }
}
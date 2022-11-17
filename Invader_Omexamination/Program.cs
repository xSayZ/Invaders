using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Invader_Omexamination
{
    class Program
    {

        public const int ScreenW = 800;
        public const int ScreenH = 900;
        public static FloatRect ScreenSize = new FloatRect(0, 0, ScreenW, ScreenH);
        
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(ScreenW, ScreenH), "Invader")) {
                window.SetView(new View(ScreenSize));
                window.Closed += (o, e) => window.Close();
                
                // TODO: Initialize
                Clock clock = new Clock();
                Scene scene = new Scene();
                scene.Load();
                
                while (window.IsOpen) {
                    
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();
                    deltaTime = MathF.Min(deltaTime, 0.01f);
                    // TODO: Updates
                    scene.UpdateAll(deltaTime);
                    
                    window.Clear(new Color(0, 0, 0));
                    // TODO: Drawing
                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}

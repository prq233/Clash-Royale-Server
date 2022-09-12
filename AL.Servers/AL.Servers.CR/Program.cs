namespace GL.Servers.CR
{
    using System;
    using System.Drawing;
    using System.Reflection;

    using GL.Servers.Core.Consoles;

    using GL.Servers.CR.Core;

    internal class Program
    {
        private const int Width    = 120;
        private const int Height   = 30;

        internal static bool isShuttingDown;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        internal static void Main()
        {
            Console.Title = $"[GRS] {Assembly.GetExecutingAssembly().GetName().Name} - Developer - {DateTime.Now.Year} ©";

            Console.SetOut(new Prefixed());

            Console.SetWindowSize(Program.Width, Program.Height);
            Console.SetBufferSize(Program.Width, Program.Height);

            Servers.Core.Consoles.Colorful.Console.WriteWithGradient(@"
                        _____   __  .__                 __         .____                       .___
                      /  _  \_/  |_|  | _____    _____/  |______  |    |   _____    ____    __| _/
                     /  /_\  \   __\  | \__  \  /    \   __\__  \ |    |   \__  \  /    \  / __ |  
                    /    |    \  | |  |__/ __ \|   |  \  |  / __ \|    |___ / __ \|   |  \/ /_/ | 
                    \____|__  /__| |____(____  /___|  /__| (____  /_______ (____  /___|  /\____ |  
                            \/               \/     \/          \/        \/    \/     \/      \/ 
                                                                                          V2.0.2
            ", Color.Yellow, Color.Fuchsia, 14);
            
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("AtlantaLand's programs are protected by our policies, available on our main website.   ");
            Console.WriteLine("AtlantaLand's programs are under the 'CC Non-Commercial-NoDerivs 3.0 Unported' license.");
            Console.WriteLine("AtlantaLand is NOT affiliated to 'Supercell Oy', 'Tencent', or 'Riot Games'.           ");
            Console.WriteLine("AtlantaLand does NOT own 'Clash of Clans', 'Boom Beach', 'Clash Royale', or 'Hay Day'. ");
            Console.WriteLine();
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name + " is now starting..." + Environment.NewLine);

            Resources.Initialize();

            // Console.WriteLine();
            // Console.WriteLine("-------------------------------------");
            // Console.WriteLine();

            Test.Initialize();
        }
    }
}
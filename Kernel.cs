using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Network.Config;
using Cosmos.System.Graphics;
using System.Drawing;
using IL2CPU.API.Attribs;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using Cosmos.System;
using Cosmos.HAL;
using Cosmos.HAL.Drivers.PCI.Audio;
using Cosmos.System.Audio.IO;
using Cosmos.System.Audio;
using Cosmos.HAL.Audio;
using Cosmos.System.Network;
using Cosmos.HAL.BlockDevice.Registers;
using System.Transactions;

namespace gcoralOS
{
    public class Kernel : Sys.Kernel
    {
        public int ScreenX = 1280;
        public int ScreenY = 720;
        public int galere = 320;

        private List<Pen> _iconColors;

        public string versiona = "2.5";
        public string nameos = "Minedos";
        public string avtor = "Michail Khodakov";
        public string username = "You";

        Sys.FileSystem.CosmosVFS fs;
        string currentdirectory = @"0:\";

        public int mode = 0;
        bool dock = false;


        public int MouseX = 0;
        public int MouseY = 0;

        private Bitmap _wall;
        [ManifestResourceStream(ResourceName = "gcoralOS.wall.bmp")]
        static byte[] wallbyte;
        //private DSMouse _cursor;

        private Bitmap _cursor;
        [ManifestResourceStream(ResourceName = "gcoralOS.сursor.bmp")] static byte[] cursorbyte;

        [ManifestResourceStream(ResourceName = "gcoralOS.startup.wav")] public static byte[] start;

        public void DSMouse(Canvas canvas)
        {
            _cursor = new Bitmap(cursorbyte);
        }

        Canvas canvas;
        protected override void BeforeRun()
        {
            if (mode == 0)
            {
                System.Console.Clear();
                System.Console.WriteLine("------------------------------------------");
                System.Console.WriteLine("Select mode for Minedos 2.5");
                System.Console.WriteLine("1 - Start in text mode with file system");
                System.Console.WriteLine("2 - Start in Ui mode(No file system)");
                System.Console.WriteLine("3 - Start in Safe Ui Mode(No file system)");
                System.Console.WriteLine("4 - Start in text mode(No file system)");
                System.Console.WriteLine("5 - Shutdown your PC");
                System.Console.WriteLine("------------------------------------------");
            }
            if (mode == 1)
            {
                System.Console.BackgroundColor = ConsoleColor.White;
                System.Console.ForegroundColor = ConsoleColor.Black;
                System.Console.Clear();

                System.Console.WriteLine("Welcome to Minedos 2.5 by Michail Khodakov");
                System.Console.WriteLine("Write 'help' to show all commands on this OS");
                System.Console.WriteLine("Please write 'gui' for run Ui Launcher for this System");
                System.Console.Beep(500, 100);
                Thread.Sleep(100);
                System.Console.Beep(500, 100);
                Thread.Sleep(100);
                System.Console.Beep(900, 300);
                fs = new Sys.FileSystem.CosmosVFS();
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            }
            if (mode == 2)
            {
                canvas = FullScreenCanvas.GetFullScreenCanvas();
                canvas.Mode = new Mode(ScreenX, ScreenY, ColorDepth.ColorDepth32);
                Sys.MouseManager.ScreenWidth = (uint)canvas.Mode.Columns;
                Sys.MouseManager.ScreenHeight = (uint)canvas.Mode.Rows;

                _wall = new Bitmap(wallbyte);

                System.Console.Beep(700, 100);
                Thread.Sleep(100);
                System.Console.Beep(700, 100);
                Thread.Sleep(100);
                System.Console.Beep(500, 300);
                //_cursor = new Bitmap(cursorbyte);
            }
            if (mode == 3)
            {
                canvas = FullScreenCanvas.GetFullScreenCanvas();
                canvas.Mode = new Mode(ScreenX, ScreenY, ColorDepth.ColorDepth32);
                Sys.MouseManager.ScreenWidth = (uint)canvas.Mode.Columns;
                Sys.MouseManager.ScreenHeight = (uint)canvas.Mode.Rows;

                //_wall = new Bitmap(wallbyte);

                System.Console.Beep(700, 100);
                Thread.Sleep(100);
                System.Console.Beep(700, 100);
                Thread.Sleep(100);
                System.Console.Beep(500, 300);
                //_cursor = new Bitmap(cursorbyte);
            }
            if (mode == 4)
            {
                System.Console.BackgroundColor = ConsoleColor.White;
                System.Console.ForegroundColor = ConsoleColor.Black;
                System.Console.Clear();

                System.Console.WriteLine("Welcome to Minedos 2.5 by Michail Khodakov");
                System.Console.WriteLine("Write 'help' to show all commands on this OS");
                System.Console.WriteLine("Please write 'gui' for run Ui Launcher for this System");
                System.Console.Beep(500, 100);
                Thread.Sleep(100);
                System.Console.Beep(500, 100);
                Thread.Sleep(100);
                System.Console.Beep(900, 300);
            }
            if (mode == 5)
            {
                Sys.Power.Shutdown();
            }

            }

        public void DrawCursor()
        {
            Pen penBlacka = new Pen(Color.Black);

            Sys.MouseManager.ScreenWidth = (uint)canvas.Mode.Columns;
            Sys.MouseManager.ScreenHeight = (uint)canvas.Mode.Rows;

            int X = (int)Sys.MouseManager.X;
            int Y = (int)Sys.MouseManager.Y;

            canvas.DrawFilledRectangle(penBlacka, X, Y, 5, 10);
            canvas.DrawFilledRectangle(penBlacka, X, Y, 10, 5);
            //canvas.DrawImageAlpha(_cursor, X, Y);
        }
        protected override void Run()
        {
            if (mode == 0)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.Write("User : ");
                System.Console.ForegroundColor = ConsoleColor.Black;
                var input = System.Console.ReadLine();
                KeyLoader(input);
            }
            if (mode == 1)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.Write("User : ");
                System.Console.ForegroundColor = ConsoleColor.Black;
                var input = System.Console.ReadLine();
                HandleCommand(input);
            }
            if (mode == 4)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.Write("User : ");
                System.Console.ForegroundColor = ConsoleColor.Black;
                var input = System.Console.ReadLine();
                HandleCommandSafe(input);
            }
            if (mode == 3)
            {
                int X = (int)Sys.MouseManager.X;
                int Y = (int)Sys.MouseManager.Y;
                Pen pen = new Pen(Color.White);
                Pen pena = new Pen(Color.Gray);
                Pen penBlack = new Pen(Color.Black);
                Pen reda = new Pen(Color.Red);
                Pen greena = new Pen(Color.Green);
                Pen bluea = new Pen(Color.Blue);
                Pen yellowa = new Pen(Color.Yellow);
                Pen magnetaa = new Pen(Color.Magenta);

                Pen blueda = new Pen(Color.DarkBlue);
                canvas.Clear(Color.Blue);
                int taskbarHeight = 30; // Высота панели задач

                // Рисуем задний фон панели задач
                canvas.DrawFilledRectangle(pena, 0, 0, 1280, taskbarHeight);

                canvas.DrawString(DateTime.Now.ToString("HH:mm:ss"), Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 1050, 10);
                canvas.DrawString(DateTime.Now.ToString("yyyy-MM-dd"), Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 1150, 10);

                // Рисуем кнопку "Выключение ПК"
                if (dock)
                {
                    canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, blueda, 20, 10);
                    canvas.DrawFilledRectangle(pena, 0, taskbarHeight, 190, taskbarHeight + 120);
                    if (MouseManager.Y < taskbarHeight)
                    {
                        if (MouseManager.X >= 110 && MouseManager.X <= 210)
                        {
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 125, 10);
                        }
                        else if (MouseManager.X >= 215 && MouseManager.X <= 315)
                        {
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        }
                        else
                        {
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                        }
                    }
                    else
                    {
                        canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                    }
                }
                else
                {
                    if (MouseManager.Y < taskbarHeight)
                    {
                        if (MouseManager.X >= 5 && MouseManager.X <= 100)
                        {
                            canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 20, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        }
                        if (MouseManager.X >= 110 && MouseManager.X <= 210)
                        {
                            canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 20, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 125, 10);
                        }
                        else if (MouseManager.X >= 215 && MouseManager.X <= 315)
                        {
                            canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 20, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        }
                        else
                        {
                            canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 20, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                        }
                    }
                    else
                    {
                        canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 20, 10);
                        canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                    }
                }

                // Рисуем кнопку "Перезагрузка ПК"
                //canvas.DrawFilledRectangle(pen, 110, 5, 210, taskbarHeight - 10);


                if (Sys.MouseManager.MouseState == Sys.MouseState.Left)
                {
                    // Проверяем, на какую кнопку нажал пользователь
                    if (MouseManager.Y < taskbarHeight)
                    {
                        if (MouseManager.X >= 5 && MouseManager.X <= 100)
                        {
                            System.Console.Beep(700, 100);
                            dock = !dock;
                        }
                        if (MouseManager.X >= 110 && MouseManager.X <= 210)
                        {
                            Sys.Power.Shutdown();
                        }
                        else if (MouseManager.X >= 215 && MouseManager.X <= 315)
                        {
                            Sys.Power.Reboot();
                        }
                    }
                    else
                    {
                        System.Console.Beep(700, 180);
                    }
                }

                if (Sys.MouseManager.MouseState == Sys.MouseState.Right)
                {
                    System.Console.Beep(380, 180);
                }

                DrawCursor();
                canvas.Display();

            }
            if (mode == 2)
            {
                int X = (int)Sys.MouseManager.X;
                int Y = (int)Sys.MouseManager.Y;
                Pen pen = new Pen(Color.White);
                Pen penBlack = new Pen(Color.Black);
                Pen reda = new Pen(Color.Red);
                Pen pena = new Pen(Color.Gray);
                Pen greena = new Pen(Color.Green);
                Pen bluea = new Pen(Color.Blue);
                Pen yellowa = new Pen(Color.Yellow);
                Pen magnetaa = new Pen(Color.Magenta);

                Pen blueda = new Pen(Color.DarkBlue);
                canvas.Clear(Color.Blue);
                canvas.DrawImage(_wall, 0, 0);
                int taskbarHeight = 30; // Высота панели задач

                // Рисуем задний фон панели задач
                canvas.DrawFilledRectangle(pena, 0, 0, 1280, taskbarHeight);

                canvas.DrawString(DateTime.Now.ToString("HH:mm:ss"), Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 1050, 10);
                canvas.DrawString(DateTime.Now.ToString("yyyy-MM-dd"), Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 1150, 10);

                // Рисуем кнопку "Выключение ПК"
                if (dock)
                {
                    canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, blueda, 20, 10);
                    canvas.DrawFilledRectangle(pena, 0, taskbarHeight, 190, taskbarHeight + 120);
                    if (MouseManager.Y < taskbarHeight)
                    {
                        if (MouseManager.X >= 110 && MouseManager.X <= 210)
                        {
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 125, 10);
                        }
                        else if (MouseManager.X >= 215 && MouseManager.X <= 315)
                        {
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        }
                        else
                        {
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                        }
                    }
                    else
                    {
                        canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                    }
                }
                else
                {
                    if (MouseManager.Y < taskbarHeight)
                    {
                        if (MouseManager.X >= 5 && MouseManager.X <= 100)
                        {
                            canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 20, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        }
                        if (MouseManager.X >= 110 && MouseManager.X <= 210)
                        {
                            canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 20, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 125, 10);
                        }
                        else if (MouseManager.X >= 215 && MouseManager.X <= 315)
                        {
                            canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 20, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, bluea, 230, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        }
                        else
                        {
                            canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 20, 10);
                            canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                            canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                        }
                    }
                    else
                    {
                        canvas.DrawString("Minedos", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 20, 10);
                        canvas.DrawString("Shutdown", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 125, 10);
                        canvas.DrawString("Restart", Sys.Graphics.Fonts.PCScreenFont.Default, penBlack, 230, 10);
                    }
                }

                // Рисуем кнопку "Перезагрузка ПК"
                //canvas.DrawFilledRectangle(pen, 110, 5, 210, taskbarHeight - 10);


                if (Sys.MouseManager.MouseState == Sys.MouseState.Left)
                {
                    // Проверяем, на какую кнопку нажал пользователь
                    if (MouseManager.Y < taskbarHeight)
                    {
                        if (MouseManager.X >= 5 && MouseManager.X <= 100)
                        {
                            System.Console.Beep(700, 100);
                            dock = !dock;
                        }
                        if (MouseManager.X >= 110 && MouseManager.X <= 210)
                        {
                            Sys.Power.Shutdown();
                        }
                        else if (MouseManager.X >= 215 && MouseManager.X <= 315)
                        {
                            Sys.Power.Reboot();
                        }
                    }
                    else
                    {
                        System.Console.Beep(700, 180);
                    }
                }

                if (Sys.MouseManager.MouseState == Sys.MouseState.Right)
                {
                    System.Console.Beep(380, 180);
                }

                DrawCursor();
                canvas.Display();

            }
        }

        private void HandleCommand(string command)
        {
            string filename = "";
            string dirname = "";
            switch (command.ToLower())
            {
                case "shutdown":
                    Sys.Power.Shutdown();
                    break;

                case "reboot":
                    Sys.Power.Reboot();
                    break;

                case "help":
                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                    ShowHelp();
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case "help2":
                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                    ShowHelp2();
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case "time":
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "avtor":
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Avtor : Michail Khodakov");
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Version OS : 2.1");
                    System.Console.WriteLine("Name OS : gcoralOS");
                    break;

                case "date":
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case "timeanddate":
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case "cls":
                    System.Console.Clear();
                    break;

                case "gui":
                    mode += 1;
                    BeforeRun();
                    break;

                case "setscreen":
                    SetScreenCommand(command);
                    break;
                case "gohome":
                    currentdirectory = @"0:\";
                    break;
                case "dir":
                    try
                    {
                        var directory_list = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(currentdirectory);
                        foreach (var directoryEntry in directory_list)
                        {
                            try
                            {
                                var entry_type = directoryEntry.mEntryType;
                                if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                                {
                                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                                    System.Console.WriteLine("| <File>       " + directoryEntry.mName);
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                }
                                if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                                {
                                    System.Console.ForegroundColor = ConsoleColor.Blue;
                                    System.Console.WriteLine("| <Directory>      " + directoryEntry.mName);
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            catch (Exception e)
                            {
                                System.Console.WriteLine("Error: Directory not found");
                                System.Console.WriteLine(e.ToString());
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                        break;
                    }
                    break;
                case "ls":
                    try
                    {
                        var directory_list = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(currentdirectory);
                        foreach (var directoryEntry in directory_list)
                        {
                            try
                            {
                                var entry_type = directoryEntry.mEntryType;
                                if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                                {
                                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                                    System.Console.WriteLine("| <File>       " + directoryEntry.mName);
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                }
                                if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                                {
                                    System.Console.ForegroundColor = ConsoleColor.Blue;
                                    System.Console.WriteLine("| <Directory>      " + directoryEntry.mName);
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            catch (Exception e)
                            {
                                System.Console.WriteLine("Error: Directory not found");
                                System.Console.WriteLine(e.ToString());
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                        break;
                    }
                    break;
                case "cd":
                    currentdirectory = System.Console.ReadLine();
                    break;
                case "test":
                    System.Console.WriteLine("Test second command processor");
                    break;
                case "mkfile":
                    filename = System.Console.ReadLine();
                    fs.CreateFile(currentdirectory + filename);
                    break;
                case "mkdir":
                    dirname = System.Console.ReadLine();
                    fs.CreateDirectory(currentdirectory + dirname);
                    break;
                case "delfile":
                    filename = System.Console.ReadLine();
                    Sys.FileSystem.VFS.VFSManager.DeleteFile(currentdirectory + filename);
                    break;
                case "deldir":
                    dirname = System.Console.ReadLine();
                    Sys.FileSystem.VFS.VFSManager.DeleteFile(currentdirectory + dirname);
                    break;
                default:
                    if (command.StartsWith("echo "))
                    {
                        EchoCommand(command);
                    }
                    else
                    {
                        System.Console.WriteLine("I don't know what is ", command);
                    }
                    break;
            }
        }

        private void HandleCommandSafe(string command)
        {
            switch (command.ToLower())
            {
                case "shutdown":
                    Sys.Power.Shutdown();
                    break;

                case "reboot":
                    Sys.Power.Reboot();
                    break;

                case "help":
                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                    ShowHelp();
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case "help2":
                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                    ShowHelp2();
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case "time":
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "avtor":
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Avtor : Michail Khodakov");
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Version OS : 2.1");
                    System.Console.WriteLine("Name OS : gcoralOS");
                    break;

                case "date":
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case "timeanddate":
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case "cls":
                    System.Console.Clear();
                    break;

                case "gui":
                    mode += 1;
                    BeforeRun();
                    break;

                case "setscreen":
                    SetScreenCommand(command);
                    break;
                default:
                    if (command.StartsWith("echo "))
                    {
                        EchoCommand(command);
                    }
                    else
                    {
                        System.Console.WriteLine("I don't know what is ", command);
                    }
                    break;
            }
        }
        private void KeyLoader(string command)
        {
            switch (command.ToLower())
            {
                case "1":
                    mode =+ 1;
                    BeforeRun();
                    break;
                case "2":
                    mode = +2;
                    BeforeRun();
                    break;
                case "3":
                    mode = +3;
                    BeforeRun();
                    break;
                case "4":
                    mode = +4;
                    BeforeRun();
                    break;
                case "5":
                    mode = +5;
                    BeforeRun();
                    break;
                default:
                        System.Console.WriteLine("I don't know what is ", command);
                    break;
            }
        }
        private void EchoCommand(string command)
        {
            string echoMessage = command.Substring(5); // Remove "echo " from the command
            System.Console.WriteLine(echoMessage);
        }

        private void DrawIcons()
        {
            int iconSize = 40; // Размер каждой иконки
            int spacing = 10; // Расстояние между иконками
            int startX = 330; // Начальная позиция X для первой иконки
            int startY = 657; // Позиция Y (одинакова для всех иконок)
            int maxX = 500; // Максимальный X, до которого должны быть иконки

            // Инициализация цветов иконок, если они еще не установлены
            if (_iconColors == null)
            {
                Random rand = new Random();
                List<Pen> possibleColors = new List<Pen> { new Pen(Color.Red), new Pen(Color.Green), new Pen(Color.Blue), new Pen(Color.Yellow), new Pen(Color.Magenta) };
                _iconColors = new List<Pen>();

                int currentX = startX;
                while (currentX + iconSize <= maxX)
                {
                    Pen randomColor = possibleColors[rand.Next(possibleColors.Count)];
                    _iconColors.Add(randomColor);
                    currentX += iconSize + spacing;
                }
            }

            // Рисование иконок
            int iconIndex = 0;
            for (int currentX = startX; currentX + iconSize <= maxX; currentX += iconSize + spacing)
            {
                Pen iconPen = _iconColors[iconIndex];
                canvas.DrawFilledRectangle(iconPen, currentX, startY, currentX + iconSize, startY + iconSize);
                iconIndex++;
            }
        }



        private void ShowHelp()
        {
            System.Console.WriteLine("Available Commands part 1 :");
            System.Console.WriteLine("  shutdown       - Shuts down the system");
            System.Console.WriteLine("  reboot         - Reboots the system");
            System.Console.WriteLine("  help           - Shows this help message");
            System.Console.WriteLine("  echo [message] - Displays a message");
            System.Console.WriteLine("  time           - Shows the current time");
            System.Console.WriteLine("  date           - Shows the current date");
            System.Console.WriteLine("  timeanddate    - Shows the current time and date");
            System.Console.WriteLine("  avtor          - Who created this OS?");
            System.Console.WriteLine("  cls            - Clear screen");
            System.Console.WriteLine("");
            System.Console.WriteLine("write help2 for get Available Commands part 2");
        }

        private void ShowHelp2()
        {
            System.Console.WriteLine("Available Commands part 2 :");
            System.Console.WriteLine("  gui            - Go to Desktop Mode");
            System.Console.WriteLine("");
        }

        private void SetScreenCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length == 3)
            {
                if (int.TryParse(parts[1], out int newScreenX) && int.TryParse(parts[2], out int newScreenY))
                {
                    ScreenX = newScreenX;
                    ScreenY = newScreenY;
                    System.Console.WriteLine($"Screen size set to {ScreenX}x{ScreenY}");
                }
                else
                {
                    System.Console.WriteLine("Invalid screen size parameters.");
                }
            }
            else
            {
                System.Console.WriteLine("Usage: setscreen <width> <height>");
            }
        }
    }
}
using System;
using System.Text;

namespace Matrix
{
    class Console_Matrix
    {
        // fields
        static Random rand = new Random();
        readonly static int capSpace = 25;
        readonly static int capStart = 65;
        readonly static int normSpace = 25;
        readonly static int normStart = 97;
        readonly static int rusSpace = 63;
        readonly static int rusStart = 1040;
        readonly static int numSpace = 9;
        readonly static int numStart = 48;

        // properties
        static char printCharacter
        {
            get
            {
                int space = rand.Next(4);
                int index;

                switch (space)
                {
                    case 0:
                        index = rand.Next(capSpace);
                        return Convert.ToChar(capStart + index);
                    case 1:
                        index = rand.Next(normSpace);
                        return Convert.ToChar(normStart + index);
                    case 2:
                        index = rand.Next(rusSpace);
                        return Convert.ToChar(rusStart + index);
                    case 3:
                        index = rand.Next(numSpace);
                        return Convert.ToChar(numStart + index);
                    default:
                        return '\u0041';
                }

                // debug, letter A
                //return '\u0041';
            }
        }

        // methods
        [STAThread]
        static void Main()
        {

            if (NativeMethods.AllocConsole())
            {
                System.Diagnostics.Debug.WriteLine("Console allocation successfully!");
                IntPtr stdHandle = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE);
                
                // set the console to Lucida true type
                Console_Font_Helper.SetLucida(stdHandle);

                Console.Title = "Matrix Digital Rain";
                Console.OutputEncoding = Encoding.UTF8;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WindowLeft = Console.WindowTop = 0;
                // pretty unprecise way of fitting in view
                Console.BufferHeight = (Console.LargestWindowHeight - 2);
                Console.BufferWidth = (Console.LargestWindowWidth - 2);
                Console.WindowHeight = (Console.LargestWindowHeight - 2);
                Console.WindowWidth = (Console.LargestWindowWidth - 2);
                Console.WriteLine("Hit Any Key To Continue");
                Console.ReadKey();
                Console.CursorVisible = false;

                int width, height;
                // setup array of starting y values
                int[] y;

                // setup the screen and initial conditions of y
                Initialize(out width, out height, out y);

                // do the Matrix effect
                // every loop all y's get incremented by 1
                while (true)
                    UpdateAllColumns(width, height, y);
            }
            else
                System.Diagnostics.Debug.WriteLine("Console allocation failed!");
        }


        private static void UpdateAllColumns(int width, int height, int[] y)
        {
            int x;
            // draws 3 characters in each x column each time... 
            // a dark green, light green, and a space

            // y is the position on the screen
            // y[x] increments 1 each time so each loop does the same thing but down 1 y value
            for (x = 0; x < width; ++x)
            {
                // the bright green character
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(x, y[x]);
                char a = printCharacter;
                Console.Write(a);

                // the dark green character -  2 positions above the bright green character
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                int temp = y[x] - 2;
                Console.SetCursorPosition(x, inScreenYPosition(temp, height));
                Console.Write(printCharacter);

                // the 'space' - 20 positions above the bright green character
                int temp1 = y[x] - 20;
                Console.SetCursorPosition(x, inScreenYPosition(temp1, height));
                Console.Write(' ');

                // increment y
                y[x] = inScreenYPosition(y[x] + 1, height);
            }

            // F5 to reset, F11 to pause and unpause
            if (Console.KeyAvailable)
            {
                if (Console.ReadKey().Key == ConsoleKey.F5)
                    Initialize(out width, out height, out y);
                if (Console.ReadKey().Key == ConsoleKey.F11)
                    System.Threading.Thread.Sleep(1);
            }

        }

        // Deals with what happens when y position is off screen
        public static int inScreenYPosition(int yPosition, int height)
        {
            if (yPosition < 0)
                return yPosition + height;
            else if (yPosition < height)
                return yPosition;
            else
                return 0;
        }

        // only called once at the start
        private static void Initialize(out int width, out int height, out int[] y)
        {
            height = Console.WindowHeight;
            width = Console.WindowWidth - 1;

            // 209 for me.. starting y positions of bright green characters
            y = new int[width];

            Console.Clear();
            // loops 209 times for me
            for (int x = 0; x < width; ++x)
            {
                // gets random number between 0 and 81
                y[x] = rand.Next(height);
            }
        }
    }
}
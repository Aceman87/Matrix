using System;
using System.Text;

namespace Matrix
{
    class Form_Matrix
    {
        // fields
        static Random rand = new Random();
        static String matrixCode = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヰヱヲ";

        // properties
        static char printCharacter
        {
            get
            {
                int t = rand.Next(10);
                if (t <= 2)
                    // returns a number
                    return (char)('0' + rand.Next(10));
                else if (t <= 4)
                    // small letter
                    return (char)('a' + rand.Next(27));
                else if (t <= 6)
                    // capital letter
                    return (char)('A' + rand.Next(27));
                else
                    return matrixCode[rand.Next(matrixCode.Length)];
            }
        }

        // methods
        static void Main()
        {
            MatrixForm form = new MatrixForm();
            form.DrawString();
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
                Console.Write(printCharacter);

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
    }
}
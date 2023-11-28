using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class RougeGameUI
    {
        public const string Pixel = "██";
        public const string SinglePixel = "█";

        public int Height;
        public int Width;


        public static void DrawUIBars(CreatureBase player, CreatureBase enemy)
        {
            string SinglePixel = "█";

            float barWidth = MathF.Floor((Console.BufferWidth - 1f) / 2f);

            float totalBarWidth = barWidth * 2f;

            //Console.WriteLine(Console.BufferWidth + " " + barWidth + " " + (barWidth*2));

            // player health

            float pixelLength = barWidth / CreatureBase.maxHealth;

            float playerHealthValue = (50 / CreatureBase.maxHealth) * barWidth * pixelLength;
            float enemyHealthValue = (100 / CreatureBase.maxHealth) * barWidth * pixelLength + Console.BufferWidth - barWidth;

            for (int i = 0; i < Console.BufferWidth; i++)
            {
                //Console.WriteLine(i + " " + barWidth + " " + (Console.BufferWidth - barWidth) + " " + (Console.BufferWidth - totalBarWidth) + " " + (Console.BufferWidth - totalBarWidth + barWidth));

                if (playerHealthValue >= i * pixelLength && i <= barWidth)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(SinglePixel);
                }
                else if (i <= barWidth)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(SinglePixel);
                }
                else if (i >= barWidth && i <= Console.BufferWidth - barWidth)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(SinglePixel);
                }
                else if (enemyHealthValue >= i * pixelLength && i >= Console.BufferWidth - barWidth)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(SinglePixel);
                }
                else if (i >= Console.BufferWidth - barWidth)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(SinglePixel);
                }
            }

            //for (int i = 0;)

            // enemy health

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        public static void DrawBar(float value, float total, int startY, int startX, float length)
        {
            Console.SetCursorPosition(startX, startY);

            string SinglePixel = "█";

            float pixelLength = length / total;
            
            float healthValue = (value / total) * length * pixelLength;

            for (int i = 0; i < length; i++)
            {
                
                if (healthValue >= i * pixelLength)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(SinglePixel);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(SinglePixel);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        public static void DrawBar(float value, float total)
        {
            string SinglePixel = "█";

            float consoleWidth = Console.BufferWidth;

            float pixelLength = consoleWidth / total;
            Console.WriteLine(pixelLength);


            float healthValue = (value / total) * consoleWidth * pixelLength;
            Console.WriteLine(healthValue);

            for (int i = 0; i < consoleWidth; i++)
            {

                if (healthValue >= i * pixelLength)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(SinglePixel);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(SinglePixel);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        public static int[,] Initialise()
        {
            int height = Console.BufferHeight;
            int width = Console.BufferWidth;

            Console.WriteLine(height + " " + width);

            return new int[height, width];
        }


        // windows only. cannot use on other platforms.
        public static void ForceConsoleSize(int height, int width)
        {
            Console.SetBufferSize(height, width);
        }

        public static void DrawImageTest(int[,] imageMatrix)
        {
            
            
            //int[,] image =
            //    {
            //    {0,4,4,4,0,0},
            //    {4,4,11,11,0,0},
            //    {4,4,4,4,0,0},
            //    {0,4,0,4,0,0},
            //    {0,4,0,4,0,0},
            //    {0,4,0,4,0,0},
            //    {0,7,6,7,6,0},
            //    {0,7,7,8,7,8}
            //};

            // gets the length of the x axis and loops through it.
            for (int x = 0; x < imageMatrix.GetLength(0); x++)
            {
                // gets the length of the y axis and loops through it.
                for (int y = 0; y < imageMatrix.GetLength(1); y++)
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Enum.GetName(typeof(ConsoleColor), imageMatrix[x, y]));
                    Console.Write(Pixel);

                }
                Console.Write("\n");
            }
        }
    }
}

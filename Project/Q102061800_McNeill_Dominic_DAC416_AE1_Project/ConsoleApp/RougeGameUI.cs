using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        
        public static void DrawUIBars(float barOneValue, float barOneTotal, ConsoleColor barOneColour, float barTwoValue, float barTwoTotal, ConsoleColor barTwoColour, ConsoleColor emptyColour = ConsoleColor.Black, ConsoleColor spacerColour = ConsoleColor.DarkGray)
        {
            /* I have no clue on how this works, there is so much math,
             * my brain hurts.
             */

            //string SinglePixel = "█";

            float spacerSize = 1f;
            float barSegments = 2f;

            float barWidth = MathF.Floor((Console.BufferWidth - spacerSize) / barSegments);

            float totalBarWidth = barWidth * barSegments;

            float barSpacer = Console.BufferWidth - totalBarWidth;

            //Console.WriteLine(Console.BufferWidth + " " + barWidth + " " + (barWidth*2));

            // player health

            float pixelLength = (float)Console.BufferWidth / (Console.BufferWidth - spacerSize);

            float barOnePixelValue = (barOneValue / barOneTotal) * barWidth * pixelLength;
            float barTwoPixelValue = ((barTwoValue / barTwoTotal) * barWidth * pixelLength) + (Console.BufferWidth - barWidth);

            //Console.WriteLine(barWidth + " " + totalBarWidth + " " + pixelLength + " " + (barWidth + barSpacer) + " " + Console.BufferWidth);

            //Console.WriteLine(barOnePixelValue);
            //Console.WriteLine(barTwoPixelValue);

            for (int i = 0; i < Console.BufferWidth; i++)
            {
                // for more flexi, need to redo for bar segments.
                if (barOnePixelValue >= i * pixelLength && i <= barWidth)
                {
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, barOneColour);
                }
                else if (barOnePixelValue < i * pixelLength && i <= barWidth)
                {
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, emptyColour);
                }
                else if (i >= barWidth && i <= barWidth + barSpacer)
                {
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, spacerColour);
                }
                else if (barTwoPixelValue >= i * pixelLength && i >= barWidth + barSpacer)
                {
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, barTwoColour);
                }
                else if (barTwoPixelValue < i * pixelLength && i >= barWidth + barSpacer)
                {
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, emptyColour);
                }
                //Console.Write(i * pixelLength + "/" + enemyHealthValue);
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
            
            float barValue = (value / total) * length * pixelLength;

            for (int i = 0; i < length; i++)
            {
                
                if (barValue >= i * pixelLength)
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


            float barValue = (value / total) * consoleWidth * pixelLength;
            Console.WriteLine(barValue);

            for (int i = 0; i < consoleWidth; i++)
            {

                if (barValue >= i * pixelLength)
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
            //    {-1,4,4,4,-1,0},
            //    {4,4,11,11,0,0},
            //    {4,4,4,4,0,0},
            //    {0,4,0,4,0,0},
            //    {0,4,0,4,0,0},
            //    {0,4,0,4,0,0},
            //    {0,7,6,7,6,0},
            //    {0,7,7,8,7,8}
            //};

            // gets the length of the x axis and loops through it.
            for (int y = 0; y < imageMatrix.GetLength(0); y++)
            {
                // gets the length of the y axis and loops through it.
                for (int x = 0; x < imageMatrix.GetLength(1); x++)
                {
                    if (imageMatrix[y, x] >= 0)
                    {
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Enum.GetName(typeof(ConsoleColor), imageMatrix[y, x]));
                    }
                    else if (imageMatrix[y, x] == -1)
                    {
                        (int cursorX, int cursorY) = Console.GetCursorPosition();

                        Console.SetCursorPosition(cursorX + 1, cursorY);
                        Console.ForegroundColor = Console.BackgroundColor;
                        Console.SetCursorPosition(cursorX, cursorY);
                    }
                    else if (imageMatrix[y, x] == -2)
                    {
                        (int cursorX, int cursorY) = Console.GetCursorPosition();

                        Console.SetCursorPosition(cursorX + 1, cursorY);
                        Console.ForegroundColor = Console.ForegroundColor;
                        Console.SetCursorPosition(cursorX, cursorY);
                    }


                    Console.Write(Pixel);

                }

                Console.Write("\n");
            }
        }
    }
}

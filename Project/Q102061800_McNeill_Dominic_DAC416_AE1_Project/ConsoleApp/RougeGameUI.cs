using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RougeGame
{
    public class RougeGameUI
    {
        public const string Pixel = "██";
        public const string SinglePixel = "█";

        public int Height;
        public int Width;

        // TODO: for more flexi, need to redo for bar segments. not needed for now. mainly due to limitations in the input feilds.

        public static void DrawUIBars(float barOneValue, float barOneTotal, ConsoleColor barOneColour, float barTwoValue, float barTwoTotal, ConsoleColor barTwoColour, float spacerSize = 1, ConsoleColor emptyColour = ConsoleColor.Black, ConsoleColor spacerColour = ConsoleColor.DarkGray)
        {
            /* I have no clue on how this works, there is so much math,
             * my brain hurts. ik
             */

            // keep it hard coded for now.
            float barSegments = 2f;

            float barWidth = MathF.Floor((Console.BufferWidth - spacerSize) / barSegments);

            float totalBarWidth = barWidth * barSegments;

            float barSpacer = Console.BufferWidth - totalBarWidth;

            // can make console.buffer - spacersize into another variable.

            float pixelLength = (float)Console.BufferWidth / (Console.BufferWidth - spacerSize);

            float barOnePixelValue = (barOneValue / barOneTotal) * barWidth * pixelLength;
            //float barTwoPixelValue = ((barTwoValue / barTwoTotal) * barWidth * pixelLength) + (Console.BufferWidth - barWidth);

            float barTwoPixelValue = ((barTwoValue / barTwoTotal) * barWidth * pixelLength);

            barTwoPixelValue = (Console.BufferWidth - barTwoPixelValue);

            for (int i = 0; i < Console.BufferWidth; i++)
            {
                
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
                else if (barTwoPixelValue < i * pixelLength && i >= barWidth + barSpacer)
                {
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, barTwoColour);
                }
                else if (barTwoPixelValue >= i * pixelLength && i >= barWidth + barSpacer)
                {
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, emptyColour);
                }
            }

            // reset the colour to defualt.
            Console.ForegroundColor = ConsoleColor.White;

            // move onto a new line.
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

        // draws a UI bar that scales with the console window.
        public static void DrawBar(float value, float total)
        {
            // used to draw the bar.
            string SinglePixel = "█";

            // just used to remove the Consle.BufferWidth used everywhere.
            float consoleWidth = Console.BufferWidth;

            // Gets the point value represented by each pixel. so 100 pixels / 100 total = 1 per pixel.
            float pixelLength = consoleWidth / total;

            // Converts the value into a fill ammount.
            float barValue = (value / total) * consoleWidth * pixelLength;

            // itterates through the length of the screen.
            for (int i = 0; i < consoleWidth; i++)
            {
                // if the bar fill ammount is higher then the represented value then draw fill.
                // the bar spans the who row. left is 0, and right is the total.
                if (barValue >= i * pixelLength)
                {
                    // sets the color to red.
                    Console.ForegroundColor = ConsoleColor.Red;

                    // draw the pixel.
                    Console.Write(SinglePixel);
                }
                else
                {
                    // set the color to the background.
                    Console.ForegroundColor = Console.BackgroundColor;

                    // draw the pixel.
                    Console.Write(SinglePixel);
                }
            }

            // reset the colour to defualt
            Console.ForegroundColor = ConsoleColor.White;

            // move onto a new line.
            Console.WriteLine("");
        }


        /* Used for 2d lists.
         * This takes in a 2d List and then writes to the console the image.
         * Example:
         *    1,5,2,13
         *    5,1,3,8
         *    1,6,8,9
         *    10,12,4,2
         */
        public static void DrawImage(List<List<int>> imageMatrix)
        {
            // gets the length of the x axis and loops through it.
            for (int y = 0; y < imageMatrix.Count; y++)
            {
                // gets the length of the y axis and loops through it.
                for (int x = 0; x < imageMatrix[y].Count; x++)
                {
                    // if the image int is greater or equal to 0 then its a colour.
                    if (imageMatrix[y][x] >= 0)
                    {
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Enum.GetName(typeof(ConsoleColor), imageMatrix[y][x]));
                    }
                    // if the image int is -1 then its a background colour.
                    else if (imageMatrix[y][x] == -1)
                    {
                        // save the coursor's position for later.
                        (int cursorX, int cursorY) = Console.GetCursorPosition();


                        // move the corsor right one space.
                        Console.SetCursorPosition(cursorX + 1, cursorY);

                        // set the colour of the cursour to the background colour.
                        Console.ForegroundColor = Console.BackgroundColor;

                        // move the corsor back to it's original position.
                        Console.SetCursorPosition(cursorX, cursorY);
                    }
                    // if the iamge int is -2 then its a foreground colour.
                    else if (imageMatrix[y][x] == -2)
                    {
                        // save the coursor's position for later.
                        (int cursorX, int cursorY) = Console.GetCursorPosition();

                        // move the corsor right one space.
                        Console.SetCursorPosition(cursorX + 1, cursorY);

                        // set the colour of the cursour to the background colour.
                        Console.ForegroundColor = Console.ForegroundColor;

                        // move the corsor back to it's original position.
                        Console.SetCursorPosition(cursorX, cursorY);
                    }

                    // draw the pixel.
                    Console.Write(Pixel);

                }

                // start writing in a new line.
                Console.Write("\n");
            }
        }

        // Used for 2d arrays.
        public static void DrawImage(int[,] imageMatrix)
        {

            // gets the length of the x axis and loops through it.
            for (int y = 0; y < imageMatrix.GetLength(0); y++)
            {
                // gets the length of the y axis and loops through it.
                for (int x = 0; x < imageMatrix.GetLength(1); x++)
                {
                    // if the image int is greater or equal to 0 then its a colour.
                    if (imageMatrix[y, x] >= 0)
                    {
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Enum.GetName(typeof(ConsoleColor), imageMatrix[y, x]));
                    }
                    // if the image int is -1 then its a background colour.
                    else if (imageMatrix[y, x] == -1)
                    {
                        // save the coursor's position for later.
                        (int cursorX, int cursorY) = Console.GetCursorPosition();


                        // move the corsor right one space.
                        Console.SetCursorPosition(cursorX + 1, cursorY);

                        // set the colour of the cursour to the background colour.
                        Console.ForegroundColor = Console.BackgroundColor;

                        // move the corsor back to it's original position.
                        Console.SetCursorPosition(cursorX, cursorY);
                    }
                    // if the iamge int is -2 then its a foreground colour.
                    else if (imageMatrix[y, x] == -2)
                    {
                        // save the coursor's position for later.
                        (int cursorX, int cursorY) = Console.GetCursorPosition();

                        // move the corsor right one space.
                        Console.SetCursorPosition(cursorX + 1, cursorY);

                        // set the colour of the cursour to the foreground colour.
                        Console.ForegroundColor = Console.ForegroundColor;

                        // move the corsor back to it's original position.
                        Console.SetCursorPosition(cursorX, cursorY);
                    }

                    // draw the pixel.
                    Console.Write(Pixel);

                }

                // start writing in a new line.
                Console.Write("\n");
            }
        }
    }
}

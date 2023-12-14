using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.Util;

namespace RougeGame.UI
{
    public class RougeGameUI
    {
        // squre pixel. used for images.
        public const string Pixel = "██";
        // single pixel. used for ui.
        public const string SinglePixel = "█";

        
        // TODO: for more flexi, need to redo for bar segments. not needed for now. mainly due to limitations in the input feilds.

        // Draws a split bar with 2 seperate bars that can have differnt values.
        public static void DrawUIBars(float barOneValue, float barOneTotal, ConsoleColor barOneColour, float barTwoValue, float barTwoTotal, ConsoleColor barTwoColour, float spacerSize = 1, ConsoleColor emptyColour = ConsoleColor.Black, ConsoleColor spacerColour = ConsoleColor.DarkGray)
        {
            // how many segments are there. (can adjust this to have multiple bars but rather not do that).
            float barSegments = 2f;

            // the width of one bar.
            float singleBarWidth = MathF.Floor((Console.BufferWidth - spacerSize) / barSegments);

            // the width of both bars combined.
            float totalBarWidth = singleBarWidth * barSegments;

            // the the spacer size.
            float barSpacer = Console.BufferWidth - totalBarWidth;

            // The new pixel length with the spacer.
            float pixelLength = (float)Console.BufferWidth / (Console.BufferWidth - spacerSize);

            // the fill ammount for bar one.
            float barOneFillValue = (barOneValue / barOneTotal) * singleBarWidth * pixelLength;

            // the fill ammount for bar two.
            float barTwoFillValue = ((barTwoValue / barTwoTotal) * singleBarWidth * pixelLength);

            // offset the pixel value.
            barTwoFillValue = (Console.BufferWidth - barTwoFillValue);

            // iterate through each pixel for the row in the console window.
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                // if the bar one fill ammount is higher than the current pixel value and is within the bar single length.
                if (barOneFillValue >= i * pixelLength && i <= singleBarWidth)
                {
                    // draw the pixel with the bar one fill coulour.
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, barOneColour);
                }
                // of the fill ammount is less than the current pixel value and is within the single bar length.
                else if (barOneFillValue < i * pixelLength && i <= singleBarWidth)
                {
                    // draw a pixel with the empty colour.
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, emptyColour);
                }
                // if the pixel is inbatween both bars.
                else if (i >= singleBarWidth && i <= singleBarWidth + barSpacer)
                {
                    // draw the spacer with the space coulour.
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, spacerColour);
                }
                // is the second bar fill ammount is less than the pixel value and is in the second bar.
                else if (barTwoFillValue < i * pixelLength && i >= singleBarWidth + barSpacer)
                {
                    // draw the pixel with the second bar colour.
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, barTwoColour);
                }
                // if the second bar balue is higher than the pixel value and is in the second bar.
                else if (barTwoFillValue >= i * pixelLength && i >= singleBarWidth + barSpacer)
                {
                    // draw the pixel with the empty colour.
                    RougeGameUtil.DisplayTextSameLine(SinglePixel, emptyColour);
                }
            }

            // reset the colour to defualt.
            Console.ForegroundColor = ConsoleColor.White;

            // move onto a new line.
            Console.WriteLine("");
        }


        // draws a UI bar at a specific location.
        public static void DrawBar(float value, float total, int startY, int startX, float length)
        {
            // set the corsor location to the specified location.
            Console.SetCursorPosition(startX, startY);

            // gets the worth for each pixel.
            float pixelLength = length / total;
            
            // gets the bar fill ammount.
            float barValue = (value / total) * length * pixelLength;

            // iterate for each pixel for the console screen row.
            for (int i = 0; i < length; i++)
            {
                // if the bar value is higher than the represented value.
                if (barValue >= i * pixelLength)
                {
                    // set foreground colour to red.
                    Console.ForegroundColor = ConsoleColor.Red;
                    // write single pixel.
                    Console.Write(SinglePixel);
                }
                else
                {
                    // set the foreground colour to white.
                    Console.ForegroundColor = ConsoleColor.White;
                    // draw the single pixel.
                    Console.Write(SinglePixel);
                }
            }

            // set the foreground color back to defualt.
            Console.ForegroundColor = ConsoleColor.White;
            // move the corsor to a new line.
            Console.WriteLine("");
        }

        // draws a UI bar that scales with the console window.
        public static void DrawBar(float value, float total)
        {

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

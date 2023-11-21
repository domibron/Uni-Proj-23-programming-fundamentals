using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame.Image
{
    public class RougeGameDrawImage
    {
        public static void DrawImageTest(int[,] imageMatrix)
        {
            const string pixel = "██";
            
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
                    Console.Write(pixel);

                }
                Console.Write("\n");
            }
        }
    }
}

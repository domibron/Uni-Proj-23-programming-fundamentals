using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class RougeGameFileSystem
    {
        public const string fileSuffix = ".rgimg";

        public const string folderName = "Images";

        public static Dictionary<string, List<List<int>>> Images = new Dictionary<string, List<List<int>>>();

        public static List<string> fileNames = new List<string>();

        public static void LoadAllImages()
        {
            DirectoryInfo d = new DirectoryInfo(folderName);

            FileInfo[] files = d.GetFiles("*" + fileSuffix);

            foreach (FileInfo file in files)
            {
                fileNames.Add(file.Name);
            }

            foreach (string fileName in fileNames)
            {
                List<List<int>>? item = LoadImage(fileName);

                if (item != null)
                {
                    Images.Add(fileName.Replace(fileSuffix, "").ToLower(), item);
                }
            }
        }

        

        public static List<List<int>>? LoadImage(string fileName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(Path.GetFileName(folderName));
            }

            Console.WriteLine(fileName);

            //try
            //{
            FileStream fs = new FileStream(Path.Combine(folderName,fileName), FileMode.OpenOrCreate, FileAccess.Read);

            StreamReader sr = new StreamReader(fs);

            string text = sr.ReadToEnd();

            string[] lineSegments = text.Split("\n");

            List<List<int>> final2DArray = new List<List<int>>();

            for (int iy = 0; iy < lineSegments.Length; iy++)
            {
                // can make smaller.
                final2DArray.Add(new List<int>());
                string[] CharacterSegments = lineSegments[iy].Split(",");

                for (int ix = 0; ix < CharacterSegments.Length; ix++)
                {
                    final2DArray[iy].Add(Convert.ToInt32(CharacterSegments[ix]));
                }
                    
            }

            RougeGameUtil.DisplayText("Operation Successful", ConsoleColor.DarkGreen);

            sr.Close();

            return final2DArray;
            //}
            //catch (Exception e)
            //{
            //    RougeGameUtil.DisplayText($"Operation Failure {e.Message}", ConsoleColor.DarkRed);
            //}
            //finally
            //{
            //    RougeGameUtil.DisplayText("Operation Compleate");
            //}

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.LogSystem;

namespace RougeGame.FileSystem
{
    public class RougeGameFileSystem
    {
        // the suffix for the image file.
        public const string fileSuffix = ".rgimg";

        // the folder name.
        public const string folderName = "Images";

        // the store for all iamges key is the name of the image the value is a 2d list.
        public static Dictionary<string, List<List<int>>> Images = new Dictionary<string, List<List<int>>>();

        // the name of all the files.
        public static List<string> fileNames = new List<string>();

  
        // Loads all the images in the image folder.
        public static void LoadAllImages()
        {
            // the directory (folder) for the images.
            DirectoryInfo directory = new DirectoryInfo(folderName);

            // Gets all files in the image folder.
            FileInfo[] files = directory.GetFiles("*" + fileSuffix);

            // cycles through each file in files.
            foreach (FileInfo file in files)
            {
                //
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

            RougeGameLogSystem.Instance.WriteLine("Operation Successful");

            sr.Close();
            fs.Close();

            return final2DArray;
        }
    }
}

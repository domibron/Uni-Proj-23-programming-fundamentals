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
            // if the directory does not exist then create one.
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(Path.GetFileName(folderName));
            }

            // the directory (folder) for the images.
            DirectoryInfo directory = new DirectoryInfo(folderName);

            // Gets all files in the image folder.
            FileInfo[] files = directory.GetFiles("*" + fileSuffix);

            // cycles through each file in files.
            foreach (FileInfo file in files)
            {
                // adds the file to all file names for use later.
                fileNames.Add(file.Name);
            }

            // cycles through the newly created list of file names.
            foreach (string fileName in fileNames)
            {
                // creats a 2D list and loads the file into the list.
                List<List<int>>? item = LoadImage(fileName);

                // if trhe list is not null.
                if (item != null)
                {
                    // add to the dictionary. Low capitalises the name for consistency.
                    Images.Add(fileName.Replace(fileSuffix, "").ToLower(), item);
                }
            }
        }

        
        // Image loader.
        public static List<List<int>>? LoadImage(string fileName)
        {
            // try to open the file if not create a new file.
            FileStream fs = new FileStream(Path.Combine(folderName,fileName), FileMode.OpenOrCreate, FileAccess.Read);

            // opens stream reader.
            StreamReader sr = new StreamReader(fs);

            // stores the information from the file reader.
            string text = sr.ReadToEnd();

            // slit the text along new lines. This is our y.
            string[] lineSegments = text.Split("\n");

            // create a new 2d list.
            List<List<int>> final2DArray = new List<List<int>>();

            // cycle through the newly splut segments.
            for (int iy = 0; iy < lineSegments.Length; iy++)
            {
                // create a new empty x in the 2d list.
                final2DArray.Add(new List<int>());

                // seperate the x columns.
                string[] CharacterSegments = lineSegments[iy].Split(",");

                // cycle through the x.
                for (int ix = 0; ix < CharacterSegments.Length; ix++)
                {
                    // add the x to the corrisponding y in the 2d list.
                    final2DArray[iy].Add(Convert.ToInt32(CharacterSegments[ix]));
                }
                    
            }

            // log success.
            RougeGameLogSystem.Instance.WriteLine("Operation Successful");

            // close the file steam and reader.
            sr.Close();
            fs.Close();

            // return the 2d array.
            return final2DArray;
        }
    }
}

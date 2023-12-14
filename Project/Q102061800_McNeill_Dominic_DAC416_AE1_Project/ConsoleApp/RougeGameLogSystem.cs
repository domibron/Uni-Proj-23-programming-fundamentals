using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame.LogSystem
{
    public class RougeGameLogSystem
    {
        // used to store the instance.
        private static RougeGameLogSystem _instance;

        // just call this from other scripts, if the instance does not exist then it will create one.
        public static RougeGameLogSystem Instance
        {
            get
            {
                if ( _instance == null)
                {
                    _instance = new RougeGameLogSystem();
                }

                return _instance;
            }
            set // should not really use this. Only this script should set.
            { 
                _instance = value; 
            }
        }

        // the folder's name.
        public const string folderName = "Logs";
        // the log file's suffix.
        public const string suffix = ".log";

        // creates a new log file that is differnt to the rest.
        string currentLogName = $"date_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_time_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_ms_{DateTime.Now.Millisecond}" + suffix;
        //string currentLogName = DateTime.Now.ToString();

        FileStream fs;
        StreamWriter sw;

        public RougeGameLogSystem()
        {
            Initilize();
        }

        public void Initilize()
        {
            // event listener is added for when the application closes. (Not for crashes).
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);

            // if the folder does not exist then create a folder.
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(Path.GetFileName(folderName));
            }

            // can create a error when trying to open a file with a already exsiting file stream.
            try
            {
                fs = new FileStream(Path.Combine(folderName, currentLogName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            catch
            {
                if (fs == null)
                {
                    fs = new FileStream(Path.Combine(folderName, currentLogName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
                }
            }
            
            // steam writer so writing to the file is simple with write line.
            sw = new StreamWriter(fs);

            // marks the start of the log file.
            sw.WriteLine("INIT");
        }

        // A function for other scripts so they can use to write to the log file.
        public void WriteLine(string text)
        {
            sw.WriteLine(text);
        }

        // A function for other scripts so they can use to write on the same line in the log file.
        public void Write(string text)
        {
            sw.Write(text);
        }

        // on exit function for the event.
        public void OnExit(object? sender, EventArgs e)
        {
            // will give a error when debugging and not for the build. just stops the program from erroring on its final miliseconds.
            try
            {
                // writes the last message.
                sw.WriteLine("EXITED!");
                // stop writing.
                sw.Close();
                // close the file and save.
                fs.Close();
            }
            catch
            { }
        }
    }
}

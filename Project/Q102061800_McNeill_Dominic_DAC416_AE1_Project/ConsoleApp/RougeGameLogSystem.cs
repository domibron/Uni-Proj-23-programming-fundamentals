using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame.LogSystem
{
    public class RougeGameLogSystem
    {
        private static RougeGameLogSystem _instance;

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
            set // should not really use this.
            { 
                _instance = value; 
            }
        }

        // the folder's name.
        public const string folderName = "Logs";
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
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(Path.GetFileName(folderName));
            }

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
            
            sw = new StreamWriter(fs);

            sw.WriteLine("INIT");
        }

        public void WriteLine(string text)
        {
            sw.WriteLine(text);
        }

        public void Write(string text)
        {
            sw.Write(text);
        }

        public void OnExit(object? sender, EventArgs e)
        {
            try
            {
                sw.WriteLine("EXITED!");
                sw.Close();
                fs.Close();
            }
            catch
            { }
        }
    }
}

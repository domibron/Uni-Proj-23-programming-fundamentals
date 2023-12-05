using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RougeGame
{
    [System.Serializable]
    public class RougeGameData
    {
        public int gamesPlayed;
        public int gamesWon;
        public int gamesLossed;
        public int score;

        public RougeGameData()
        {
            gamesPlayed = 0;
            gamesWon = 0;
            gamesLossed = 0;
            score = 0;
        }

    }

    [System.Serializable]
    public class RougeGameSaveData
    {
        private static RougeGameSaveData _current;
        public static RougeGameSaveData current
        {
            get
            {
                if (_current == null)
                {
                    _current = new RougeGameSaveData();
                }

                return _current;
            }
            set
            {
                _current = value;
            }
        }

        public RougeGameSaveData()
        {
            rougeGameData = new();
        }

        public RougeGameData rougeGameData;
    }


        public class RougeGameSaveManager
    {
        public static RougeGameSaveManager instance;

        public string saveName = "SAVEDATA";
        public string suffix = ".SAVEDATA";

        public event Action OnSave;
        public void GameSaveInvoke()
        {
            if (OnSave != null)
            {
                OnSave();
            }
        }

        public void Initilize()
        {
            if (instance != null && instance != this)
            {
                //Destroy(this);
            }
            else
            {
                instance = this;
                //DontDestroyOnLoad(this);
            }

            if (RougeGameSerializationManager.Load("saves/" + saveName + suffix) == null)
            {
                Console.WriteLine(RougeGameSerializationManager.Load("saves/" + saveName + suffix));
                RougeGameSerializationManager.Save(saveName, suffix, RougeGameSaveData.current);
            }
            else
            {
                RougeGameSaveData.current = (RougeGameSaveData)RougeGameSerializationManager.Load("saves/" + saveName + suffix);
            }
        }

        public void Save()
        {
            RougeGameSerializationManager.Save(saveName, suffix, RougeGameSaveData.current);
        }

        public RougeGameSaveData Load()
        {
            return (RougeGameSaveData)RougeGameSerializationManager.Load("saves/" + saveName + suffix);
        }

        public class RougeGameSerializationManager
        {
            public static bool Save(string saveName, string saveSuffix, object saveData)
            {
                BinaryFormatter formatter = GetBinaryFormatter();

                if (!Directory.Exists("saves"))
                {
                    Directory.CreateDirectory(Path.GetFileName("saves"));
                }

                Console.WriteLine(Directory.Exists(Path.GetFileName("saves")));

                string path = "saves/" + saveName + saveSuffix;

                FileStream file = File.Create(path);

                formatter.Serialize(file, saveData);

                file.Close();

                return true;
            }

            public static object Load(string path)
            {
                if (!File.Exists(path))
                {
                    return null;
                }

                BinaryFormatter formatter = GetBinaryFormatter();

                FileStream file = File.Open(path, FileMode.Open);

                try
                {
                    object save = formatter.Deserialize(file);
                    file.Close();
                    return save;
                }
                catch
                {
                    RougeGameUtil.DisplayText($"Failed to load file at {path}");
                    file.Close();
                    return null;
                }

            }

            public static BinaryFormatter GetBinaryFormatter()
            {
                BinaryFormatter formatter = new BinaryFormatter();

                SurrogateSelector selector = new SurrogateSelector();

                //Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
                //QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();

                //selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
                //selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

                formatter.SurrogateSelector = selector;

                return formatter;
            }
        }
    }
}

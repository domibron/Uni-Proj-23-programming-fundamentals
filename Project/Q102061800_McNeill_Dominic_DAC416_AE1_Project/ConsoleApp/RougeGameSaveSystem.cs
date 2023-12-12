using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using RougeGame.Util;

namespace RougeGame.SaveSystem
{
    // the data for the game
    [System.Serializable]
    public class RougeGameData
    {
        // all the game stats
        public int gamesPlayed;
        public int gamesWon;
        public int gamesLossed;
        public int score;

        // when new is called this is what the data is set to.
        public RougeGameData()
        {
            gamesPlayed = 0;
            gamesWon = 0;
            gamesLossed = 0;
            score = 0;
        }

    }

    // save data core
    [System.Serializable]
    public class RougeGameSaveData
    {
        // this is for the data, only holds it. Local data.
        private static RougeGameSaveData _current;
        // this is how data is gotten and set.
        public static RougeGameSaveData current
        {
            get
            {
                // if the local data is null.
                if (_current == null)
                {
                    // create new data to the local data.
                    _current = new RougeGameSaveData();
                }

                // return the local data.
                return _current;
            }
            set
            {
                // set the local data to the value given.
                _current = value;
            }
        }

        // this allows the data to create a new instance rathar than a null.
        public RougeGameSaveData()
        {
            // creates new empty values, instead of null values.
            rougeGameData = new();
        }

        // the data used for the score.
        public RougeGameData rougeGameData;
    }


    // the save manager, it controlls saving and loading the save data.
    public class RougeGameSaveManager
    {
        // creates a public access point for other classes and scripts.
        public static RougeGameSaveManager instance;

        // save data name for the file.
        public string saveName = "SAVEDATA";
        // save data suffix used to make the file type.
        public string suffix = ".SAVEDATA";

        // a event so any scripts and classes depended on the save data can know when a save occours.
        // normally used to refresh the local data.
        public event Action OnSave;
        // invokes the save event (triggers the evenmt).
        public void GameSaveInvoke()
        {
            // checks to see if there are any listeners.
            if (OnSave != null)
            {
                // invokes the event.
                OnSave();
            }
        }

        // used to set up the save system, should be called first before anything else.
        public void Initialize()
        {
            // this stops any duplicates as this is called a singleton.
            // checks if there is a instance and the instance is not this.
            if (instance != null && instance != this)
            {
                // do nothing.
            }
            // if no other instances then set this a the instance.
            else
            {
                // set the instance to this.
                instance = this;
            }

            // if the save does not exists. create a new save.
            if (RougeGameSerializationManager.Load("saves/" + saveName + suffix) == null)
            {
                // create a new save.
                RougeGameSerializationManager.Save(saveName, suffix, RougeGameSaveData.current);
            }
            // if there is a insance. load the save data.
            else
            {
                // load the data and store it.
                RougeGameSaveData.current = (RougeGameSaveData)RougeGameSerializationManager.Load("saves/" + saveName + suffix);
            }
        }

        // save the current state of the data.
        public void Save()
        {
            // save the data.
            RougeGameSerializationManager.Save(saveName, suffix, RougeGameSaveData.current);
        }

        // load save data.
        public RougeGameSaveData Load()
        {
            // returns the save data.
            return (RougeGameSaveData)RougeGameSerializationManager.Load("saves/" + saveName + suffix);
        }

        // used to serialize the data (encrypt)
        public class RougeGameSerializationManager
        {
            // Save data.
            public static bool Save(string saveName, string saveSuffix, object saveData)
            {
                // get the formatter we have generated.
                BinaryFormatter formatter = GetBinaryFormatter();

                // if the save folder does not exist.
                if (!Directory.Exists("saves"))
                {
                    // create a new folder called saves.
                    Directory.CreateDirectory(Path.GetFileName("saves"));
                }

                // get the path to the save data.
                string path = "saves/" + saveName + saveSuffix;

                // create (can also write over) a new file for the save data.
                FileStream file = File.Create(path);

                // encrypt the data.
                formatter.Serialize(file, saveData);

                // close the file stream. Very important.
                // stop the application of keeping the file open and may currupt.
                file.Close();

                // return sucessful.
                return true;
            }

            // Load the file
            public static object Load(string path)
            {
                // checks if the file exists.
                if (!File.Exists(path))
                {
                    // if the file does not exsist then return null.
                    return null;
                }

                // get the generated formatter.
                BinaryFormatter formatter = GetBinaryFormatter();

                // open the save file.
                FileStream file = File.Open(path, FileMode.Open);

                // stops the application of crashing because of a file not found exeption.
                try
                {
                    // decrypt the save data and store it.
                    object save = formatter.Deserialize(file);
                    // close the file steam.
                    file.Close();
                    // return the save data.
                    return save;
                }
                catch
                {
                    // display a error message.
                    RougeGameUtil.DisplayText($"Failed to load file at {path}");
                    // close the file steam.
                    file.Close();
                    // return null as the data does not exsits.
                    return null;
                }

            }

            // used to create a formatter (the encription). you can added more sophisticated data types here such as Quaternions.
            public static BinaryFormatter GetBinaryFormatter()
            {
                // create a new formatter.
                BinaryFormatter formatter = new BinaryFormatter();

                // create a new surrogate (used to tell the application how to write and read more complicated data).
                SurrogateSelector selector = new SurrogateSelector();

                // put any surrogates here.

                // set the formatter's surrogate to the new surrogate we have created.
                formatter.SurrogateSelector = selector;

                // return the formatter.
                return formatter;
            }
        }
    }
}

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Systems
{
    public class AppDataSystem
    {
        public static void Save<T>(T data, string fileName)
        {
            var directoryPath = $"{Application.dataPath}/StreamingAssets/" + typeof(T).Name;
            var filePath = directoryPath + "/" + fileName + ".json";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                var fileStream = File.Create(filePath);
                fileStream.Close();
            }

            var serializedData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, serializedData);
        }

        public static T Load<T>(string fileName)
        {
            var filePath = $"{Application.dataPath}/StreamingAssets/{typeof(T).Name}/{fileName}.json";

            if (!File.Exists(filePath))
            {
                T defaultObject = default;
                Save(defaultObject, fileName);
            }

            var serializedData = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<T>(serializedData);
            return data;
        }

        public static List<T> LoadAll<T>()
        {
            var directoryPath = $"{Application.dataPath}/StreamingAssets/{typeof(T).Name}";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                return new List<T>();
            }

            var filePaths = Directory.GetFiles(directoryPath, "*.json");

            var fileDataList = new List<T>();

            foreach (var filePath in filePaths)
            {
                var serializedData = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<T>(serializedData);

                if (!fileDataList.Contains(data))
                {
                    fileDataList.Add(data);
                }
            }

            return fileDataList;
        }
    }
}
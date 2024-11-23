using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FinalGame.Develop.CommonServices.DataManagement
{
    public class LocalDataRepository : IDataRepository
    {
        private const string SaveFileExtension = "json";

        private string FolderPath => Application.persistentDataPath;
        
        private readonly Dictionary<string, string> _dataRepository = new();

        public string Read(string key) => File.ReadAllText(GetFullPathFor(key));

        public void Write(string key, string serializedData) => File.WriteAllText(GetFullPathFor(key), serializedData);

        public void Remove(string key) => File.Delete(GetFullPathFor(key));

        public bool Exists(string key) => File.Exists(GetFullPathFor(key));
            
        private string GetFullPathFor(string key)
            => Path.Combine(FolderPath, key) + "." + SaveFileExtension;
    }
}

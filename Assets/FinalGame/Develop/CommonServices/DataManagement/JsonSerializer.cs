using Newtonsoft.Json;

namespace FinalGame.Develop.CommonServices.DataManagement
{
    public class JsonSerializer : IDataSerializer
    {
        public TData Deserialize<TData>(string serializedData) 
            => JsonConvert.DeserializeObject<TData>(serializedData,new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        
        public string Serialize<TData>(TData data)
            => JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto
            });
    }
}

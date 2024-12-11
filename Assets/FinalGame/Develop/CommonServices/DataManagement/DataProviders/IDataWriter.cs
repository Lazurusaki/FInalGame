namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    public interface IDataWriter<in TData> where TData : ISaveData
    {
        void WriteTo(TData data);
    }
}
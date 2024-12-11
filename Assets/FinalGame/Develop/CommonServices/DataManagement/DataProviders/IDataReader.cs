namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    public interface IDataReader<in TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}

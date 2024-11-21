namespace FinalGame.Develop.CommonServices.LoadingScreen
{
    public interface ILoadingScreen : IService
    {
        public bool IsShown { get; }

        public void Show();
        public void Hide();
    }
}

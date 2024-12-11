namespace FinalGame.Develop.CommonServices.LoadingScreen
{
    public interface ILoadingScreen
    {
        public bool IsShown { get; }

        public void Show();
        public void Hide();
    }
}

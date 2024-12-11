using FinalGame.Develop.DI;

namespace FinalGame.Develop.MainMenu
{
    public interface IMenuCommand
    {
        public void Initialize(DIContainer container);
        public void Execute();
    }
}

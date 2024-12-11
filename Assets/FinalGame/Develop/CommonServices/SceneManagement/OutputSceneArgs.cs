namespace FinalGame.Develop.CommonServices.SceneManagement
{
    public abstract class OutputSceneArgs : IOutputSceneArgs
    {
        public IInputSceneArgs NextSceneInputArgs { get; }

        protected OutputSceneArgs(IInputSceneArgs nextSceneInputArgs) 
            => NextSceneInputArgs = nextSceneInputArgs;
    }

    public class BootstrapSceneOutputArgs : OutputSceneArgs
    {
        public BootstrapSceneOutputArgs(IInputSceneArgs nextSceneInputArgs)  : base (nextSceneInputArgs)
        {
        }
    }
    
    public class GameplaySceneOutputArgs : OutputSceneArgs
    {
        public GameplaySceneOutputArgs(IInputSceneArgs nextSceneInputArgs)  : base (nextSceneInputArgs)
        {
        }
    }
    
    public class MainMenuSceneOutputArgs : OutputSceneArgs
    {
        public MainMenuSceneOutputArgs(IInputSceneArgs nextSceneInputArgs)  : base (nextSceneInputArgs)
        {
        }
    }
}

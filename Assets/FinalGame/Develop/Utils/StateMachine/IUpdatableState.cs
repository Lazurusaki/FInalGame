namespace FinalGame.Develop.Utils.StateMachine
{
    public interface IUpdatableState : IState
    {
        void Update(float deltaTime);
    }
}
namespace FinalGame.Develop.Gameplay.Entities.Behaviors
{
    public interface IEntityUpdate: IEntityBehavior
    {
        void OnUpdate(float deltaTime);
    }
}
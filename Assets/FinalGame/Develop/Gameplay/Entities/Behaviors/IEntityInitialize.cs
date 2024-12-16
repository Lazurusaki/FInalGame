namespace FinalGame.Develop.Gameplay.Entities.Behaviors
{
    public interface IEntityInitialize : IEntityBehavior
    {
        void OnInit(Entity entity);
    }
}
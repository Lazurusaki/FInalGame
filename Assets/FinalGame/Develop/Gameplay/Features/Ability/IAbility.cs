namespace FinalGame.Develop.Gameplay.Features.Ability
{
    public interface IAbility
    {
        string ID { get; }

        void Activate();
    }
}
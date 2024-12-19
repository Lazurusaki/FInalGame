using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Utils.Extensions
{
    public static class EntityExtensions
    {
        public static bool TryTakeDamage(this Entity entity, float damage)
        {
            if (entity.TryGetTakeDamageRequest(out ReactiveEvent<float> damageRequest))
            {
                damageRequest?.Invoke(damage);
                return true;
            }

            return false;
        }
    }
}
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Utils.Extensions
{
    public static class PhysicsExtensions
    {
        public static bool MatchWithLayer(this Collider collider, LayerMask mask)
            => ((1 << collider.gameObject.layer) & mask) != 0;

        public static bool TryGetEntity(this Collider collider, out Entity entity)
        {
            if (collider.TryGetComponent(out entity))
                return true;

            return false;
        }
    }
}
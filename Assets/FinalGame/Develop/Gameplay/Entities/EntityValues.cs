namespace FinalGame.Develop.Gameplay.Entities
{
    public enum EntityValues
    {
        MoveDirection,
        MoveSpeed,
        MoveCondition,
        IsMoving,
        
        RotationDirection,
        RotationSpeed,
        RotationCondition,
        
        SelfTriggerReceiver,
        SelfTriggerDamage,
        
        CharacterController,
        Transform,
        
        Health,
        MaxHealth,
        
        Energy,
        MaxEnergy,
        SpendEnergyEvent,
        SpendEnergyRequest,
        SpendEnergyCondition,
        RestoreEnergyEvent,
        RestoreEnergyCooldown,
        RestoreEnergyCondition,
        
        RadiusAttackDamage,
        RadiusAttackRadius,
        RadiusAttackTrigger,
        RadiusAttackCondition,
        
        TeleportRadius,
        TeleportEnergyCost,
        TeleportCondition,
        TeleportTrigger,
        TeleportStartEvent,
        TeleportEndEvent,
        
        RadialAttackTeleportTrigger,
        
        TakeDamageRequest,
        TakeDamageEvent,
        TakeDamageCondition,
        
        IsDead,
        IsDeathProcess,
        DeathCondition,
        SelfDestroyCondition,
    }
}
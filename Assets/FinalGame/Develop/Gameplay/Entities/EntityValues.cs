﻿namespace FinalGame.Develop.Gameplay.Entities
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
        Rigidbody,
        
        Health,
        MaxHealth,
        
        TakeDamageRequest,
        TakeDamageEvent,
        TakeDamageCondition,
        
        IsDead,
        IsDeathProcess,
        DeathCondition,
        SelfDestroyCondition,
        
        AttackTrigger,
        AttackCondition,
        IsAttackProcess, 
        AttackCancelCondition,
        
        Damage,
        ShootPoint,
        InstantAttackEvent,
        AttackInterval,
        AttackCooldown,
        
        Team,
        
        DetectedEntitiesBuffer,
        
        IsMainHero,
        
        BaseStats,
        ModifiedStats,
        StatsEffectsList,
        AbilityList,
        
        //ADV_03
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
        
        RadialAttackTeleportTrigger
    }
}
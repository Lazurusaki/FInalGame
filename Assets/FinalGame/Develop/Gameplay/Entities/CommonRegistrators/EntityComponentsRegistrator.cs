using System;
using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;
using Object = UnityEngine.Object;


public class EntityComponentsRegistrator : MonoEntityRegistrator
{
    [SerializeField] private List<ComponentData> _components;
    
    public override void Register(Entity entity)
    {
        foreach (var component in _components)
        {
            entity.AddValue(component.EntityValue, component.Component);
        }
    }

    [Serializable]
    private class ComponentData
    {
        [field: SerializeField] public EntityValues EntityValue;
        [field: SerializeField] public Object Component;
    }
}



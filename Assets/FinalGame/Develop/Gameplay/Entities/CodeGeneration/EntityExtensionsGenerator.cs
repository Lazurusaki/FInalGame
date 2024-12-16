using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FinalGame.Develop.Utils.Reactive;
using UnityEditor;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities.CodeGeneration
{
    public static class EntityExtensionsGenerator
    {
        private static Dictionary<EntityValues, Type> _entityValuesType = new Dictionary<EntityValues, Type>()
        {
            {EntityValues.MoveSpeed, typeof(ReactiveVariable<float>) },
            {EntityValues.MoveDirection, typeof(ReactiveVariable<Vector3>) },
            
            {EntityValues.RotationSpeed, typeof(ReactiveVariable<float>) },
            {EntityValues.RotationDirection, typeof(ReactiveVariable<Vector3>) },
            
            {EntityValues.CharacterController, typeof(CharacterController) },
            {EntityValues.Transform, typeof(Transform) }
        };
        
        [InitializeOnLoadMethod]
        [MenuItem("Tools/GenerateEntityExtensions")]

        private static void Generate()
        {
            var path = GetPathToExtensionsFile();

            var writer = new StreamWriter(path);
            
            writer.WriteLine(GetClassHeader());
            writer.WriteLine("{");

            foreach (var entityValueTypePair in _entityValuesType)
            {
                var type = entityValueTypePair.Value.FullName;

                if (entityValueTypePair.Value.IsGenericType)
                {
                    type = type.Substring(0, type.IndexOf('`'));

                    type += "<";

                    for (var i = 0; i < entityValueTypePair.Value.GenericTypeArguments.Length; i++)
                    {
                        type += entityValueTypePair.Value.GenericTypeArguments[i].FullName;

                        if (i != entityValueTypePair.Value.GenericTypeArguments.Length - 1)
                            type += ",";
                    }

                    type += ">";
                }
                
                if (HasEmptyConstructor(entityValueTypePair.Value))
                    writer.WriteLine($"public static {typeof(Entity)} Add{entityValueTypePair.Key}(this {typeof(Entity)} entity) => entity.AddValue({typeof(EntityValues)}.{entityValueTypePair.Key}, new {type}());");
                
                writer.WriteLine($"public static {typeof(Entity)} Add{entityValueTypePair.Key}(this {typeof(Entity)} entity, {type} value) => entity.AddValue({typeof(EntityValues)}.{entityValueTypePair.Key}, value);");
                writer.WriteLine($"public static {type} Get{entityValueTypePair.Key}(this {typeof(Entity)} entity) => entity.GetValue<{type}>({typeof(EntityValues)}.{entityValueTypePair.Key});");
                writer.WriteLine($"public static {typeof(bool)} TryGet{entityValueTypePair.Key}(this {typeof(Entity)} entity, out {type} value) => entity.TryGetValue<{type}>({typeof(EntityValues)}.{entityValueTypePair.Key}, out value);");
            }
            
            writer.WriteLine("}");
            
            writer.Close();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static string GetClassHeader()
            => "public static class EntityExtensionsGenerated";

        private static string GetPathToExtensionsFile()
            => $"{Application.dataPath}/FinalGame/Develop/Gameplay/Entities/CodeGeneration/EntityExtensionsGenerated.cs";

        private static bool HasEmptyConstructor(Type type) =>
            type.IsAbstract == false
            && type.IsSubclassOf(typeof(UnityEngine.Object)) == false
            && type.IsInterface == false
            && type.GetConstructors().Any(c => c.GetParameters().Count() == 0);
    }
}
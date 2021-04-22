using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem_V3
{
    public abstract class GameplayAttribute : ScriptableObject
    {
        public bool Is(GameplayAttribute Other) => Other.GetType().IsAssignableFrom(GetType());
        public bool Is(Type Other) => Other.IsAssignableFrom(GetType());
    }

    [Serializable]
    public struct GameplayAttributeSetEntry
    {
        public GameplayAttribute Attribute;
        public float Value;
    }

    public static class GameplayAttributes
    {
        public static Type FireResistance => typeof(FireResistanceAttribute);
        public static Type Health => typeof(HealthAttribute);
    }

    public class FireResistanceAttribute : GameplayAttribute { }
    public class HealthAttribute : GameplayAttribute { }
}

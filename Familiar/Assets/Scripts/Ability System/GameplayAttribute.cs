﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class GameplayAttribute : ScriptableObject
    {
        public bool Is(GameplayAttribute Other) => Other.GetType().IsAssignableFrom(GetType());
        public bool Is(Type Other) => Other.IsAssignableFrom(GetType());
    }

    [Serializable]
    public struct GameplayAttributeSetEntry
    {
        public GameplayAttribute attribute;
        public float value;
    }

    public static class GameplayAttributes
    {
        /* EXAMPLE */
        public static Type PlayerHealth => typeof(PlayerHealthAttribute);
        public static Type PlayerMana => typeof(PlayerManaAttribute);
        public static Type FireResistance => typeof(FireResistanceAttribute);
    }

    public class FireResistanceAttribute : GameplayAttribute { }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class GameplayTag : ScriptableObject
    {
        public bool Is(GameplayTag Other) => Other.GetType().IsAssignableFrom(GetType());
    }

    public static class GameplayTags
    {
        /* EXAMPLE */
        public static Type ZapAbilityTag => typeof(ZapAbilityTag);
    }
}

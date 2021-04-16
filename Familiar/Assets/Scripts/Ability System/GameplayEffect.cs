using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{ 
    public enum EffectDurationType
    {
        Instant,
        Duration,
        Infinite,
    }
    [CreateAssetMenu()]
    public class GameplayEffect : ScriptableObject
    {
        public GameplayAttribute attribute;
        public float value;

        public float duration;
        public EffectDurationType effectType;

        public List<GameplayTag> appliedTags;
        public List<GameplayTag> blockedByTags;
        //public List<GameplayTag> requiredTags;
    }
}

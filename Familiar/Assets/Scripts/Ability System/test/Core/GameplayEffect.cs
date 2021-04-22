using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem_V3
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
        // TODO: Array?
        public GameplayAttribute Attribute;
        public float Value;

        public float Duration;
        public EffectDurationType EffectType;

        public List<GameplayTag> AppliedTags;
        public List<GameplayTag> BlockedByTags;
        // TODO: RequiredTags;
    }
}

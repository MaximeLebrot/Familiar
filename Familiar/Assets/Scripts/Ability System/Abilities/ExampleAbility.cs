using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class ExampleAbility : GameplayAbility
    {
        public override void Activate(GameplayAbilitySystem Owner)
        {
            //wait anim
            Commit(Owner);
            GameplayAbilitySystem otherAS = null; //hit.getComponent<GameplayAbilitySystem>();
            otherAS.ApplyEffectToSelf(Effect);
        }
    }
}

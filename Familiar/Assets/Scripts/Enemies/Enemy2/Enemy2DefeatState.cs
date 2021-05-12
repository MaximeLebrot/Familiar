using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2DefeatState")]
public class Enemy2DefeatState : Enemy2BaseState
{
    public override void Enter()
    {
        base.Enter();
        Defeated();
    }

    public override void HandleUpdate()
    {

    }

    private void Defeated()
    {
        owner.anim.SetTrigger("spiderDie");
        owner.StartCoroutine(owner.KillAfterAnim());
    }
}

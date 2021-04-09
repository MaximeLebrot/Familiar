using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2DefeatState")]
public class Enemy2DefeatState : Enemy2BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy2 Entered Defeat State");
        Defeated();
    }

    public override void HandleUpdate()
    {
        //Debug.Log("enemy defeated");
    }

    private void Defeated()
    {
        Destroy(owner.gameObject);
    }
}

using UnityEngine;

public class PoisonousDietAbility : Ability
{
    public PoisonousDietAbility(int player) : base(player)
    {
    }

    public override void SelfTrigger()
    {
        HurtEnemy();
        HealSelf();
    }

    public override void EnemyTrigger()
    {
        
    }
}

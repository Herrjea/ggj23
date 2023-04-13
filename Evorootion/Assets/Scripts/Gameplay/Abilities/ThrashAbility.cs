using UnityEngine;


public class ThrashAbility : Ability
{
    public ThrashAbility(int player) : base(player)
    {
    }

    public override void SelfTrigger()
    {
        HurtEnemy(3);
    }

    public override void EnemyTrigger()
    {
        HurtSelf();
    }
}

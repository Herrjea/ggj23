using UnityEngine;

public class SpikesAbility : Ability
{
    public SpikesAbility(int player) : base(player)
    {

    }

    public override void SelfTrigger()
    {
        HurtEnemy(1);
    }

    public override void EnemyTrigger()
    {
        HurtEnemy(1);
    }
}

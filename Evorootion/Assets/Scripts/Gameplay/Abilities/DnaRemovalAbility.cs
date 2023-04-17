using UnityEngine;

public class DnaRemovalAbility : Ability
{
    public DnaRemovalAbility(int player) : base(player)
    {

    }

    public override void SelfTrigger()
    {
        HurtEnemy(2);
    }

    public override void EnemyTrigger()
    {
        HurtSelf(2);
    }
}

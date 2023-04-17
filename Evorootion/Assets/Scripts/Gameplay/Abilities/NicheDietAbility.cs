using UnityEngine;

public class NicheDietAbility : Ability
{
    public NicheDietAbility(int player) : base(player)
    {

    }

    public override void SelfTrigger()
    {
        HealSelf(1);
    }

    public override void EnemyTrigger()
    {
        HealSelf(1);
    }
}

using UnityEngine;

public class GenomeDuplicationAbility : Ability
{
    public GenomeDuplicationAbility(int player) : base(player)
    {

    }

    public override void SelfTrigger()
    {
        HealSelf(3);
    }

    public override void EnemyTrigger()
    {
        HealSelf(1);
    }
}

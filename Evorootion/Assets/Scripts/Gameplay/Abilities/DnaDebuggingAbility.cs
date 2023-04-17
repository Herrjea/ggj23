using UnityEngine;

public class DnaDebuggingAbility : Ability
{
    public DnaDebuggingAbility(int player) : base(player)
    {

    }

    public override void SelfTrigger()
    {
        HealSelf(2);
    }

    public override void EnemyTrigger()
    {
       HealEnemy(2);
    }
}

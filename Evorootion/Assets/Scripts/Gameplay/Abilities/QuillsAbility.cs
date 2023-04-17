using UnityEngine;

public class QuillsAbility : Ability
{
    public QuillsAbility(int player) : base(player)
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

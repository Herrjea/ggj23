using UnityEngine;


public class MicroplasticSludgeAbility : Ability
{
    public MicroplasticSludgeAbility(int player) : base(player)
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

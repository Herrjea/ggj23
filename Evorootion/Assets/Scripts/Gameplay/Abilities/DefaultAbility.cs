using UnityEngine;

//[CreateAssetMenu(
//    menuName = "Abilities/Default",
//    fileName = "DefaultAbility"
//)]
public class DefaultAbility : Ability
{
    public DefaultAbility(int player) : base(player)
    {
    }

    public override void SelfTrigger()
    {
        HealSelf();

        //Debug.Log("DefaultAbility self-triggered");
    }

    public override void EnemyTrigger()
    {
        HurtSelf();

        //Debug.Log("DefaultAbility enemy-triggered");
    }
}

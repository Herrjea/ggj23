using UnityEngine;


public class Ability
{
    protected int player = 0;

    public Ability(int player)
    {
        this.player = player;
    }


    // If the ability's owner triggers it
    public virtual void SelfTrigger() { }

    // If the enemy triggers the ability
    public virtual void EnemyTrigger() { }


    protected void HurtEnemy(int amount = 1)
    {
        if (player == 1)
            GameEvents.P2TakeDamage.Invoke(amount);
        else if (player == 2)
            GameEvents.P1TakeDamage.Invoke(amount);
        else
            Debug.LogError("Unrecognized player number: " + player);
    }

    protected void HurtSelf(int amount = 1)
    {
        if (player == 1)
            GameEvents.P1TakeDamage.Invoke(amount);
        else if (player == 2)
            GameEvents.P2TakeDamage.Invoke(amount);
        else
            Debug.LogError("Unrecognized player number: " + player);
    }

    protected void HealEnemy(int amount = 1)
    {
        if (player == 1)
            GameEvents.P2Heal.Invoke(amount);
        else if (player == 2)
            GameEvents.P1Heal.Invoke(amount);
        else
            Debug.LogError("Unrecognized player number: " + player);
    }

    protected void HealSelf(int amount = 1)
    {
        if (player == 1)
            GameEvents.P1Heal.Invoke(amount);
        else if (player == 2)
            GameEvents.P2Heal.Invoke(amount);
        else
            Debug.LogError("Unrecognized player number: " + player);
    }
}

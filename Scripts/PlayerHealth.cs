using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth

{
    public int Health { get; private set; }

    public PlayerHealth(int health)
    {
        Health = health;
    }

    public bool IsDestroyed
    {
        get { return Health <= 0; }
    }

    public void RemoveHealth(int amount)
    {
        Health -= amount;
    }
}

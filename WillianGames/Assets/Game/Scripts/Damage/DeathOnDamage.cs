using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathOnDamage : MonoBehaviour, IDamageable
{
    public bool IsDead { get; private set;}
    public event Action DeathEvent;
    private void Awake() {
        IsDead = false;
    }
    public void TakeDamage(int damage)
    {
        StartCoroutine(IsDeadTime());
        DeathEvent.Invoke(); // MATA O PLAYER
    }

    IEnumerator IsDeadTime()
    {
        IsDead = true;
        yield return new WaitForSeconds(0.1f);
        IsDead = false;
    }
}

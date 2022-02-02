using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    void TakeDamage(int damage); 
    event Action DeathEvent; // Action -> função void 
    bool IsDead {get;}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zonedeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if(damageable != null){
            damageable.TakeDamage(10);
        }
    }
}

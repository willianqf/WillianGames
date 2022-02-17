using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mellow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 9)
        {
            Animator anim = GetComponent<Animator>();
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            box.enabled = false;
            GameController.instance.SetPontos();
            GameController.instance.SetPontuacao();
            anim.SetBool("Collected", true);
            Destroy(gameObject, 0.8f);
        }
    }
}

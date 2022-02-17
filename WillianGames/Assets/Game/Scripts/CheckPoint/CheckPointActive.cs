using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointActive : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D valor)
    {
        if(valor.name == "Player")
        {
            GameObject particle = transform.GetChild(0).gameObject;
            particle.SetActive(true);
            GameController.instance.Vcam.gameObject.SetActive(false);
            GameController.instance.SetarPontuacao();
        }
    }
}

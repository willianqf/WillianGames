using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform Vcam;
    public Transform Player; //<!--Usando na transição de movimentação da câmera-->
    private void FixedUpdate(){
        if (Player != null){
            Vector3 player = new Vector3(Vcam.position.x, (Vcam.position.y + 2f), Vcam.position.z);
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, 1), player, 0.07f);
        }
    }
    
}

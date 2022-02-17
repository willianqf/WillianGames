using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelowScript : MonoBehaviour
{
    public GameObject Center;
    float cont = 0.4f;
    float valor = 0;
    // Update is called once per frame
    void Update()
    {
        transform.position += (Center.transform.position - transform.position) * 4 * Time.deltaTime;
        valor += Time.deltaTime;
        if(valor > cont)
        {
            Destroy(gameObject);
        }
    }
}

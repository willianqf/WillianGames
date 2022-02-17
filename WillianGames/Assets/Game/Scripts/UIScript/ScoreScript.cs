using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public bool Center = false;

    // Update is called once per frame
    void Update()
    {
        if(Center)
        {
            transform.position += (GameController.instance.Center.transform.position - transform.position) * 4 * Time.deltaTime;
        }
        else
        {
            transform.position += (GameController.instance.PositionScore.transform.position - transform.position) * 4 * Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    void Awake()
    {
        int vidas = PlayerPrefs.GetInt("Vida");
        string pontuacao = PlayerPrefs.GetString("Score");
        GameController.instance.SetVidas(vidas);
        GameController.instance.ScoreValor.text = pontuacao;
    }

}

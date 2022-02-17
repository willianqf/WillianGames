using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int vidas = 5;
    public Transform Vcam;
    public GameObject NPCs;
    public GameObject Finish;
    public GameObject MobileSingle;
    public GameObject Player;
    public GameObject Spawn;
    public static GameController instance;
    public Text textlife;
    public Text ContagemText;
    public GameObject Contagem;
    public GameObject gameover;
    public Text Pontuacao;

    public GameObject melow;
    public GameObject SpawnMelow;

    public GameObject PositionScore;
    public GameObject Score;
    public GameObject Center;
    public Text ScoreValor;

    private int Pontos = 0;
    private Scene scene;
    void Awake()
    {
        instance = this;
        scene = SceneManager.GetActiveScene();
    }
    void Start()
    {
        textlife.text = "x" + vidas.ToString();
    }

    public void SetPontos()
    {
        Pontos++;
    }
    public void AlterScorePontos()
    {

    }
    public void SetPontuacao()
    {
        Pontuacao.text = Pontos.ToString();
    }

    public int GetPontos()
    {
        return Pontos;
    }

    public void SetVidas(int valor)
    {
        vidas = valor;
    }
    public int GetVidas()
    {
        return vidas;
    }
    public void FinishLevel()
    {
        StartCoroutine(TimeOpenCena(1));
    }
    IEnumerator TimeOpenCena(int time)
    {
        yield return new WaitForSeconds(time);
        Finish.SetActive(true);
    }

    public void FollowNewPlayer(GameObject Player)
    {
        CinemachineVirtualCamera CinemachinePlayer = Vcam.gameObject.GetComponent<CinemachineVirtualCamera>();
        CinemachinePlayer.Follow = Player.transform;
    }

    public GameObject CreateNewPlayer()
    {
        GameObject NewPlayer = Instantiate(Player, Spawn.transform.position, Spawn.transform.rotation);
        NewPlayer.transform.position = Spawn.transform.position;
        NewPlayer.name = "Player";
        return NewPlayer;
    }

    public void CheckGameOver(GameObject Player)
    {
        if(vidas > 1)
        {
            if (Player.layer == 9)
            {
                PlayerController valor = Player.GetComponent<PlayerController>();
                valor.enabled = true;
                vidas--;
                Player.SetActive(true);
                Player.transform.position = Spawn.transform.position;
                AtualizarVidas(vidas);
                valor.ReiniciarContagemInvencivel();
                ReiniciarCont();

            }
        }
        else if (Player.layer == 9)
        {
            vidas--;
            AtualizarVidas(vidas);
            GameOver();
        }
    }

    void GameOver()
    {
        gameover.SetActive(true);
        MobileSingle.SetActive(false);
    }

    void AtualizarVidas(int vidas)
    {
        textlife.text = "x"+vidas.ToString();
    }


    float valor = 0;
    float valor1 = 1f;
    float cont = 0;

    void Update()
    {
        if(scene.name != "Intro")
        {
            TelaContagem(4);
        }
    }
    public void TelaContagem(int TimeValor)
    {
        if(cont < TimeValor)
        {
            valor += Time.deltaTime;
            Contagem.SetActive(true);
            if(valor >= valor1)
            {
                cont++;
                if (cont < 3)
                {
                    string val = (3 - Mathf.Round(cont)).ToString();
                    ContagemText.text = val;
                }
                else
                {
                    ContagemText.text = "GO!";
                }
                valor = 0;
            }
        }
        else
        {
            Contagem.SetActive(false);
        }
    }
    public void ReiniciarCont()
    {
        cont = 0;
        ContagemText.text = 3.ToString();
    }

    public void NextLevel()
    {
        int sceneslevel = int.Parse(scene.name[5].ToString()) + 1;
        string newlevel = "Level" + sceneslevel;
        SceneManager.LoadScene(newlevel);

    }

    public void IniciarCena()
    {
        SceneManager.LoadScene("Level1");
    }
    public void VoltarMenu()
    {  
        SceneManager.LoadScene("Intro");
    }

    public void SetarPontuacao()
    {
        SetarPosicaoPontuacao(true);
        StartCoroutine(ValorScoreIncrement(int.Parse(ScoreValor.text), Pontos, ScoreValor));
    }

    public void SetarPosicaoPontuacao(bool valor)
    {
        if(valor)
        {
            ScoreScript x = Score.GetComponent<ScoreScript>();
            x.Center = true;
        }
        else
        {
            ScoreScript x = Score.GetComponent<ScoreScript>();
            x.Center = false;
        }
    }

    IEnumerator ValorScoreIncrement(int PontosTotais, int PontosFaseAtual, Text Valor)
    {
        int ValorAtual = PontosTotais;
        int QuantAIncrementar = ValorAtual + PontosFaseAtual;
        float time = 0.05f;
        MobileSingle.SetActive(false);
        for(int x = ValorAtual; x <= QuantAIncrementar; x++){ 
            int valornow = x;
            if (valornow < 10)
            {
               Valor.text = "0000" + x.ToString();
               GameObject mel = Instantiate(melow, SpawnMelow.transform);
               mel.transform.position = SpawnMelow.transform.position;
               mel.SetActive(true);
               if (Pontos != 0)
               {
                    Pontos--;
                    SetPontuacao();
               }
               yield return new WaitForSeconds(time);
            }else if (valornow < 100)
            {
               GameObject mel = Instantiate(melow, SpawnMelow.transform);
               mel.SetActive(true);
               mel.transform.position = SpawnMelow.transform.position;
                Valor.text = "000" + x.ToString();
                if (Pontos != 0)
               {
                    Pontos--;
                    SetPontuacao();
               }
                yield return new WaitForSeconds(time);
            }else if (valornow < 1000)
            {
               GameObject mel = Instantiate(melow, SpawnMelow.transform);
               mel.SetActive(true);
               mel.transform.position = SpawnMelow.transform.position;
                Valor.text = "00" + x.ToString();
                if (Pontos != 0)
               {
                    Pontos--;
                    SetPontuacao();
               }
                yield return new WaitForSeconds(time);
            }else if (valornow < 10000){
               GameObject mel = Instantiate(melow, SpawnMelow.transform);
               mel.SetActive(true);
               mel.transform.position = SpawnMelow.transform.position;
                Valor.text = "0" + x.ToString();
                if (Pontos != 0)
               {
                    Pontos--;
                    SetPontuacao();
               }
                yield return new WaitForSeconds(time);
            }else{
               GameObject mel = Instantiate(melow, SpawnMelow.transform);
               mel.SetActive(true);
               mel.transform.position = SpawnMelow.transform.position;
                Valor.text = x.ToString();
                if (Pontos != 0)
               {
                    Pontos--;
                    SetPontuacao();
               }
                yield return new WaitForSeconds(time);
            }
        }
        yield return new WaitForSeconds(1);
        SetarPosicaoPontuacao(false);
        FinishLevel();
        PlayerPrefs.SetString("Score", ScoreValor.text);
        PlayerPrefs.SetInt("Vida", vidas);
    }


}
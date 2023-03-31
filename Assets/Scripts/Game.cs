using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    //Equipes
    int _scoreEquipeUne;
    int _scoreEquipeDeux;
    string _nomEquipeUne;
    string _nomEquipeDeux;

    //Scoreboards
    public GameObject _scoreboardEquipe1_1;
    public GameObject _scoreboardEquipe1_2;
    public GameObject _scoreboardEquipe2_1;
    public GameObject _scoreboardEquipe2_2;

    public GameObject _spawnPlayer;
    public GameObject _player;

    public GameObject _spawnSouafle;
    public GameObject _souafle;

    TMP_Text _scoreboardTextEquipe1_1;
    TMP_Text _scoreboardTextEquipe1_2;
    TMP_Text _scoreboardTextEquipe2_1;
    TMP_Text _scoreboardTextEquipe2_2;

    //Timer de jeu
    float _timer;
    public GameObject _timerObject1;
    public GameObject _timerObject2;
    TMP_Text _timerText1;
    TMP_Text _timerText2;

    // Start is called before the first frame update
    void Start()
    {
        //Equipes
        _scoreEquipeUne = 0;
        _scoreEquipeDeux = 0;

        //Scoreboard
        _scoreboardTextEquipe1_1 = _scoreboardEquipe1_1.GetComponent<TMP_Text>();
        _scoreboardTextEquipe1_2 = _scoreboardEquipe1_2.GetComponent<TMP_Text>();
        _scoreboardTextEquipe2_1 = _scoreboardEquipe2_1.GetComponent<TMP_Text>();
        _scoreboardTextEquipe2_2 = _scoreboardEquipe2_2.GetComponent<TMP_Text>();
        
        _scoreboardTextEquipe1_1.text = _scoreboardTextEquipe1_2.text = _scoreEquipeUne.ToString();
        _scoreboardTextEquipe2_1.text = _scoreboardTextEquipe2_2.text = _scoreEquipeDeux.ToString();

        //Temps de jeu
        _timer = 0;
        _timerText1 = _timerObject1.GetComponent<TMP_Text>();
        _timerText2 = _timerObject2.GetComponent<TMP_Text>();
        _timerText1.text = _timer.ToString();
        _timerText2.text = _timer.ToString();

        //Equipes de jeu
        _nomEquipeUne = "Equipe 1";
        _nomEquipeDeux = "Equipe 2";
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        int minutes = (int) _timer / 60;
        int secondes = (int) _timer % 60;

        _timerText1.text = minutes + ":" + secondes;
        _timerText2.text = minutes + ":" + secondes;

        _scoreboardTextEquipe1_1.text = _scoreboardTextEquipe1_2.text = _scoreEquipeUne.ToString();
        _scoreboardTextEquipe2_1.text = _scoreboardTextEquipe2_2.text = _scoreEquipeDeux.ToString();
    }

    public void incrementScore(int equipe, int points)
    {
        if(equipe == 1)
        {
            _scoreEquipeUne += points;
        }
        else
        {
            _scoreEquipeDeux += points;
        }
        Debug.Log("Equipe " + equipe + "gagne " + points + " points !");
        Debug.Log("Le score est maintenant de : " + _scoreEquipeUne + " : " + _scoreEquipeDeux);

        _player.GetComponent<Transform>().position = _spawnPlayer.GetComponent<Transform>().position;
        _souafle.GetComponent<Transform>().position = _spawnSouafle.GetComponent<Transform>().position;

    }

    public void getVifOr()
    {
        _scoreEquipeUne += 150;
    }
}

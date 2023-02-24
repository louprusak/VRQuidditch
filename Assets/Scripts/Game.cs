using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    int _scoreEquipeUne;
    int _scoreEquipeDeux;
    double _timer;
    string _nomEquipeUne;
    string _nomEquipeDeux;

    // Start is called before the first frame update
    void Start()
    {
        _scoreEquipeUne = 0;
        _scoreEquipeDeux = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}

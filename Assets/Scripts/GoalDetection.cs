using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetection : MonoBehaviour
{

    private GameObject _partie = null;
    private Game _scriptPartie = null;

    // Start is called before the first frame update
    void Start()
    {
        _partie = GameObject.Find("Partie");
        _scriptPartie = _partie.GetComponent<Game>();

        //Faire en sorte que le but sache de quelle equipe il est
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision but");
        Debug.Log(other.name);
        if (other.GetComponent<Transform>().gameObject.name == "Souafle")
        {
            Debug.Log("entré");
            int equipe;
            equipe = this.CompareTag("ButDroite") ? 1 : 2;
            _scriptPartie.incrementScore(equipe, 10);
        }
    }
}

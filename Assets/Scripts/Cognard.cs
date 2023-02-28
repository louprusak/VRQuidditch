using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cognard : MonoBehaviour
{
    private GameObject _player;
    private Transform _playerTrans;
    private Transform _monTrans;
    public float _dureeSmooth = 0.5f;
    private Vector3 _velocite = Vector3.zero;
    public bool _activOrbite = true;
    private Vector3 _ciblePos;
    private Vector3 _playerPos;
    private Vector3 _dirDepuisJoueur;
    private Vector3 _randomDirection = Vector3.up;
    public float _rotaRandAmp = 0.1f;
    public float _rotaVitesse = 150f;
    private float _distCible = 5f;
    private float _distCibleVar = 0.1f;
    public float _vitesseMaxSuivi = 10f;
    
    void Start()
    {
        _monTrans = GetComponent<Transform>();
        _player = GameObject.FindWithTag("MainCamera"); // Changer en GameObject.Find("Player")
        _playerTrans = _player.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (_activOrbite) // Joueur actif, je tourne autour (à ~_distCible mètres)
        {
            _distCible = Mathf.Clamp(_distCible + Random.Range(-_distCibleVar, _distCibleVar), 4.5f, 5.5f);
        }
        else // Joueur immobile, je lui fonce dessus
        {
            _distCible--;
        }
        
        _playerPos = _playerTrans.position;
        _dirDepuisJoueur = _monTrans.position - _playerPos;
        _dirDepuisJoueur.Normalize();
        _ciblePos = _playerPos + (_dirDepuisJoueur) * _distCible;
        float randomDirDeltaX = Random.Range(-_rotaRandAmp, _rotaRandAmp);
        float randomDirDeltaY = Random.Range(-_rotaRandAmp, _rotaRandAmp);
        float randomDirDeltaZ = Random.Range(-_rotaRandAmp, _rotaRandAmp);
        //_randomDirection = Quaternion.Euler(randomDirDeltaX, randomDirDeltaY, randomDirDeltaZ) * _randomDirection;
        _randomDirection += new Vector3(randomDirDeltaX, randomDirDeltaY, randomDirDeltaZ);
        _randomDirection.Normalize();
        _monTrans.RotateAround(_playerPos, _randomDirection, _rotaVitesse * Time.deltaTime);
        //Debug.DrawLine(_playerPos, _randomDirection + _playerPos);
        //Debug.Log("_distCible : " + _distCible + "\nDistance : " + Vector3.Distance(_monTrans.position, _playerPos));
        _monTrans.position = Vector3.SmoothDamp(_monTrans.position, _ciblePos, ref _velocite, _dureeSmooth, _vitesseMaxSuivi);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            _activOrbite = true;
        }
    }
}
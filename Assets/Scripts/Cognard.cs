using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cognard : MonoBehaviour
{
    private GameObject _player;
    private Transform _playerTrans;
    private Vector3 _playerPos;
    private Vector3 _playerLastPos;
    private Transform _monTrans;
    public bool _activOrbite = true;
    private Vector3 _dirDepuisJoueur;
    private Vector3 _ciblePos;
    private Vector3 _randomDirection = Vector3.up;
    private Vector3 _velocite;
    private float _dureeSmooth = 0.5f;
    private float _rotaRandAmp = 0.1f;
    private float _rotaVitesse = 100f;
    private float _distCible = 5f;
    private float _vitesseMaxSuivi = 20f;
    private const float _maxIdlePlayerTime = 60f;
    private float _idlePlayerTimer = 0f;
    private float _hitLagTimer = 0f;
    
    void Start()
    {
        _monTrans = GetComponent<Transform>();
        //_player = GameObject.FindWithTag("MainCamera");
        _player = GameObject.FindWithTag("PlayerBody"); // Il faut tag BodyCollider et HeadCollider avec PlayerBody
        _playerTrans = _player.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        _playerPos = _playerTrans.position;
        if (_activOrbite) // Joueur actif, je tourne autour (à _distCible mètres)
        {
            _distCible = 5f * (1 - (_idlePlayerTimer / (5f * _maxIdlePlayerTime)));
            _dureeSmooth = 0.5f;
            _dirDepuisJoueur = _monTrans.position - _playerPos;
            _dirDepuisJoueur.Normalize();
            _ciblePos = _playerPos + (_dirDepuisJoueur) * _distCible;
        }
        else // Joueur immobile, je lui fonce dessus
        {
            _ciblePos = _playerPos;
            _dureeSmooth = 0.1f;
        }

        _monTrans.position = Vector3.SmoothDamp(_monTrans.position, _ciblePos, ref _velocite, _dureeSmooth, _vitesseMaxSuivi);
        
        _rotaVitesse = 100f * (1 + (1.5f * _idlePlayerTimer / _maxIdlePlayerTime));
        float randomDirDeltaX = Random.Range(-_rotaRandAmp, _rotaRandAmp);
        float randomDirDeltaY = Random.Range(-_rotaRandAmp, _rotaRandAmp);
        float randomDirDeltaZ = Random.Range(-_rotaRandAmp, _rotaRandAmp);
        //_randomDirection = Quaternion.Euler(randomDirDeltaX, randomDirDeltaY, randomDirDeltaZ) * _randomDirection;
        _randomDirection += new Vector3(randomDirDeltaX, randomDirDeltaY, randomDirDeltaZ);
        _randomDirection.Normalize();
        _monTrans.RotateAround(_playerPos, _randomDirection, _rotaVitesse * Time.deltaTime);
        //Debug.DrawLine(_playerPos, _randomDirection + _playerPos);
        //Debug.DrawLine(_playerPos, _monTrans.position);
        //Debug.Log("_idlePlayerTimer = " + _idlePlayerTimer + "\t _activOrbite = " + (_activOrbite ? "1" : "0") + "\t _distCible = " + _distCible + "\t _rotaVitesse = " + _rotaVitesse + "\t distance = " + Vector3.Distance(_monTrans.position, _playerPos) + "\t distance à cible = " + Vector3.Distance(_monTrans.position, _ciblePos) + "\t _hitLagTimer = " + _hitLagTimer);
    }

    void Update()
    {
        bool isPlayerMoving = (_playerPos != _playerLastPos); // On regarde si le joueur a bougé depuis la dernière frame
        if (isPlayerMoving) _idlePlayerTimer = 0f; // Si oui, on met le timer  d'inactivité à zéro
        else // Si non...
        {
            if (_hitLagTimer > 0) // ... on vérifie si le cognard est en hitlag (il a frappé le joueur il y a peu)
            {
                // Si oui, on fait descendre le timer petit à petit et si le joueur a bougé pendant que le cognard l'a chassé, on met le timer à zéro
                _hitLagTimer -= (_idlePlayerTimer < 10f ? _hitLagTimer : Time.deltaTime);
            }
            else
            {
                // Si non, on regarde si le joueur a dépassé la limite d'inactivité
                if ((_idlePlayerTimer > _maxIdlePlayerTime) && _activOrbite) {
                    _activOrbite = false; // Si oui, le cognard le pourchasse
                    Debug.Log("À l'attaque !");
                }
                else if (_activOrbite) _idlePlayerTimer += Time.deltaTime; // Si non, on compte la durée de l'inactivité
            }
        }
        
        _playerLastPos = _playerPos;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            Debug.Log(other.gameObject.ToString());
            _activOrbite = true;
            _hitLagTimer = _maxIdlePlayerTime / 4f;
        }
    }
}
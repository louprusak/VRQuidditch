using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VifOr : MonoBehaviour
{


    private GameObject _player;
    private Transform _playerTrans;
    private Vector3 _playerPos;
    private Vector3 _dirDepuisJoueur;
    
    private Transform _monTrans;
    private Vector3 _ciblePos;
    private Vector3 _randomDirection = Vector3.up;
    private Vector3 _randomRotation;
    private Vector3 _velocite;
    private float _dureeSmooth = 0.5f;
    private float _randAmp = 90f;
    private float _distFromPlayer;
    private float _vitesse;

    void Start()
    {
        _monTrans = GetComponent<Transform>();
        _player = GameObject.FindWithTag("PlayerBody"); // Il faut tag BodyCollider et HeadCollider avec PlayerBody
        _playerTrans = _player.GetComponent<Transform>();
        
    }

    void FixedUpdate()
    {
        _playerPos = _playerTrans.position;
        _dirDepuisJoueur = _monTrans.position - _playerPos;
        _distFromPlayer = _dirDepuisJoueur.magnitude;
        _vitesse = 1f + (1 * (5 - Mathf.Clamp(_distFromPlayer, 0, 5)));
        
        float randomDirDeltaX = Random.Range(-_randAmp, _randAmp);
        float randomDirDeltaY = Random.Range(-_randAmp, _randAmp);
        float randomDirDeltaZ = Random.Range(-_randAmp, _randAmp);
        _randomRotation = new Vector3(randomDirDeltaX, randomDirDeltaY, randomDirDeltaZ);
        _randomRotation.Normalize();
        
        if (_monTrans.position.y < 1f) _randomRotation += Vector3.up;
        else if (_monTrans.position.y > 50f) _randomRotation += Vector3.down;
        
        if (_monTrans.position.x < -50f) _randomRotation += Vector3.right;
        else if (_monTrans.position.x > 50f) _randomRotation += Vector3.left;
        
        if (_monTrans.position.z < -50f) _randomRotation += Vector3.forward;
        else if (_monTrans.position.z > 50f) _randomRotation += Vector3.back;

        _randomDirection = Vector3.RotateTowards(_randomDirection, _randomRotation, Mathf.PI / 45f, 0f);

        _ciblePos = _monTrans.position + _randomDirection * _vitesse;
        _monTrans.position = Vector3.SmoothDamp(_monTrans.position, _ciblePos, ref _velocite, _dureeSmooth, 50f);
     
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            Debug.Log(other.gameObject.ToString());
        }
    }


    public void destroy()
    {
        gameObject.SetActive(false);   
    }

}
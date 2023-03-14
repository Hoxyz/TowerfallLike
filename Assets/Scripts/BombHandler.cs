using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class BombHandler : MonoBehaviour {
    [SerializeField] private string id;
    [SerializeField] private KeyCode throwKey;
    [SerializeField] private float handleBombCooldown;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    private bool _handlingBomb;
    private BombController _handledBomb;
    private PlayerController _playerController;
    private float _currentHandleBombCooldown;

    private void Update() {
        _currentHandleBombCooldown -= Time.deltaTime;
        
        if (_handlingBomb && _handledBomb != null) {
            _handledBomb.gameObject.transform.position = transform.position;
            if (Input.GetKey(throwKey)) {
                _playerController.Stop();
            }
            
            if (Input.GetKeyUp(throwKey)) {
                _handledBomb.Throw(new Vector2(1f, 3f));
                _handlingBomb = false;
                _handledBomb = null;
                _playerController.Move();
            }
        }
    }

    private void Awake() {
        _handlingBomb = false;
        _handledBomb = null;
        _playerController = GetComponent<PlayerController>();
        _currentHandleBombCooldown = handleBombCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bomb") && !_handlingBomb && _currentHandleBombCooldown < 0f) {
            other.gameObject.GetComponent<BombController>().PickUp(id);
            _handlingBomb = true;
            _handledBomb = other.gameObject.GetComponent<BombController>();
            _currentHandleBombCooldown = handleBombCooldown;
        }
    }
}

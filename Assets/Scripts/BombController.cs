using System;
using UnityEngine;

public class BombController : MonoBehaviour {
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private float resetTimerTime;
    [SerializeField]
    private float explodeTimerTime;
    
    private Rigidbody2D _rb;
    private int _level;
    private string _lastThrower;

    private void Update() {
        explodeTimerTime -= Time.deltaTime;
        if (explodeTimerTime < 0f) {
            Debug.Log("BOOM");
        }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _level = 1;
        _lastThrower = "";
    }

    public void Throw(Vector2 direction) {
        _rb.AddForce(direction.normalized * throwForce, ForceMode2D.Impulse);
    }

    public void PickUp(string id) {
        if (_lastThrower == id) return;

        _lastThrower = id;
        Mathf.Clamp(_level++, 1, 3);
        explodeTimerTime = resetTimerTime;
    }
}

using TarodevController;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private KeyCode throwKey;
    [SerializeField] private float handleBombCooldown;
    [SerializeField] private Transform facingIndicator;
    private float _currentHandleBombCooldown;
    private BombController _handledBomb;
    private bool _handlingBomb;
    private PlayerController _playerController;

    private void Awake()
    {
        _handlingBomb = false;
        _handledBomb = null;
        _playerController = GetComponent<PlayerController>();
        _currentHandleBombCooldown = handleBombCooldown;
    }

    private void Update()
    {
        facingIndicator.localPosition = new Vector2(Input.GetAxisRaw("Horizontal") * 0.3f, 0f);
        _currentHandleBombCooldown -= Time.deltaTime;
        if (_handlingBomb && _handledBomb != null)
        {
            _handledBomb.gameObject.transform.position = transform.position;
            if (Input.GetKey(throwKey)) _playerController.Stop();

            if (Input.GetKeyUp(throwKey))
            {
                _currentHandleBombCooldown = handleBombCooldown;
                _handledBomb.Throw(new Vector2(Input.GetAxisRaw("Horizontal"), 1f));
                _handlingBomb = false;
                _handledBomb = null;
                _playerController.Move();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb") && !_handlingBomb && _currentHandleBombCooldown < 0f)
        {
            other.gameObject.GetComponent<BombController>().PickUp(id);
            _handlingBomb = true;
            _handledBomb = other.gameObject.GetComponent<BombController>();
        }
    }
}
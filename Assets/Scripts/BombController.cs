using TMPro;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float throwForce;

    [SerializeField] private float resetTimerTime;

    [SerializeField] private float explodeTimerTime;
    [SerializeField] private TextMeshProUGUI text;

    private string _lastThrower;
    private int _level;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _level = 1;
        _lastThrower = "";
    }

    private void Update()
    {
        explodeTimerTime -= Time.deltaTime;
        if (explodeTimerTime < 0f) Debug.Log("BOOM");
    }

    public void Throw(Vector2 direction)
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(direction.normalized * throwForce, ForceMode2D.Impulse);
    }

    public void PickUp(string id)
    {
        // if (_lastThrower == id) return;

        _lastThrower = id;
        _level = Mathf.Clamp(_level + 1, 1, 3);
        explodeTimerTime = resetTimerTime;
        text.text = _level.ToString();
    }
}
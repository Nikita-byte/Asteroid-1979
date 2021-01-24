using UnityEngine;


public sealed class Destroyer : MonoBehaviour
{
    private float _highPos;
    private float _angle;
    private float _speed;
    private float _coolDownOfTurn;
    private float _currentCooldownTurn;
    public bool IsAlive { get; set; }

    private void Awake()
    {
        _coolDownOfTurn = GameSettings.Instance.CoolDownOfTurnDestroyer;
        _currentCooldownTurn = _coolDownOfTurn;
        _speed = GameSettings.Instance.SpeedOfDestroyer;
    }

    public void Move()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (_currentCooldownTurn <= 0)
        {
            _currentCooldownTurn = _coolDownOfTurn;
            transform.Rotate(Vector3.forward * Random.Range(-GameSettings.Instance.AngleOfTurnDestroyer / 2,
            GameSettings.Instance.AngleOfTurnDestroyer / 2));
        }

        _currentCooldownTurn -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser temp = collision.gameObject.GetComponent<Laser>();

        if (temp != null)
        {
            IsAlive = false;
        }
    }

}

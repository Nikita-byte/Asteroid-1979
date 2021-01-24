using UnityEngine;
using System;


public class Cruiser : MonoBehaviour
{
    [SerializeField] private GameObject _shield;

    private float _speed;
    private float _maxSpeed;
    private float _acceleration;
    private float _deceleration;
    private float _highChangePos;
    private float _widthChangePos;
    private bool _isGodMode;
    private ShipState _shipState;

    public SpriteRenderer _shieldSprite;
    public Action HitEventHandler = delegate { };

    private void Start()
    {
        _shieldSprite = _shield.GetComponent<SpriteRenderer>();
        _speed = 0;
        _acceleration = GameSettings.Instance.Acceleration;
        _deceleration = GameSettings.Instance.Deceleration;
        _maxSpeed = GameSettings.Instance.MaxSpeedOfShip;
        _highChangePos = GameSettings.Instance.High - GameSettings.Instance.High / GameSettings.Instance.Indent;
        _widthChangePos = GameSettings.Instance.Width - GameSettings.Instance.Width / GameSettings.Instance.Indent;
    }

    public void Move(Vector3 direction)
    {
        switch (_shipState)
        {
            case ShipState.Acceleration:

                if (_speed >= _maxSpeed)
                {
                    _speed = _maxSpeed;
                }
                else
                {
                    _speed += _acceleration;
                }

                break;

            case ShipState.Deceleration:

                _speed -= _deceleration;

                if (_speed <= 0)
                {
                    _speed = 0;
                }

                break;
        }

        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    public void ChangeState(ShipState shipState)
    {
        _shipState = shipState;
    }

    public void StartPosition()
    {
        transform.position = Vector3.zero;
        _speed = 0;
    }

    public void TurnOnShield()
    {
        _shield.SetActive(true);
    }

    public void TurnOffShield()
    {
        _shield.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SideOfGameZone sideOfGameZone;
        collision.TryGetComponent<SideOfGameZone>(out sideOfGameZone);

        if (sideOfGameZone != null)
        {
            if (sideOfGameZone.TypeOfGameZone == TypeOfGameZone.Top)
            {
                gameObject.transform.position += Vector3.up * - _highChangePos;
            }
            else if (sideOfGameZone.TypeOfGameZone == TypeOfGameZone.Bottom)
            {
                gameObject.transform.position += Vector3.up * _highChangePos;
            }
            else if (sideOfGameZone.TypeOfGameZone == TypeOfGameZone.Left)
            {
                gameObject.transform.position += Vector3.right * _widthChangePos;
            }
            else if (sideOfGameZone.TypeOfGameZone == TypeOfGameZone.Right)
            {
                gameObject.transform.position += Vector3.right * - _widthChangePos;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isGodMode)
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();

            if (asteroid != null)
            {
                HitEventHandler.Invoke();
                TurnOnShield();
            }

            Destroyer destroyer = collision.gameObject.GetComponent<Destroyer>();

            if (destroyer != null)
            {
                HitEventHandler.Invoke();
                TurnOnShield();
            }
        }
    }

    public void TurnGodMode(bool turn)
    {
        _isGodMode = turn;
    }
}

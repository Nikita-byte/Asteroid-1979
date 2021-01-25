using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missle : MonoBehaviour
{
    private float _speed;
    private bool _isFire;
    private GameObject _aim;
    private float _timeOfUpdateAngle;
    private float _currentTimeOfUpdateAngle;
    private float _maxAngleOfTurn;

    public bool IsFire { get { return _isFire; } set { } }


    void Start()
    {
        _maxAngleOfTurn = GameSettings.Instance.MaxAngleOfTurnMissle;
        _timeOfUpdateAngle = GameSettings.Instance.TimeOfUpdateAngle;
        _currentTimeOfUpdateAngle = 0;
        _speed = GameSettings.Instance.MissleSpeed;
    }

    public void Move()
    {
        gameObject.transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);

        if (_currentTimeOfUpdateAngle >= _timeOfUpdateAngle)
        {
            _currentTimeOfUpdateAngle = 0;
            Turn();
        }

        _currentTimeOfUpdateAngle += Time.deltaTime;
    }

    public void SetAim(GameObject aim)
    {
        _aim = aim;
    }

    public void ClearAim()
    {
        _aim = null;
    }

    public void Fire()
    {
        _isFire = true;
    }

    private void Turn()
    {
        if (_aim != null)
        {
            Vector3 relative = _aim.transform.position - gameObject.transform.position;

            float angle = Vector3.Angle(relative, gameObject.transform.up);

            if (angle > _maxAngleOfTurn)
            {
                transform.Rotate(0, 0, _maxAngleOfTurn);
            }
            else
            {
                transform.Rotate(0, 0, angle);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        IsFire = false;
    }
}

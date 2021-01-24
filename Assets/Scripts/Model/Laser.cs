using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed;
    private bool _isFire;
    private float _laserLifeTime;
    private float _currentLifeTime;

    void Start()
    {
        _speed = GameSettings.Instance.LaserSpeed;
        _laserLifeTime = GameSettings.Instance.LaserLifeTime;
        _currentLifeTime = 0;
    }

    void Update()
    {
        if (_isFire)
        {
            gameObject.transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
            _currentLifeTime += Time.deltaTime;

            if (_currentLifeTime >= _laserLifeTime)
            {
                _currentLifeTime = 0;
                _isFire = false;
                gameObject.SetActive(false);
                ObjectPool.Instance.ReturnObjectInPool(this.gameObject, TypeOfGameObject.Laser);
            }
        }
    }

    public void Fire()
    {
        _isFire = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        ObjectPool.Instance.ReturnObjectInPool(gameObject, TypeOfGameObject.Laser);
    }
}

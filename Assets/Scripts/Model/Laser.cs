using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed;
    private bool _isFire;

    public bool IsFire { get { return _isFire; } set { } }

    void Start()
    {
        _speed = GameSettings.Instance.LaserSpeed;
    }

    public void Move()
    {
        gameObject.transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
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

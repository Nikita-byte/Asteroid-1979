using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
    }
}

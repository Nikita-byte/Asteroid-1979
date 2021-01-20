using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideOfGameZone : MonoBehaviour
{
    [SerializeField] private float _hight;
    [SerializeField] private float _width;
    private Cruiser _cruiser;
    private Asteroid _asteroid;

    public TypeOfGameZone TypeOfGameZone;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cruiser>(out _cruiser))
        {
            if (TypeOfGameZone == TypeOfGameZone.Top)
            {
                _cruiser.Move(collision.gameObject.transform.position += Vector3.up * -_hight);
            }
            else if (TypeOfGameZone == TypeOfGameZone.Bottom)
            {
                _cruiser.Move(collision.gameObject.transform.position += Vector3.up * _hight);
            }
            else if (TypeOfGameZone == TypeOfGameZone.Left)
            {
                _cruiser.Move(collision.gameObject.transform.position += Vector3.right * _width);
            }
            else if (TypeOfGameZone == TypeOfGameZone.Right)
            {
                _cruiser.Move(collision.gameObject.transform.position += Vector3.right * -_width);
            }
        }

        if (collision.TryGetComponent<Asteroid>(out _asteroid))
        {
            if (TypeOfGameZone == TypeOfGameZone.Top)
            {
                _asteroid.gameObject.GetComponent<Asteroid>().Move(collision.gameObject.transform.position += Vector3.up * -_hight);
            }
            else if (TypeOfGameZone == TypeOfGameZone.Bottom)
            {
                _asteroid.gameObject.GetComponent<Asteroid>().Move(collision.gameObject.transform.position += Vector3.up * _hight);
            }
            else if (TypeOfGameZone == TypeOfGameZone.Left)
            {
                _asteroid.gameObject.GetComponent<Asteroid>().Move(collision.gameObject.transform.position += Vector3.right * _width);
            }
            else if (TypeOfGameZone == TypeOfGameZone.Right)
            {
                _asteroid.gameObject.GetComponent<Asteroid>().Move(collision.gameObject.transform.position += Vector3.right * -_width);
            }
        }

        _asteroid = null;
        _cruiser = null;
    }
}

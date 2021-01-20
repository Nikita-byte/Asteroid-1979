using UnityEngine;


public class Cruiser : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private GameObject _moveDir;
    [SerializeField] private GameObject _missile;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_missile, gameObject.transform.position, gameObject.transform.rotation);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, _angle));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, -_angle));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _moveDir.transform.rotation = gameObject.transform.rotation;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (_speed >= _maxSpeed)
            {
                _speed = _maxSpeed;
            }
            else
            {
                _speed += _acceleration;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

        }

        _speed -= _deceleration;

        if (_speed <= 0)
        {
            _speed = 0;
        }

        _moveDir.transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
        gameObject.transform.position = _moveDir.transform.position;
    }

    public void Move(Vector2 position)
    {
        _moveDir.transform.position = position;
    }
}

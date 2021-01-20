using UnityEngine;


public class Asteroid: MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _coefficient;
    [SerializeField] private GameObject _icon;
    [SerializeField] private TypeOfAsteroid _typeOfAsteroid;

    private GameController _gameController;
    private Missile _missile;
    private Cruiser _cruiser;
    private float _angle;
    private float _speed;

    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        float temp = Random.Range(_minSpeed, _maxSpeed);

        transform.Rotate(Vector3.forward * temp);
        _angle = temp;
        _speed = temp / _coefficient;
    }

    void Update()
    {
        _icon.transform.Rotate(Vector3.forward * _angle * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    public void Move(Vector2 position)
    {
        gameObject.transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Missile>(out _missile))
        {
            Destroy(_missile.gameObject);
            Destroy(gameObject);
            _gameController.EnlargeScore();
        }

        if (collision.TryGetComponent<Cruiser>(out _cruiser))
        {
            if (!_gameController.IsGodMod)
            {
                Destroy(_cruiser.gameObject);
                Destroy(gameObject);
            }
        }

        Instantiate(_gameController.Explosion, collision.transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        switch (_typeOfAsteroid)
        {
            case TypeOfAsteroid.Big:
                for (int i = 0; i < _gameController.CountOfAsteroids; i++)
                {
                    Instantiate(_gameController.NormalAsteroid, transform.position, Quaternion.identity);
                }
                break;
            case TypeOfAsteroid.Normal:
                for (int i = 0; i < _gameController.CountOfAsteroids; i++)
                {
                    Instantiate(_gameController.SmallAsteroid, transform.position, Quaternion.identity);
                }
                break;
            case TypeOfAsteroid.Small:
                break;
        }
    }
}

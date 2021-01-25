using System.Collections.Generic;
using UnityEngine;


public sealed class EnemysController : IInitialization, IExecute
{
    private float _highChangePos;
    private float _widthChangePos;
    private List<GameObject> _asteroids;
    private List<GameObject> _tempGameObjects;
    private float _cooldownOfDestroyer;
    private float _currentTimeOfDestroyer;
    private Destroyer _destroyer;
    private int _index;
    private bool _isUpdateAsteroids;
    private Context _context;

    public EnemysController(Context context)
    {
        _context = context;
    }

    public void Initialization()
    {
        _cooldownOfDestroyer = GameSettings.Instance.CoolDownOfDestroyer;
        _currentTimeOfDestroyer = _cooldownOfDestroyer;
        _destroyer = ObjectPool.Instance.GetGameObject(TypeOfGameObject.Destroyer).GetComponent<Destroyer>();
        _index = 0;
        _isUpdateAsteroids = false;
        _asteroids = new List<GameObject>();
        _tempGameObjects = new List<GameObject>();
        _highChangePos = GameSettings.Instance.High - GameSettings.Instance.High / GameSettings.Instance.Indent;
        _widthChangePos = GameSettings.Instance.Width - GameSettings.Instance.Width / GameSettings.Instance.Indent;
        _destroyer.IsAlive = true;

        for (int i = 0; i < GameSettings.Instance.CountOfBigAsteroidsInPool; i++)
        {
            _asteroids.Add(ObjectPool.Instance.GetGameObject(TypeOfGameObject.BigAsteroid));
        }

        foreach (GameObject asteroid in _asteroids)
        {
            asteroid.transform.position = new Vector3(Random.Range(-_widthChangePos / 2, _widthChangePos / 2), Random.Range(-_highChangePos / 2, _highChangePos / 2), 0);
            asteroid.GetComponent<Asteroid>().IsAlive = true;
            asteroid.SetActive(true);
            asteroid.GetComponent<Asteroid>().SetSpeed(Random.Range(GameSettings.Instance.MinSpeedOfAsteroids, GameSettings.Instance.MaxSpeedOfAsteroids));
        }
    }

    public void Execute()
    {
        foreach (GameObject asteroid in _asteroids)
        {
            if (asteroid.GetComponent<Asteroid>().IsAlive)
            {
                asteroid.GetComponent<Asteroid>().Move();
            }
            else
            {
                _context.Score += GameSettings.Instance.ScoreForOneOfAsteroid;
                ScreenFactory.GetInstance().GetGameUI().GetScore().text = _context.Score.ToString();

                switch (asteroid.GetComponent<Asteroid>().TypeOfAsteroid)
                {
                    case TypeOfAsteroid.Big:
                        for (int i = 0; i < GameSettings.Instance.CountAsteroidsAfterDistruction; i++)
                        {
                            GameObject temp = ObjectPool.Instance.GetGameObject(TypeOfGameObject.NormalAsteroid);
                            temp.transform.position = asteroid.transform.position;
                            temp.GetComponent<Asteroid>().IsAlive = true;
                            temp.SetActive(true);
                            temp.GetComponent<Asteroid>().SetSpeed(Random.Range(GameSettings.Instance.MinSpeedOfAsteroids, GameSettings.Instance.MaxSpeedOfAsteroids));
                            _tempGameObjects.Add(temp);
                        }
                        break;
                    case TypeOfAsteroid.Normal:
                        for (int i = 0; i < GameSettings.Instance.CountAsteroidsAfterDistruction; i++)
                        {
                            GameObject temp = ObjectPool.Instance.GetGameObject(TypeOfGameObject.SmallASteroid);
                            temp.transform.position = asteroid.transform.position;
                            temp.GetComponent<Asteroid>().IsAlive = true;
                            temp.SetActive(true);
                            temp.GetComponent<Asteroid>().SetSpeed(Random.Range(GameSettings.Instance.MinSpeedOfAsteroids, GameSettings.Instance.MaxSpeedOfAsteroids));
                            _tempGameObjects.Add(temp);
                        }
                        break;
                    case TypeOfAsteroid.Small:
                        break;
                }

                _isUpdateAsteroids = true;
                _index = _asteroids.IndexOf(asteroid);
                asteroid.SetActive(false);
                ObjectPool.Instance.ReturnObjectInPool(asteroid.gameObject, TypeOfGameObject.BigAsteroid);
            }
        }

        if (!_destroyer.IsAlive)
        {
            _destroyer.gameObject.SetActive(false);
            _context.Score += GameSettings.Instance.ScoreForDestroyer;
            ScreenFactory.GetInstance().GetGameUI().GetScore().text = _context.Score.ToString();
        }

        if (_isUpdateAsteroids)
        {
            _asteroids.RemoveAt(_index);

            foreach (GameObject gameObject in _tempGameObjects)
            {
                _asteroids.Add(gameObject);
            }

            _tempGameObjects.Clear();
            _index = 0;
            _isUpdateAsteroids = false;
        }

        if (_currentTimeOfDestroyer > 0)
        {
            _destroyer.Move();
        }
        else
        {
            _currentTimeOfDestroyer = _cooldownOfDestroyer;
            DestroyerOnPosition();
        }

        _currentTimeOfDestroyer -= Time.deltaTime;
    }

    public void DestroyerOnPosition()
    {
        _destroyer.transform.position = new Vector3( - GameSettings.Instance.WidthOfApperance, Random.Range(GameSettings.Instance.MinHighOfApperance, 
            GameSettings.Instance.MaxHighOfApperance), 0);
        _destroyer.transform.Rotate(Vector3.forward * Random.Range(-GameSettings.Instance.AngleOfTurnDestroyer/2, 
            GameSettings.Instance.AngleOfTurnDestroyer / 2));
        _destroyer.gameObject.SetActive(true);
    }

    public GameObject GetEnemy()
    {
        return _asteroids[Random.Range(0, _asteroids.Count - 1)];
    }
}

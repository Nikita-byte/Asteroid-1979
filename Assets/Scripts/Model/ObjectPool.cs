using UnityEngine;
using System.Collections.Generic;


public sealed class ObjectPool
{
    private static readonly ObjectPool _instance = new ObjectPool();

    private readonly string POOLNAME = "Pool";
    private readonly string BIGASTEROIDS = "BigAsteroids";
    private readonly string NORMALASTEROIDS = "NormalAsteroids";
    private readonly string SMALLASTEROIDS = "SmallAsteroids";
    private readonly string LASERSHELLS = "LaserShells";
    private Queue<GameObject> _bigAsteroidsPool;
    private Queue<GameObject> _normalAsteroidsPool;
    private Queue<GameObject> _smallAsteroidsPool;
    private Queue<GameObject> _laserShellsPool;
    private GameObject _destroyer;
    private GameObject _objectPool;
    private GameObject _ship;
    private GameObject _moveDirection;
    private GameObject _bigAsteroids;
    private GameObject _normalAsteroids;
    private GameObject _smallAsteroids;
    private GameObject _laserShells;

    public static ObjectPool Instance { get { return _instance; } }

    public ObjectPool()
    {
        _objectPool = new GameObject(POOLNAME);
        _laserShells = new GameObject(LASERSHELLS);
        _bigAsteroids = new GameObject(BIGASTEROIDS);
        _normalAsteroids = new GameObject(NORMALASTEROIDS);
        _smallAsteroids = new GameObject(SMALLASTEROIDS);

        _laserShellsPool = new Queue<GameObject>();
        _bigAsteroidsPool = new Queue<GameObject>();
        _normalAsteroidsPool = new Queue<GameObject>();
        _smallAsteroidsPool = new Queue<GameObject>();

        _laserShells.transform.SetParent(_objectPool.transform);
        _bigAsteroids.transform.SetParent(_objectPool.transform);
        _normalAsteroids.transform.SetParent(_objectPool.transform);
        _smallAsteroids.transform.SetParent(_objectPool.transform);

        CreateDestroyer();
        CreateShip();
        CreateAsteroids();
        CreateLaser();
    }

    private void CreateAsteroids()
    {
        GameObject _asteroid = Resources.Load<GameObject>(AssetPath.Objects[TypeOfGameObject.BigAsteroid]);

        for (int i = 0; i < GameSettings.Instance.CountOfBigAsteroidsInPool; i++)
        {
            GameObject temp = GameObject.Instantiate(_asteroid, _bigAsteroids.transform);
            temp.SetActive(false);
            _bigAsteroidsPool.Enqueue(temp);
        }

        _asteroid = Resources.Load<GameObject>(AssetPath.Objects[TypeOfGameObject.NormalAsteroid]);

        for (int i = 0; i < GameSettings.Instance.CountOfNormalAsteroidsInPool; i++)
        {
            GameObject temp = GameObject.Instantiate(_asteroid, _normalAsteroids.transform);
            temp.SetActive(false);
            _normalAsteroidsPool.Enqueue(temp);
        }

        _asteroid = Resources.Load<GameObject>(AssetPath.Objects[TypeOfGameObject.SmallASteroid]);

        for (int i = 0; i < GameSettings.Instance.CountOfSmallAsteroidsInPool; i++)
        {
            GameObject temp = GameObject.Instantiate(_asteroid, _smallAsteroids.transform);
            temp.SetActive(false);
            _smallAsteroidsPool.Enqueue(temp);
        }
    }

    private void CreateLaser()
    {
        GameObject _shell = Resources.Load<GameObject>(AssetPath.Objects[TypeOfGameObject.Laser]);

        for (int i = 0; i < GameSettings.Instance.CountOfLaserShellsInPool; i++)
        {
            GameObject temp = GameObject.Instantiate(_shell, _laserShells.transform);
            temp.SetActive(false);
            _laserShellsPool.Enqueue(temp);
        }
    }

    private void CreateShip()
    {
        _ship = GameObject.Instantiate(Resources.Load<GameObject>(AssetPath.Objects[TypeOfGameObject.Ship]));
    }

    private void CreateDestroyer()
    {
        _destroyer = GameObject.Instantiate(Resources.Load<GameObject>(AssetPath.Objects[TypeOfGameObject.Destroyer]));
        _destroyer.SetActive(false);
        _destroyer.transform.SetParent(_objectPool.transform);
    }

    public GameObject GetShip()
    {
        return _ship;
    }

    public GameObject GetGameObject(TypeOfGameObject gameObject)
    {
        switch (gameObject)
        {
            case TypeOfGameObject.Laser:
                return _laserShellsPool.Dequeue();
            case TypeOfGameObject.BigAsteroid:
                return _bigAsteroidsPool.Dequeue();
            case TypeOfGameObject.NormalAsteroid:
                return _normalAsteroidsPool.Dequeue();
            case TypeOfGameObject.SmallASteroid:
                return _smallAsteroidsPool.Dequeue();
            case TypeOfGameObject.Destroyer:
                return _destroyer;
        }

        return null;
    }

    public void ReturnObjectInPool(GameObject gameObject, TypeOfGameObject typeOfGameObject)
    {
        switch (typeOfGameObject)
        {
            case TypeOfGameObject.Laser:
                gameObject.transform.SetParent(_laserShells.transform);
                _laserShellsPool.Enqueue(gameObject);
                break;
            case TypeOfGameObject.BigAsteroid:
                gameObject.transform.SetParent(_bigAsteroids.transform);
                _bigAsteroidsPool.Enqueue(gameObject);
                break;
            case TypeOfGameObject.NormalAsteroid:
                gameObject.transform.SetParent(_normalAsteroids.transform);
                _normalAsteroidsPool.Enqueue(gameObject);
                break;
            case TypeOfGameObject.SmallASteroid:
                gameObject.transform.SetParent(_smallAsteroids.transform);
                _smallAsteroidsPool.Enqueue(gameObject);
                break;
        }
    }
}

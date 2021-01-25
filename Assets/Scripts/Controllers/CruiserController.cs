using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CruiserController : IExecute, IInitialization
{
    private Cruiser _cruiser;
    private GameObject _moveDir;
    private GameObject _tempObject;
    private Missle _missle;
    private EnemysController _enemyController;
    private List<GameObject> _lasers;
    private List<int> _indexes;
    private int _maxHP = 5;
    private int _currentHP;
    private Context _context;
    private float _timeOfShield;
    private float _currentShieldTime;
    private float _currentCoolDownTimeOfShield;
    private float _coolDownOfShield;
    private float _currentCoolDownTimeOfMissle;
    private float _coolDownOfMissle;
    private float _currentTimeOfLaser;
    private float _lifeTimeOfLaser;
    private bool _isUpdateLaserPool;
    private int _index;


    public CruiserController(Context context, EnemysController enemysController)
    {
        _enemyController = enemysController;
        _context = context;
    }

    public void Initialization()
    {
        _lasers = new List<GameObject>();
        _indexes = new List<int>();
        _isUpdateLaserPool = false;
        _currentTimeOfLaser = 0;
        _lifeTimeOfLaser = GameSettings.Instance.LaserLifeTime;
        _coolDownOfMissle = GameSettings.Instance.CoolDownOfMissle;
        _coolDownOfShield = GameSettings.Instance.CoolDownOfShield;
        _timeOfShield = GameSettings.Instance.TimeOfShield;
        _missle = ObjectPool.Instance.GetGameObject(TypeOfGameObject.Missle).GetComponent<Missle>();
        _cruiser = ObjectPool.Instance.GetShip().GetComponent<Cruiser>();
        _moveDir = new GameObject("MoveDirection");
        _moveDir.transform.position = Vector3.zero;
        _cruiser.HitEventHandler += Damage;
        _cruiser.gameObject.SetActive(false);
    }

    public void Execute()
    {
        _cruiser.Move(_moveDir.transform.up);

        ShieldUpdate();
        MissleUpdate();
        LaserUpdate();
    }

    public void LeftTurn()
    {
        _cruiser.GetComponent<Transform>().Rotate(Vector3.forward * GameSettings.Instance.ShipRotateSpeed * Time.deltaTime);
    }

    public void RightTurn()
    {
        _cruiser.GetComponent<Transform>().Rotate(Vector3.forward * - GameSettings.Instance.ShipRotateSpeed * Time.deltaTime);
    }

    public void UpdateState(ShipState shipState)
    {
        _cruiser.ChangeState(shipState);
    }

    public void UpdateRotatePosition()
    {
        _moveDir.transform.rotation = _cruiser.transform.rotation;
    }

    public void LaserFire()
    {
        _tempObject = ObjectPool.Instance.GetGameObject(TypeOfGameObject.Laser);
        _tempObject.transform.position = _cruiser.gameObject.transform.position;
        _tempObject.transform.rotation = _cruiser.gameObject.transform.rotation;
        _tempObject.SetActive(true);
        _tempObject.GetComponent<Laser>().Fire();
        _lasers.Add(_tempObject);
    }

    public void MissleFire()
    {
        if (_currentCoolDownTimeOfMissle == 0)
        {
            _tempObject = ObjectPool.Instance.GetGameObject(TypeOfGameObject.Missle);
            _tempObject.transform.position = _cruiser.gameObject.transform.position;
            _tempObject.transform.rotation = _cruiser.gameObject.transform.rotation;
            _tempObject.transform.Rotate(new Vector3(0,0,180));
            _tempObject.SetActive(true);
            _tempObject.GetComponent<Missle>().Fire();
            _tempObject.GetComponent<Missle>().SetAim(_enemyController.GetEnemy());
            _currentCoolDownTimeOfMissle = _coolDownOfMissle;
        }
    }

    public void TurnOnShield()
    {
        if (_currentCoolDownTimeOfShield == 0)
        {
            _cruiser.TurnGodMode(true);
            _cruiser.TurnOnShield();
            _currentCoolDownTimeOfShield = _coolDownOfShield;
            _currentShieldTime = _timeOfShield;
        }
    }

    public void StartGame()
    {
        _cruiser.gameObject.SetActive(true);
        _cruiser.StartPosition();
        _currentHP = _maxHP;
        TurnOnShield();
        ScreenFactory.GetInstance().GetGameUI().SetLifes(_maxHP);
    }

    public void Damage()
    {
        _currentHP--;

        if (_currentHP > 0)
        {
            ScreenFactory.GetInstance().GetGameUI().OffOneLife(_currentHP);
            _cruiser.StartPosition();
        }
        else
        {
            _context.IsRun = false;
            ScreenFactory.GetInstance().GetGameUI().gameObject.SetActive(false);
            ScreenFactory.GetInstance().GetGameOverUI().gameObject.SetActive(true);
            ScreenFactory.GetInstance().GetGameOverUI().GetScore().text = _context.Score.ToString();
        }
    }

    public void ShieldUpdate()
    {
        if (_currentShieldTime != 0)
        {
            _currentShieldTime -= Time.deltaTime;

            _cruiser._shieldSprite.color = new Color(1, 1, 1, _currentShieldTime / _timeOfShield);

            if (_currentShieldTime <= 0)
            {
                _currentShieldTime = 0;
                _cruiser.TurnOffShield();
                _cruiser.TurnGodMode(false);
            }
        }

        _currentCoolDownTimeOfShield -= Time.deltaTime;
        ScreenFactory.GetInstance().GetGameUI().GetQ().fillAmount = 1 - (_currentCoolDownTimeOfShield / _coolDownOfShield);

        if (_currentCoolDownTimeOfShield <= 0)
        {
            _currentCoolDownTimeOfShield = 0;
        }
    }

    public void MissleUpdate()
    {
        if (_missle.IsFire)
        {
            _missle.Move();
        }

        if (_currentCoolDownTimeOfMissle != 0)
        {
            _currentCoolDownTimeOfMissle -= Time.deltaTime;
        }

        _currentCoolDownTimeOfMissle -= Time.deltaTime;
        ScreenFactory.GetInstance().GetGameUI().GetW().fillAmount = 1 - (_currentCoolDownTimeOfMissle / _coolDownOfMissle);

        if (_currentCoolDownTimeOfMissle <= 0)
        {
            _currentCoolDownTimeOfMissle = 0;
            _missle.IsFire = false;
            _missle.ClearAim();
            _missle.gameObject.SetActive(false);
        }
    }

    public void LaserUpdate()
    {
        foreach (GameObject laser in _lasers)
        {
            if (laser.GetComponent<Laser>().IsFire)
            {
                laser.GetComponent<Laser>().Move();

                _currentTimeOfLaser += Time.deltaTime;

                if (_currentTimeOfLaser >= _lifeTimeOfLaser)
                {
                    _currentTimeOfLaser = 0;
                    laser.GetComponent<Laser>().IsFire = false;
                    laser.SetActive(false);
                }
            }
            else
            {
                _isUpdateLaserPool = true;
                _index = _lasers.IndexOf(laser);
                _indexes.Add(_index);
                laser.SetActive(false);
                ObjectPool.Instance.ReturnObjectInPool(laser, TypeOfGameObject.Laser);
            }
        }

        if (_isUpdateLaserPool)
        {
            foreach (int index in _indexes)
            {
                _lasers.RemoveAt(index);
            }

            _lasers.Clear();
            _index = 0;
            _isUpdateLaserPool = false;
        }
    }
}

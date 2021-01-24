using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CruiserController : IExecute, IInitialization
{
    private Cruiser _cruiser;
    private GameObject _moveDir;
    private GameObject _tempObject;
    private int _maxHP = 5;
    private int _currentHP;
    private Context _context;
    private float _timeOfShield;
    private float _currentShieldTime;
    private float _currentCoolDownTime;
    private float _coolDownOfShield;

    public CruiserController(Context context)
    {
        _context = context;
    }

    public void Initialization()
    {
        _coolDownOfShield = GameSettings.Instance.CoolDownOfShield;
        _timeOfShield = GameSettings.Instance.TimeOfShield;
        _cruiser = ObjectPool.Instance.GetShip().GetComponent<Cruiser>();
        _moveDir = new GameObject("MoveDirection");
        _moveDir.transform.position = Vector3.zero;
        _cruiser.HitEventHandler += Damage;
        _cruiser.gameObject.SetActive(false);
    }

    public void Execute()
    {
        _cruiser.Move(_moveDir.transform.up);

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

        _currentCoolDownTime -= Time.deltaTime;
        ScreenFactory.GetInstance().GetGameUI().GetQ().fillAmount = 1 - (_currentCoolDownTime / _coolDownOfShield);

        if (_currentCoolDownTime <= 0)
        {
            _currentCoolDownTime = 0;
        }
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
    }

    public void TurnOnShield()
    {
        if (_currentCoolDownTime == 0)
        {
            _cruiser.TurnGodMode(true);
            _cruiser.TurnOnShield();
            _currentCoolDownTime = _coolDownOfShield;
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
}

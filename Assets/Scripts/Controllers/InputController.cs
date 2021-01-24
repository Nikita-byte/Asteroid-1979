using UnityEngine;


public class InputController : IExecute
{
    private CruiserController _cruiserController;
    private EnemysController _enemyController;
    private Context _context;

    public InputController(CruiserController cruiserController, Context context, EnemysController enemysController)
    {
        _cruiserController = cruiserController;
        _context = context;

        ScreenFactory.GetInstance().GetStartMenu().gameObject.SetActive(true);
        ScreenFactory.GetInstance().GetStartMenu().GetStartButton().onClick.AddListener(delegate 
        {
            StartGame();
        });

        ScreenFactory.GetInstance().GetGameOverUI().GetStartButton().onClick.AddListener(delegate
        {
            StartGame();
        });
        ScreenFactory.GetInstance().GetGameOverUI().gameObject.SetActive(false);
    }

    public void Execute()
    {
        if (_context.IsRun)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _cruiserController.LeftTurn();
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _cruiserController.RightTurn();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _cruiserController.UpdateRotatePosition();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _cruiserController.UpdateState(ShipState.Acceleration);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                _cruiserController.UpdateState(ShipState.Deceleration);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _cruiserController.LaserFire();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _cruiserController.TurnOnShield();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {

            }
        }
        

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_context.IsRun)
            {
                ScreenFactory.GetInstance().GetStartMenu().GetStartButton().onClick.Invoke();
            }
        }
    }

    private void StartGame()
    {
        _context.IsRun = true;
        _context.Score = 0;
        ScreenFactory.GetInstance().GetStartMenu().gameObject.SetActive(false);
        ScreenFactory.GetInstance().GetGameOverUI().GetScore().text = "";
        ScreenFactory.GetInstance().GetGameOverUI().gameObject.SetActive(false);
        ScreenFactory.GetInstance().GetGameUI().gameObject.SetActive(true);
        ScreenFactory.GetInstance().GetGameUI().GetScore().text = "";
        _cruiserController.StartGame();
    }
}

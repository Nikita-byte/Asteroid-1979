using UnityEngine;


public sealed class ScreenFactory
{
    private Canvas _canvas;
    private StartMenu _startMenu;
    private GameUI _gameUI;
    private GameOverUI _gameOverUI;
    private static ScreenFactory _instance;

    public ScreenFactory()
    {
        GetCanvas();
    }

    public static ScreenFactory GetInstance()
    {
        return _instance ?? (_instance = new ScreenFactory());
    }

    public Canvas GetCanvas()
    {
        if (_canvas == null)
        {
            var temp = Resources.Load<Canvas>(AssetPath.Screens[TypeOfScreen.Canvas]);
            _canvas = GameObject.Instantiate(temp);
        }

        return _canvas;
    }

    public StartMenu GetStartMenu()
    {
        if (_startMenu == null)
        {
            var temp = Resources.Load<StartMenu>(AssetPath.Screens[TypeOfScreen.StartMenu]);
            _startMenu = GameObject.Instantiate(temp, _canvas.transform.position, Quaternion.identity, GetCanvas().transform);
            _startMenu.gameObject.SetActive(false);
        }

        return _startMenu;
    }

    public GameUI GetGameUI()
    {
        if (_gameUI == null)
        {
            var temp = Resources.Load<GameUI>(AssetPath.Screens[TypeOfScreen.GameUI]);
            _gameUI = GameObject.Instantiate(temp, _canvas.transform.position, Quaternion.identity, GetCanvas().transform);
            _gameUI.gameObject.SetActive(false);
        }
        return _gameUI;
    }

    public GameOverUI GetGameOverUI()
    {
        if (_gameOverUI == null)
        {
            var temp = Resources.Load<GameOverUI>(AssetPath.Screens[TypeOfScreen.GameOver]);
            _gameOverUI = GameObject.Instantiate(temp, _canvas.transform.position, Quaternion.identity, GetCanvas().transform);
            _gameOverUI.gameObject.SetActive(false);
        }
        return _gameOverUI;
    }
}

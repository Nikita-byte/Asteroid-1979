using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    private Controllers _controllers;

    private void Start()
    {
        _controllers = new Controllers();
        _controllers.Initialization();
    }

    private void Update()
    {
        foreach (IExecute execute in _controllers.Executes)
        {
            execute.Execute();
        }
    }
}

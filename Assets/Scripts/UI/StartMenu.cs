using UnityEngine;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    public Button GetStartButton()
    {
        return _startButton;
    }
}
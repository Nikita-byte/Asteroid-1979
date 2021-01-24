using UnityEngine.UI;
using UnityEngine;


public sealed class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Button _startButton;

    public Button GetStartButton()
    {
        return _startButton;
    }

    public Text GetScore()
    {
        return _score;
    }
}

using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private int _countOfAsteroids;


    public GameObject BigAsteroid;
    public GameObject NormalAsteroid;
    public GameObject SmallAsteroid;
    public GameObject Explosion;
    public Text Text;
    public int CountOfAsteroids;
    public bool IsGodMod;
    public int ScoreForOneOfAsteroid;

    private int _score;
    private float _maxY = 10;
    private float _minY = -10;
    private float _maxX = 17;
    private float _minX = -17;


    void Start()
    {
        _score = 0;

        for (int i = 0; i < _countOfAsteroids; i++)
        {
            Instantiate(BigAsteroid, new Vector3(Random.Range(_minX, _maxX), Random.Range(_minY, _maxY), 0), Quaternion.identity);
        }
    }

    public void EnlargeScore()
    {
        _score += ScoreForOneOfAsteroid;
        Text.text = _score.ToString();
    }
}

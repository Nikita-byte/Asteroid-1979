using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    [SerializeField] private Image q;
    [SerializeField] private Image w;
    [SerializeField] private Text _score;
    [SerializeField] private Image[] lifes;

    public void SetLifes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            lifes[i].gameObject.SetActive(true);
        }
    }

    public void OffOneLife(int count)
    {
        lifes[count].gameObject.SetActive(false);
    }

    public Text GetScore()
    {
        return _score;
    }

    public Image GetQ()
    {
        return q;
    }

    public Image GetW()
    {
        return w;
    }
}

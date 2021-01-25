using UnityEngine;


public class Asteroid: MonoBehaviour
{
    [SerializeField] private GameObject _icon;

    public TypeOfAsteroid TypeOfAsteroid;

    private float _highChangePos;
    private float _widthChangePos;
    private float _angle;
    private float _speed;
    private float _coefficient;
    public bool IsAlive { get; set; }

    private void Awake()
    {
        _coefficient = GameSettings.Instance.CoeffOfAsteroids;
        _highChangePos = GameSettings.Instance.High - GameSettings.Instance.High / GameSettings.Instance.Indent;
        _widthChangePos = GameSettings.Instance.Width - GameSettings.Instance.Width / GameSettings.Instance.Indent;
    }

    public void Move()
    {
        _icon.transform.Rotate(Vector3.forward * _angle * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        transform.Rotate(Vector3.forward * speed);
        _angle = speed;
        _speed = speed / _coefficient;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SideOfGameZone sideOfGameZone;
        collision.TryGetComponent<SideOfGameZone>(out sideOfGameZone);

        if (sideOfGameZone != null)
        {
            if (sideOfGameZone.TypeOfGameZone == TypeOfGameZone.Top)
            {
                gameObject.transform.position += Vector3.up * -_highChangePos;
            }
            else if (sideOfGameZone.TypeOfGameZone == TypeOfGameZone.Bottom)
            {
                gameObject.transform.position += Vector3.up * _highChangePos;
            }
            else if (sideOfGameZone.TypeOfGameZone == TypeOfGameZone.Left)
            {
                gameObject.transform.position += Vector3.right * _widthChangePos;
            }
            else if (sideOfGameZone.TypeOfGameZone == TypeOfGameZone.Right)
            {
                gameObject.transform.position += Vector3.right * -_widthChangePos;
            }
        }

        Laser temp = collision.gameObject.GetComponent<Laser>();

        if (temp != null)
        {
            IsAlive = false;
        }

        Missle temp2 = collision.gameObject.GetComponent<Missle>();

        if (temp2 != null)
        {
            IsAlive = false;
        }
    }
}

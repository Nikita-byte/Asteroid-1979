using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("GameSettings")]
    public int ScoreForOneOfAsteroid;
    public int ScoreForDestroyer;

    [Header("GameZone")]
    public float High;
    public float Width;
    public float Indent;

    [Header("Cruiser")]
    public float ShipRotateSpeed;
    public float MaxSpeedOfShip;
    public float Acceleration;
    public float Deceleration;
    public float TimeOfShield;
    public float CoolDownOfShield;

    [Header("Destroyer")]
    public float AngleOfTurnDestroyer;
    public float CoolDownOfTurnDestroyer;
    public float SpeedOfDestroyer;
    public int MinHighOfApperance;
    public int MaxHighOfApperance;
    public int WidthOfApperance;
    public float CoolDownOfDestroyer;

    [Header("Laser")]
    public float LaserSpeed;
    public float LaserLifeTime;

    [Header("Missle")]
    public float MissleSpeed;
    public float CoolDownOfMissle;
    public float TimeOfUpdateAngle;
    public float MaxAngleOfTurnMissle;

    [Header("Asteroids")]
    public float MaxSpeedOfAsteroids;
    public float MinSpeedOfAsteroids;
    public float CoeffOfAsteroids;
    public int CountAsteroidsAfterDistruction;

    [Header("ObjectPool")]
    public int CountOfLaserShellsInPool;
    public int CountOfBigAsteroidsInPool;
    public int CountOfNormalAsteroidsInPool;
    public int CountOfSmallAsteroidsInPool;


    private static GameSettings _instance;
    public static GameSettings Instance 
    { 
        get 
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GameSettings>(AssetPath.Objects[TypeOfGameObject.GameSettings]);
            }
            return _instance; 
        }
    }
}

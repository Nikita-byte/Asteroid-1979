using System;
using System.Collections.Generic;


public class AssetPath
{
    public static readonly Dictionary<TypeOfGameObject, string> Objects = new Dictionary<TypeOfGameObject, string>()
    {
        { TypeOfGameObject.Ship, "Prefabs/Ship" },
        { TypeOfGameObject.GameSettings, "Data/Settings"},
        { TypeOfGameObject.Laser, "Prefabs/Laser"},
        { TypeOfGameObject.BigAsteroid, "Prefabs/Asteroid"},
        { TypeOfGameObject.NormalAsteroid, "Prefabs/NormalAsteroid"},
        { TypeOfGameObject.SmallASteroid, "Prefabs/SmallAsteroid"},
        { TypeOfGameObject.Destroyer, "Prefabs/Destroyer"},
        { TypeOfGameObject.Missle, "Prefabs/Missle"}
    };

    public static readonly Dictionary<TypeOfScreen, string> Screens = new Dictionary<TypeOfScreen, string>()
    {
        { TypeOfScreen.Canvas, "Prefabs/UI/Canvas" },
        { TypeOfScreen.StartMenu, "Prefabs/UI/StartMenu"},
        { TypeOfScreen.GameUI, "Prefabs/UI/GameUI"},
        { TypeOfScreen.GameOver, "Prefabs/UI/GameOverUI"}
    };
}

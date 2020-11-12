using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileName
    {
        Pared,
        Cruce,
        Final,
        Puerta
    }

    public GameObject forward, back, left, right;

    public TileName currentTileName
    {
        get
        {
            var gameObjectName = gameObject.name;
            if (gameObjectName.Contains("Pared")) return TileName.Pared;
            else if (gameObjectName.Contains("Cruce")) return TileName.Cruce;
            else if (gameObjectName.Contains("Puerta")) return TileName.Puerta;
            else return TileName.Final;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTile : MonoBehaviour
{
    public enum TileName
    {
        Pared,
        Cruce,
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
            else return TileName.Puerta;

        }
    }
}

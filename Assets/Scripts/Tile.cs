using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileName
    {
        Pared,
        Final,
        Cruce,
        ParedConcava,
        PisoTecho,
        PuertaAbierta
    }

    public GameObject forward, back, left, right;

    public TileName currentTileName
    {
        get
        {
            var gameObjectName = gameObject.name;
            if (gameObjectName.Contains("Pared")) return TileName.Pared;
            else if (gameObjectName.Contains("Cruce")) return TileName.Cruce;
            else if (gameObjectName.Contains("ParedConcava")) return TileName.ParedConcava;
            else if (gameObjectName.Contains("PisoTecho")) return TileName.PisoTecho;
            else if (gameObjectName.Contains("PuertaAbierta")) return TileName.PuertaAbierta;
            else return TileName.Final;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]                                       //internette datami bunu yazmadan binary`e ceviremeyecegim yaziyor.
public class GameData
{
    public int score;
    public float y;
    public GameData(player player)                          //Oyuncu bilgilerini kaydetmek icin container
    {
        score = player.score;
        y = player.transform.position.y;
    }
}


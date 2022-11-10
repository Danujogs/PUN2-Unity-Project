using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardNetPlayer : MonoBehaviour
{
    public static List<CardNetPlayer> NetPlayers = new List<CardNetPlayer>(2);
    private CardPlayer cardplayer;

    public void Set(CardPlayer player)
    {
        cardplayer = player;
    }

    private void OnEnable()
    {
        NetPlayers.Add(this);
    }

    private void OnDisable()
    {
        NetPlayers.Remove(this);
    }
}

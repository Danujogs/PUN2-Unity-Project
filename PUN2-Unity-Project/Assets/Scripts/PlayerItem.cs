using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    public void Set(Photon.Realtime.Player player)
    {
        playerName.text = player.NickName;
        if (PhotonNetwork.IsMasterClient)
        {
            playerName.text += " (Master)";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_InputField newRoomInputField;
    [SerializeField] TMP_Text feedbackText;
    public void ClickCreateRoom()
    {
        feedbackText.text = "";
        if (newRoomInputField.text.Length < 3)
        {
            Debug.Log("Room name min 3 characters!");
            return;
        }
        else
        {
            PhotonNetwork.CreateRoom(newRoomInputField.text);
        }

        PhotonNetwork.CreateRoom(newRoomInputField.text);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);
        feedbackText.text = "Created room: " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        feedbackText.text = "Created room: " + PhotonNetwork.CurrentRoom.Name;
    }
}

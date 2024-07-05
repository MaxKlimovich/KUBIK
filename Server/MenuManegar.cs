using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuMenegar : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Creat room, Name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to create room");
    }
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text); 
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
    
}

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject cancelButton;
    [SerializeField] int RoomSize;


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
    }

    public void quickStart()
    {
        startButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("Creating room");
        int randomRoomNum = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room " + randomRoomNum, roomOps);
        Debug.Log(randomRoomNum);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed, Trying again");
        CreateRoom();
    }

    public void quickCancel()
    {
        cancelButton.SetActive(false);
        startButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}

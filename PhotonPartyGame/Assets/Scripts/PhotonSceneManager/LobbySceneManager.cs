using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using System.Text;

public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputPlayerName;
    [SerializeField] InputField inputRoomName;
    [SerializeField] Text textRoomList;

    void Start()
    {
        //�p�G�S���s�����\�N��������("")
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            //�p�G�ثe�j�U���A==�S���j�U
            if(PhotonNetwork.CurrentLobby == null)
            {
                //�[�J���A�����j�U
                PhotonNetwork.JoinLobby();
            }
        }
    }

    //���s�s������A���ɩI�s
    public override void OnConnectedToMaster()
    {
        print("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    //�[�J�j�U�ɩI�s
    public override void OnJoinedLobby()
    {
        print("Lobby joined!");
    }
    //�L�o�ж��W��
    public string FilterRoomName()
    {
        string roomName = inputRoomName.text;
        //Trim()�����ťղŸ�
        return roomName;
    }
    public string FilterPlayerName()
    {
        string playerName = inputPlayerName.text;
        return playerName;
    }


    //���o���a��J���ж��W��
    //using UnityEngine.UI
    //���\�Ыةж��|�۰ʥ[�J
    public void OnClickCreateRoom()
    {
        string roomName = FilterRoomName();
        string playerName = FilterPlayerName();
        if (roomName.Length > 0 && playerName.Length > 0)
        {
            PhotonNetwork.CreateRoom(roomName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
        }
        else
        {
            print("Invalid RoomName or PlayerName!");
        }
    }
    public void OnClickJoinRoom()
    {
        string roomName = FilterRoomName();
        string playerName = FilterPlayerName();
        if (roomName.Length > 1 && playerName.Length > 1)
        {
            PhotonNetwork.JoinRoom(roomName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
        }
        else
        {
            print("Invalid RoomName or PlayerName!");
        }

    }

    //�[�J�ж��ɩI�s
    public override void OnJoinedRoom()
    {
        print("Room Joined!");
        SceneManager.LoadScene("RoomScene");
    }

    //�ж��C���s�ɩI�s
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //using System.Text�~�i�ϥ�StringBuilder
        StringBuilder sb = new StringBuilder();

        foreach(RoomInfo roomInfo in roomList)
        {
            //��e�ж����a�ƶq
            if (roomInfo.PlayerCount > 0)
            {
                sb.AppendLine("> " + roomInfo.Name + "\n��e���a�ƶq: " + roomInfo.PlayerCount);
            }
        }
        textRoomList.text = sb.ToString();
    }
}

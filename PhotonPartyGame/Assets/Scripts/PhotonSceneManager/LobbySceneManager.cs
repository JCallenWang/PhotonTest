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
        //如果沒有連接成功就切換場景("")
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            //如果目前大廳狀態==沒有大廳
            if(PhotonNetwork.CurrentLobby == null)
            {
                //加入伺服器的大廳
                PhotonNetwork.JoinLobby();
            }
        }
    }

    //重新連接到伺服器時呼叫
    public override void OnConnectedToMaster()
    {
        print("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    //加入大廳時呼叫
    public override void OnJoinedLobby()
    {
        print("Lobby joined!");
    }
    //過濾房間名稱
    public string FilterRoomName()
    {
        string roomName = inputRoomName.text;
        //Trim()忽略空白符號
        return roomName;
    }
    public string FilterPlayerName()
    {
        string playerName = inputPlayerName.text;
        return playerName;
    }


    //取得玩家輸入的房間名稱
    //using UnityEngine.UI
    //成功創建房間會自動加入
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

    //加入房間時呼叫
    public override void OnJoinedRoom()
    {
        print("Room Joined!");
        SceneManager.LoadScene("RoomScene");
    }

    //房間列表更新時呼叫
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //using System.Text才可使用StringBuilder
        StringBuilder sb = new StringBuilder();

        foreach(RoomInfo roomInfo in roomList)
        {
            //當前房間玩家數量
            if (roomInfo.PlayerCount > 0)
            {
                sb.AppendLine("> " + roomInfo.Name + "\n當前玩家數量: " + roomInfo.PlayerCount);
            }
        }
        textRoomList.text = sb.ToString();
    }
}

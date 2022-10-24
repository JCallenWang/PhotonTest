using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using Photon.Realtime;

public class RoomSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Text textRoomName;
    [SerializeField] Text textPlayerList;
    [SerializeField] Button buttonStartGame;

    void Start()
    {
        if(PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            textRoomName.text = "CurrentRoom : " + PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
        }
        //是房主的時候按鈕就可以互動
        buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
    }


    public void UpdatePlayerList()
    {
        StringBuilder sb = new StringBuilder();
        foreach(var keyValue in PhotonNetwork.CurrentRoom.Players)
        {
            sb.AppendLine("> " + keyValue.Value.NickName);
        }
        textPlayerList.text = sb.ToString();
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickLeaveRoom()
    {
        //LeaveRoom會需要重新與伺服器連線更新Room的狀態, 利用OnConnectedToMaster()呼叫PhotonNetwork.JoinLobby()
        PhotonNetwork.LeaveRoom();
    }

    //有其他玩家進入時呼叫
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        print(newPlayer.NickName + " has Enter");
    }
    //有其他玩家離開時呼叫
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        print(otherPlayer.NickName + " has Left");
    }
    //當本人離開時呼叫
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    //房主換人時呼叫(Photon會自動更換房主)
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
    }
}

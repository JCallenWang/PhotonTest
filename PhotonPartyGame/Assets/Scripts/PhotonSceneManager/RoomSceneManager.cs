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
        //�O�ХD���ɭԫ��s�N�i�H����
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
        //LeaveRoom�|�ݭn���s�P���A���s�u��sRoom�����A, �Q��OnConnectedToMaster()�I�sPhotonNetwork.JoinLobby()
        PhotonNetwork.LeaveRoom();
    }

    //����L���a�i�J�ɩI�s
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        print(newPlayer.NickName + " has Enter");
    }
    //����L���a���}�ɩI�s
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        print(otherPlayer.NickName + " has Left");
    }
    //���H���}�ɩI�s
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    //�ХD���H�ɩI�s(Photon�|�۰ʧ󴫩ХD)
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        buttonStartGame.interactable = PhotonNetwork.IsMasterClient;
    }
}

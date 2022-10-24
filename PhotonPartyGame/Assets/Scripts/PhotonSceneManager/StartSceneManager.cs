using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviourPunCallbacks
{
    public void OnClickStart()
    {
        //自動同步場景
        PhotonNetwork.AutomaticallySyncScene = true;

        //使用設定的AppID連接到創建的伺服器專案上
        PhotonNetwork.ConnectUsingSettings();
        print("Connecting...");
    }

    //當連線到伺服器專案時執行
    public override void OnConnectedToMaster()
    {
        print("Connected!");
        SceneManager.LoadScene("LobbyScene");
    }
}

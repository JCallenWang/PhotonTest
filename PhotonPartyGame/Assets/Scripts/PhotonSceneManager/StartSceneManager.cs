using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviourPunCallbacks
{
    public void OnClickStart()
    {
        //�۰ʦP�B����
        PhotonNetwork.AutomaticallySyncScene = true;

        //�ϥγ]�w��AppID�s����Ыت����A���M�פW
        PhotonNetwork.ConnectUsingSettings();
        print("Connecting...");
    }

    //��s�u����A���M�׮ɰ���
    public override void OnConnectedToMaster()
    {
        print("Connected!");
        SceneManager.LoadScene("LobbyScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] Text messageText;
    [SerializeField] List<string> messageList;

    PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        _pv = GetComponent<PhotonView>();
        if(PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            InitGame();
        }
    }

    public void InitGame()
    {
        float spawnPointX = Random.Range(-20, 20);
        float spawnPointZ = Random.Range(-10, 10);

        //放在Assets - Resources裡的物件會自動被Photon搜尋
        PhotonNetwork.Instantiate("PhotonPlayer", new Vector3(spawnPointX, 3, spawnPointZ), Quaternion.identity);
    }

    public void CallRpcSendMessageToAll(string message)
    {
        //像所有RpcTarget執行RpcSendMessage
        _pv.RPC("RpcSendMessage", RpcTarget.All, message);
    }

    [PunRPC]
    void RpcSendMessage(string message, PhotonMessageInfo info)
    {
        //PhotonMessageInfo可以知道來自哪位玩家的資訊

        //如果訊息超過10行
        if (messageList.Count >= 10)
        {
            messageList.RemoveAt(0);
        }
        messageList.Add(message);
        UpdateMessage();
    }

    void UpdateMessage()
    {
        messageText.text = string.Join("\n", messageList);
    }
}

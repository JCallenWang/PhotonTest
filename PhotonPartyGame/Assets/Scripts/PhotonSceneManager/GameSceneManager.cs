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

        //��bAssets - Resources�̪�����|�۰ʳQPhoton�j�M
        PhotonNetwork.Instantiate("PhotonPlayer", new Vector3(spawnPointX, 3, spawnPointZ), Quaternion.identity);
    }

    public void CallRpcSendMessageToAll(string message)
    {
        //���Ҧ�RpcTarget����RpcSendMessage
        _pv.RPC("RpcSendMessage", RpcTarget.All, message);
    }

    [PunRPC]
    void RpcSendMessage(string message, PhotonMessageInfo info)
    {
        //PhotonMessageInfo�i�H���D�Ӧۭ��쪱�a����T

        //�p�G�T���W�L10��
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

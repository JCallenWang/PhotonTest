using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public PhotonView pv;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        if (!pv.IsMine)
            Destroy(rb);
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            timer += Time.deltaTime;

            if (timer > 1)
            {
                timer = 0;
                //使用PhotonNetwork.Destroy才會同步到伺服器
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}

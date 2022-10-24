using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using HashTable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
    PhotonView _pv;
    Rigidbody rb;
    public float speed;
    public float rotateSpeed;
    public float jumpPower;
    public float bulletPower;
    int hp;
    int maxHp = 1000;
    bool isJumped;

    [Header("UI")]
    [SerializeField] Image hpImage;
    [SerializeField] TextMeshProUGUI nameText;

    GameSceneManager _gm;

    // Start is called before the first frame update
    void Start()
    {
        _pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        _gm = GameObject.Find("GameSceneManager").GetComponent<GameSceneManager>();

        hp = maxHp;

        if (!_pv.IsMine)
        {
            //�R����L���a��Rigibody�קK���a���O�P�P�B�y���ݰ�
            //���O�|�ɭP���a���I�������|����
            Destroy(GetComponent<Rigidbody>());
        }
        nameText.text = _pv.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        //�u����a����
        if (_pv.IsMine)
        {
            Control();
        }
    }

    void Control()
    {
        float input_X = Input.GetAxis("Horizontal");
        float input_Z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(input_X, 0, input_Z);
        movement = Vector3.ClampMagnitude(movement,1);
        if(movement != Vector3.zero)
        {
            transform.position += movement * speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement, Vector3.up), rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumped)
        {
            rb.AddForce(Vector3.up * jumpPower,ForceMode.Impulse);
            isJumped = true;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject bullet = PhotonNetwork.Instantiate("PhotonBulletTrail", transform.position + new Vector3(0, 0, 1), Quaternion.LookRotation(transform.forward));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * bulletPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //PhotonView�[�����O���a�ޱ����~�|true(�u�������a����)
        if (_pv.IsMine) 
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                print("Hit");
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                //�p�G�l�u���󤣬O���a�ޱ���
                if (!bullet.pv.IsMine)
                {
                    print("Hit by IsMine");
                    HashTable table = new HashTable();
                    hp -= 10;
                    table.Add("hp", hp);
                    _gm.CallRpcSendMessageToAll(bullet.pv.Owner.NickName + " hit " + _pv.Owner.NickName);
                    PhotonNetwork.LocalPlayer.SetCustomProperties(table);
                    if (hp <= 0)
                    {
                        print(PhotonNetwork.NickName + " is Dead");
                        PhotonNetwork.Destroy(gameObject);
                    }
                }
            }

            if (collision.gameObject.CompareTag("Ground"))
            {
                isJumped = false;
            }
        }
    }

    public void UpdateHpBar()
    {
        float hpRatio = (float)hp / maxHp;
        hpImage.transform.localScale = new Vector3(hpRatio, hpImage.transform.localScale.y, hpImage.transform.localScale.z);
    }

    //�ݩʦ����ɩI�s�A�ñ���(���Ө���, �����ݩ�)
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, HashTable changedProps)
    {
        //�p�G����O���a��
        if (targetPlayer == _pv.Owner)
        {
            hp = (int)changedProps["hp"];
            print(targetPlayer.NickName + " : " + hp.ToString());
            UpdateHpBar();
        }
    }
}

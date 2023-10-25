using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks //automatically called when certain event happened
{

    public InputField UserName;
    public Text ButtonText ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnclickConnect()
    {
       if(UserName.text.Length >= 1)
        {
            PhotonNetwork.NickName = UserName.text;
            ButtonText.text = "connecting...";
            PhotonNetwork.ConnectUsingSettings(); //connect to photonserver
        }
    }

    public override void OnConnectedToMaster()
    {
       
        //base.OnConnectedToMaster();
        SceneManager.LoadScene("Room");
    }
}

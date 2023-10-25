using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class chat : MonoBehaviourPun
{
    public InputField inputField;
    public GameObject Message;
    public GameObject Content;
    public Dropdown playerDropdown; // for slecting multiplayer

    private int selectedPlayerIndex = -1;

    private void Start()
    {
        
        UpdatePlayerDropdown();
    }

    private void UpdatePlayerDropdown()
    {
        playerDropdown.ClearOptions();

        List<string> playerNames = new List<string>();
        playerNames.Add("All"); 

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerNames.Add(player.NickName);
        }

        playerDropdown.AddOptions(playerNames);
    }

    public void SendMessage()
    {
        string messageToSend = PhotonNetwork.NickName + ":" + inputField.text;

        if (selectedPlayerIndex == -1)
        {
            // Send the message to all players
            GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.Others, messageToSend);
        }
        else
        {
            // Send the message to the selected player only
            int targetActorNumber = PhotonNetwork.PlayerList[selectedPlayerIndex - 1].ActorNumber;
            GetComponent<PhotonView>().RPC("GetMessage", PhotonNetwork.PlayerList[targetActorNumber], messageToSend);
        }

        inputField.text = "";
    }

    public void OnPlayerDropdownValueChanged(int index)
    {
        selectedPlayerIndex = index;
    }

    [PunRPC]
    public void GetMessage(string ReceiveMessage)
    {
        GameObject M = Instantiate(Message, Vector3.zero, Quaternion.identity, Content.transform);
        M.GetComponent<Message>().MyMessage.text = ReceiveMessage;
    }
}

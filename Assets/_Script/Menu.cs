using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviourPunCallbacks
{
    #region Variables
    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject lobbyScreen;

    [Header("Main Screen")]
    public Button createRoomButton;
    public Button joinRoomButton;

    [Header("Lobby Screen")]
    public TMP_Text playerListText;
    public Button startGameButton;
    #endregion
    #region Monobehaviour method
    private void Start()
    {
        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
    }
    #endregion
    #region Photon Method
    public override void OnConnectedToMaster()
    {
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        SetScreen(lobbyScreen);
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyUI();
    }
    #endregion
    #region RPC Method
    [PunRPC]
    public void UpdateLobbyUI()
    {
        playerListText.text = "";

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }

        startGameButton.interactable = PhotonNetwork.IsMasterClient ? true : false;
    }

    public void OnLeaveLobbyButton()
    {
        PhotonNetwork.LeaveRoom();
        SetScreen(mainScreen);
    }

    public void OnStartGameButton()
    {
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Game");
    }
    #endregion
    #region Public Method
    public void OnCreateRoomButton (TMP_InputField roomNameInput) { NetworkManager.instance.createRoom(roomNameInput.text); }
    public void OnJoinRoomButton(TMP_InputField roomNameInput) { NetworkManager.instance.JoinRoom(roomNameInput.text); }
    public void OnPlayerNameUpdate(TMP_InputField playerNameInput) {PhotonNetwork.NickName = playerNameInput.text; }
    #endregion
    #region Private Method
    void SetScreen (GameObject screen)
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        screen.SetActive(true);
    }
    #endregion
}

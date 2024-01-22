using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText = null;
    //[SerializeField] private TMP_InputField playerNameInputField;
    //[SerializeField] private Button quickJoinButton;
    //[SerializeField] private Button newLobbyButton;
    //[SerializeField] private TMP_InputField codeInputField;
    //[SerializeField] private Button codeJoinButton;

    //void Start()
    //{
    //    quickJoinButton.interactable = false;
    //    newLobbyButton.interactable = false;
    //    codeJoinButton.interactable = false;

    //    playerNameInputField.onValueChanged.AddListener(delegate
    //    {
    //        InputValueCheck();
    //        MultiplayerManager.Instance.SetPlayerName(playerNameInputField.text);
    //    });

    //    codeInputField.onValueChanged.AddListener(delegate
    //    {
    //        InputValueCheck();
    //    });

    //}
    public void StartHost()
    {
        if (NetworkManager.Singleton.StartHost())
        {
            debugText.text = "Host started";
            NetworkManager.Singleton.SceneManager.LoadScene("Game2Scene", LoadSceneMode.Single);
        }
        else
        {
            debugText.text = "Host failed to Start";
        }
    }

    public void StartServer()
    {
        if (NetworkManager.Singleton.StartServer())
        {
            debugText.text = "Server started";
        }
        else
        {
            debugText.text = "Server failed to Start";
        }
    }

    public void StartClient()
    {
        if (NetworkManager.Singleton.StartClient())
        {
            debugText.text = "Client started";
            NetworkManager.Singleton.SceneManager.LoadScene("Game2Scene", LoadSceneMode.Single);
        }
        else
        {
            debugText.text = "Client failed to Start";
        }
    }

    //public void InputValueCheck()
    //{
    //    if (playerNameInputField.text != null && playerNameInputField.text.Length > 0)
    //    {
    //        quickJoinButton.interactable = true;
    //        newLobbyButton.interactable = true;
    //        if (codeInputField.text != null && codeInputField.text.Length > 0)
    //        {
    //            codeJoinButton.interactable = true;
    //        }
    //        else
    //        {
    //            codeJoinButton.interactable = false;
    //        }
    //    }
    //    else
    //    {
    //        quickJoinButton.interactable = false;
    //        newLobbyButton.interactable = false;
    //        codeJoinButton.interactable = false;
    //    }

    //}
}
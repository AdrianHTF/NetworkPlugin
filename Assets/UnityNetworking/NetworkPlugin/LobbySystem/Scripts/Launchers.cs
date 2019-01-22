
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using VRTK;
using UnityEngine.SceneManagement;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SA
{

    public class Launchers : Photon.PunBehaviour
    {
        public PhotonLogLevel logLevel = PhotonLogLevel.Full;
        bool connectedToRoom = false;
        string gameVersion = "1";
        bool isLoaded;
        bool hasRooms;
        bool uiIsOpen;
        public byte MaxPlayersPerRoom = 7;
        public GameObject roomName;
        private GameObject keyboard;
        private GameObject LobbyUI;
        private GameObject AvatarSelection;
        public UnityEngine.Object LoadSceneNext;
        public List<GameObject> avatar;
        static public GameObject playerAvatar;
        public GameObject avatarDropdown;



        public static Launchers singelton;

        private void Awake()
        {
            playerAvatar = avatar[0];
            singelton = this;
            PhotonNetwork.logLevel = logLevel;
            PhotonNetwork.autoJoinLobby = true;
            PhotonNetwork.automaticallySyncScene = true;
           
        }

        public void switchToKeyboard()
        {
            keyboard.SetActive(true);
            LobbyUI.SetActive(false);
        }

        public void switchToLobbyUI()
        {
            keyboard.SetActive(false);
            AvatarSelection.SetActive(false);
            LobbyUI.SetActive(true);
        }

        public void switchToAvatarSelection()
        {
            LobbyUI.SetActive(false);
            AvatarSelection.SetActive(true);
        }

        void Start()
        {
            PhotonNetwork.ConnectUsingSettings(gameVersion);
            UIManager.singelton.CloseUI();
            UIManager.singelton.UpdateLogger("Connecting to Server");
            uiIsOpen = false;

            keyboard = GameObject.FindGameObjectWithTag("key");
            LobbyUI = GameObject.FindGameObjectWithTag("LobbyUI");
            AvatarSelection = GameObject.FindGameObjectWithTag("avatarSelection");
            AvatarSelection.SetActive(false);
            keyboard.SetActive(false);
        }
        
        void Update()
        {
            if(isLoaded)
            {
                return;
            }
            if (PhotonNetwork.insideLobby)
            {
                if (!uiIsOpen) {
                    UIManager.singelton.OpenUI();
                    uiIsOpen = true;
                }

                if (!hasRooms)
                {
                    UIManager ui = UIManager.singelton;
                    RoomInfo[] rm = PhotonNetwork.GetRoomList();
                    for (int i = 0; i < rm.Length; i++)
                    {
                        ui.AddRoom(rm[i]);
                    }
                    if (rm.Length > 0)
                        hasRooms = true;
                }
            }

        }

        public void Button_Create()
        {
            string roomNameString = roomName.GetComponentInChildren<Text>().text;
            if (roomNameString.Length < 1)
                roomNameString = "Default-Room"; 
            var options = new RoomOptions();
            options.MaxPlayers = MaxPlayersPerRoom;
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.LoadLevel(LoadSceneNext.name);
                PhotonNetwork.CreateRoom(roomNameString, options, null);
            }
            else
            {
                Debug.Log("not yet");
            }
        }

        public void Button_JoinRoom(RoomInfo r)
        {
            UIManager.singelton.CloseUI();
            UIManager.singelton.UpdateLogger("Joining Room");
            PhotonNetwork.LoadLevel(LoadSceneNext.name);
            PhotonNetwork.JoinRoom(r.Name);
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            UIManager.singelton.UpdateLogger("Room created");
            StartCoroutine("CreateRoomProcess");
        }

        public override void OnJoinedLobby()
        {
            UIManager.singelton.UpdateLogger("Joined Lobby", true);
            
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("Can't join random room!");
        }

        public override void OnConnectedToMaster()
        {
            connectedToRoom = true;
            UIManager.singelton.OpenUI();
            UIManager.singelton.UpdateLogger("Connected", true);
        }


        void OnApplicationQuit()
        {
            Debug.Log("Quiting game");
            QuitGame();
        }

        public void QuitGame()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void setAvatar(int idx)
        {
            playerAvatar = avatar[idx];
        }


    }
}

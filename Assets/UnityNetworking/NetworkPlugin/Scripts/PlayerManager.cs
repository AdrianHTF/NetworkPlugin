using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SA
{
    public class PlayerManager : Photon.PunBehaviour
    {
        private SortedDictionary<int, GameObject> spawns;

        [Tooltip("Reference to the player avatar prefab")]
        public GameObject playerAvatar;
        //public List<GameObject> playerAvatar = new List<GameObject>();
        public Boolean joinRandomRoom = false;
        public byte MaxPlayersPerRoom = 5;

        public delegate void OnCharacterInstantiated(GameObject character);
        public static event OnCharacterInstantiated CharacterInstantiated;


        void Awake()
        {
            UpdateSpawns();
            if (joinRandomRoom)
            {
                PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms.
                PhotonNetwork.automaticallySyncScene = true;
                PhotonNetwork.ConnectUsingSettings("1.0");
            }
            if(Launchers.playerAvatar != null)
            {
                playerAvatar = Launchers.playerAvatar;
            }
        }

        public override void OnJoinedLobby()
        {
            if (joinRandomRoom)
            {
                Debug.Log("Joined lobby");
                Debug.Log("Joining random room...");
                PhotonNetwork.JoinRandomRoom();

            }
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            if (joinRandomRoom)
            {
                Debug.Log("Can't join random room!");
                Debug.Log("Creating room...");
                var options = new RoomOptions();
                options.MaxPlayers = MaxPlayersPerRoom;
                PhotonNetwork.CreateRoom(null, options, null);
            }
            else
            {
                Debug.Log("Failed to join room");
            }
        }

        public override void OnConnectedToMaster()
        {
            if (joinRandomRoom)
            {
                Debug.Log("Connected to master");
                Debug.Log("Joining random room...");
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinedRoom()
        {

            if (playerAvatar == null)
            {
                Debug.LogError("MyNetworkManager is missing a reference to the player avatar prefab!");
            }

            var idx = PhotonNetwork.otherPlayers.Length;
            Debug.Log(PhotonNetwork.playerList[idx] + " joined room");
            photonView.RPC("NewPlayer", PhotonNetwork.playerList[idx], idx + 1, playerAvatar);
        }


        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Debug.Log(newPlayer + " connected");
        }

        public void UpdateSpawns()
        {
            spawns = ConvertToDictionary(GameObject.FindGameObjectsWithTag("Respawn"));
            if (spawns.Count == 0)
            {
                Debug.LogError("no spawning point defined! closing play mode!");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        [PunRPC]
        public void NewPlayer(int idx, GameObject avatar)
        {
            // Create a new player at the appropriate spawn spot
            if (!spawns.ContainsKey(idx))
            {
                idx = (spawns.Count % idx) + 1;
            }

            var trans = spawns[idx].transform;
            var player = PhotonNetwork.Instantiate(avatar.name, trans.position, trans.rotation, 0);
            player.name = "Player " + (idx);

        }


        private SortedDictionary<int, GameObject> ConvertToDictionary(Array array)
        {
            SortedDictionary<int, GameObject> dict = new SortedDictionary<int, GameObject>();
            foreach (GameObject element in array)
            {
                String[] temp = element.name.Split(' ');
                dict.Add(Int32.Parse(temp[2]), element);
            }

            return dict;
        }
    }
    
}


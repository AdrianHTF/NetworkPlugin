using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace SA
{
    public class ConnectMenu : EditorWindow
    {



        [MenuItem("Network/Setup Basic Network", false, 1)]
        private static void NewMenuItemObject()
        {
            bool option = EditorUtility.DisplayDialog("Setup basic connection?",
                   "Are you sure that you want to setup basic connection scripts?", "OK", "Cancle");
            if (option)
            {
                GameObject connector;
                connector = new GameObject("CONNECT");
                connector.AddComponent<PhotonView>();
                connector.AddComponent<PlayerManager>();
                connector.AddComponent<ShowStatusWhenConnecting>();
                connector.GetComponent<PlayerManager>().playerAvatar = (GameObject)Resources.Load("PlayerAvatar", typeof(GameObject));
                connector.AddComponent<PhotonVoiceSettings>();
                connector.GetComponent<PhotonVoiceSettings>().VoiceDetection = true;
            }
            else
                Debug.Log("Cancle was choosen");
        }
    }
}

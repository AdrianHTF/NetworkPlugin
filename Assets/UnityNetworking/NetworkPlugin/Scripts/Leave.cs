using UnityEngine;
using VRTK;
using System.Collections;

public class Leave : MonoBehaviour
{
    public UnityEngine.Object Lobby;

    void Start()

    { 
        if (GetComponent<VRTK_InteractableObject>() == null)
        {
            Debug.LogError("Leave is required to be attached to an Object that has the VRTK_InteractableObject script attached to it");
            return;
        }
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);

    }

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(Lobby.name);
    }
}

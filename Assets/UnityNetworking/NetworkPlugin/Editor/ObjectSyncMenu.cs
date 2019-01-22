using UnityEngine;
using UnityEditor;
using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NetVRTK;
using NetBase;

public class ObjectSyncMenu : EditorWindow
{
    Vector2 scrollPos;
    [MenuItem("Network/Object Sync", false, 33)]
    private static void NewMenuItemObject()
    {
        ObjectSyncMenu window = (ObjectSyncMenu)EditorWindow.GetWindow(typeof(ObjectSyncMenu));

        window.minSize = new Vector2(410f, 250f);
        window.maxSize = new Vector2(410f, 250f);

        window.autoRepaintOnSceneChange = true;
        window.titleContent.text = "Object Sync";
        window.Show();


    }
    
   /*
   *
   *
   *
   */
    private void OnGUI()
    {


        EditorGUILayout.LabelField("");        EditorGUILayout.LabelField("Setup game object", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("-------------------------------------------------------------------------");


        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Adding photon components", GUILayout.Width(200));
        

        if (GUILayout.Button("setup object(s)", GUILayout.Height(40), GUILayout.Width(170)))
        {
            SetupObject();
            SetupObservedComponents();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("");

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Adding photon/vrtk components", GUILayout.Width(200));


        if (GUILayout.Button("setup interactable object(s)", GUILayout.Height(40), GUILayout.Width(170)))
        {
            SetupInteractableObject();
            SetupObservedComponents();
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Label("");
        
        EditorGUILayout.LabelField("-------------------------------------------------------------------------");
        GUILayout.Label("");
        //EditorGUILayout.LabelField("For adding observed components to the PhotonView it's sometimes nece");


    }

    /*
    * Add relevant components to selected game objects
    */


    private void SetupObject()
    {
        Transform[] transforms = Selection.transforms;
        foreach (Transform currentTransform in transforms)
        {
            Debug.Log(message: currentTransform);
            SetupNetworkObject(currentTransform);
        }
    }

    private void SetupInteractableObject()
    {
        Transform[] transforms = Selection.transforms;
        foreach (Transform currentTransform in transforms)
        {
            Debug.Log(message: currentTransform);
            SetupNetworkObject(currentTransform);
            SetupNetworkGrabManager(currentTransform);
            SetupSnapManager(currentTransform);
        }
    }

    /*
    * Add PhotonView to component
    */
    private void SetupPhotonView(Transform currentTransform)
    {
        PhotonView transform = currentTransform.GetComponentInChildren<PhotonView>();
        if (transform == null)
            currentTransform.gameObject.AddComponent<PhotonView>();
       
    }

    /*
    * Add NetworkObject to component
    */
    private void SetupNetworkObject(Transform currentTransform)
    {
        NetworkObject transform = currentTransform.GetComponentInChildren<NetworkObject>();
        if (transform == null)
            currentTransform.gameObject.AddComponent<NetworkObject>();
        
    }


    /*
    * Add NetworkGrabManager to component
    */
    private void SetupNetworkGrabManager(Transform currentTransform)
    {
        NetworkGrabManager transform = currentTransform.GetComponentInChildren<NetworkGrabManager>();
        if (transform == null)
            currentTransform.gameObject.AddComponent<NetworkGrabManager>();
    }


    /*
    * Add SnapManager to component
    */
    private void SetupSnapManager(Transform currentTransform)
    {
        NetworkSnapManager transform = currentTransform.GetComponentInChildren<NetworkSnapManager>();
        if (transform == null)
            currentTransform.gameObject.AddComponent<NetworkSnapManager>();
    }



    /*
    * Adding observable components to PhotonView
    */
    private void SetupObservedComponents()
    {
        Transform[] transforms = Selection.transforms;
        foreach (Transform currentTransform in transforms)
        {
            PhotonView photonView = currentTransform.GetComponentInChildren<PhotonView>();
            photonView.ObservedComponents = new List<Component>();
            photonView.ownershipTransfer = OwnershipOption.Takeover;
            photonView.synchronization = ViewSynchronization.UnreliableOnChange;
            photonView.ObservedComponents.Add(currentTransform.GetComponentInChildren<NetworkObject>());
            
        }
    }
}



using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class PlayerSyncMenu : EditorWindow {

    public string xCord = "0";
    public string yCord = "0";
    public string zCord = "0";
    private Vector2 scrollPos;
    private GameObject spawnPosition;
    private SortedDictionary<int, GameObject> spawns;

    [MenuItem("Network/Spawn Positions", false, 32)]
    private static void NewMenuItemPlayer()
    {
        PlayerSyncMenu window = (PlayerSyncMenu)EditorWindow.GetWindow(typeof(PlayerSyncMenu));

        window.minSize = new Vector2(300f, 370f);
        window.maxSize = new Vector2(400f, 400f);

        window.autoRepaintOnSceneChange = true;
        window.titleContent.text = "Player Sync";
        window.Show();

    }

    private void OnGUI()
    {
        var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
        EditorGUILayout.LabelField("");

        EditorGUILayout.LabelField("Add new spawn position", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("-------------------------------------------------------------------------");

        EditorGUILayout.BeginHorizontal();
        

        EditorGUILayout.LabelField("X-Coordinate", EditorStyles.boldLabel);
        xCord = EditorGUILayout.TextField(xCord).Replace(",", ".");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Y-Coordinate", EditorStyles.boldLabel);
        yCord = EditorGUILayout.TextField(yCord).Replace(",", ".");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Z-Coordinate", EditorStyles.boldLabel);
        zCord = EditorGUILayout.TextField(zCord).Replace(",", ".");
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("add", GUILayout.Height(40), GUILayout.Width(100)))
        {
            if (regex.IsMatch(xCord) && regex.IsMatch(yCord) && regex.IsMatch(zCord))
            {
                spawns = ConvertToDictionary(GameObject.FindGameObjectsWithTag("Respawn"));
                GameObject temp = new GameObject("Spawn Position " + (spawns.Count + 1));
                temp.transform.position = new Vector3(float.Parse(xCord), float.Parse(yCord), float.Parse(zCord));
                temp.tag = "Respawn";
                temp.AddComponent<NetworkStartPosition>();
                spawnPosition = temp;
            }
            else
                EditorUtility.DisplayDialog("Error", "No valid coordinate!", "OK");
           

        }

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("List of spawn positions", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("-------------------------------------------------------------------------");
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(400), GUILayout.Height(200));
        spawns = ConvertToDictionary(GameObject.FindGameObjectsWithTag("Respawn"));
        foreach (KeyValuePair<int, GameObject> entry in spawns)
        {
            
            EditorGUILayout.BeginHorizontal();
            string buttonName = "Player " + (entry.Key) + ": " + entry.Value.transform.position;
            if (GUILayout.Button(buttonName, GUILayout.Width(300)))
            {
                Selection.activeGameObject = entry.Value;

            }
            
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {

                bool option = EditorUtility.DisplayDialog("Remove spawnpoint?",
                "Are you sure you want to remove selected spawnpoint from list?", "OK", "Cancle");
                
                if (option)
                {
                    DestroyImmediate(entry.Value);
                    spawns = ConvertToDictionary(GameObject.FindGameObjectsWithTag("Respawn"));
                    RenameSpawnPoints();
                }
                else
                    Debug.Log("Cancle was choosen"); 
                
                
            }
            EditorGUILayout.EndHorizontal();
        }
            EditorGUILayout.EndScrollView();
    }

    private void RenameSpawnPoints()
    {
        SortedDictionary<int, GameObject> temp = new SortedDictionary<int, GameObject>();
        int i = 1;

        foreach (KeyValuePair<int, GameObject> entry in spawns)
        {
            temp[i] = entry.Value;
            i++;
        }
        spawns = temp;
        foreach (KeyValuePair<int, GameObject> entry in spawns)
        {
            spawns[entry.Key].name = "Spawn Position " + (entry.Key);        }


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




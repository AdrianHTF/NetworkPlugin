using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : EditorWindow
{
    [MenuItem("Network/Help", false, 51)]
    private static void NewMenuItemHelp()
    {
        Application.OpenURL("file://C:/Users/trbea/OneDrive/BachelorArbeit/Unity/VRDemo/Assets/NetworkPlugin/new.html");
    }
}


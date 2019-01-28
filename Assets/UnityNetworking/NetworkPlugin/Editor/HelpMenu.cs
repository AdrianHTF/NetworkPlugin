using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : EditorWindow
{
    [MenuItem("Network/Help", false, 51)]
    private static void NewMenuItemHelp()
    {
        Application.OpenURL("https://github.com/AdrianHTF/NetworkPlugin");
    }
}


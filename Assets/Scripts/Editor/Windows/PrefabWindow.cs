using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabWindow : EditorWindow
{
    //List<string> prefabName = new List<string>(); No pudimos pasarle el index en el popUp, por eso usamos array, pero no es la idea que buscamos.
    //public List<string> pName = new List<string>() { "A", "B", "C", "D" };
    string[] prefabName = {"Pared", "Final", "Cruce", "ParedConcava", "PisoTecho", "PuertaAbierta"};
    public static string prefab;
    int prefabIndex;
    Vector2 _scrollPos;

    [MenuItem("Level Windows/Prefab Editor")]
    static void CreateWindow()
    {
        ((PrefabWindow)GetWindow(typeof(PrefabWindow))).Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, false);
        prefabIndex = EditorGUILayout.Popup("Prefab", prefabIndex, prefabName);
        prefab = prefabName[prefabIndex];
        Repaint();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}

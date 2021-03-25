﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelCreator : EditorWindow
{
    //List<string> prefabName = new List<string>(); No pudimos pasarle el index en el popUp, por eso usamos array, pero no es la idea que buscamos.
    //public List<string> pName = new List<string>() { "A", "B", "C", "D" };
    string[] prefabName = {"Pared", "Cruce", "Puerta"};
    public static string prefab;
    public static string assetPath;
    int prefabIndex;
    Vector2 _scrollPos;
    Vector2 _scroll;
    public static GameObject selectedObject = null;

    [MenuItem("Level Windows/Level Creator")]
    static void CreateWindow()
    {
        ((LevelCreator)GetWindow(typeof(LevelCreator))).Show();
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

        
        string[] assetPaths = AssetDatabase.FindAssets(prefab);
        Debug.Log(prefab);

        for (int i = 0; i < assetPaths.Length; i++)
        {
            assetPaths[i] = AssetDatabase.GUIDToAssetPath(assetPaths[i]);
            //assetPath = assetPaths[i];
        }

        GameObject[] assets = new GameObject[assetPaths.Length];
        for (int i = 0; i < assetPaths.Length; i++)
        {
            assets[i] = AssetDatabase.LoadAssetAtPath<GameObject>(assetPaths[i]);
        }
        _scroll = EditorGUILayout.BeginScrollView(_scroll);
        EditorGUILayout.BeginHorizontal();
        foreach (GameObject go in assets)
        {
            GUI.color = (go == selectedObject) ? Color.green : Color.white;
            GUIContent cont = new GUIContent(AssetPreview.GetAssetPreview(go));

            if (GUILayout.Button(cont))
            {
                selectedObject = go;
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
        GUI.color = Color.white;
        if (GUILayout.Button("Create"))
        {
            var go = Instantiate(selectedObject, Vector3.zero, Quaternion.identity);
        }
    }
}

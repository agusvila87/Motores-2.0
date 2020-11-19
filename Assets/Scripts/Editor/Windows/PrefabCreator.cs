using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabCreator : EditorWindow
{
    string[] prefabName = { "Pared", "Cruce", "Puerta" };
    public static string prefab;
    public static string assetPath;
    int prefabIndex;

    GameObject emptyPrefab = null;
    string emptyPrefabPath;
    string _prefabName;

    [MenuItem("Level Windows/Prefab Creator")]
    static void CreateWindow()
    {
        ((PrefabCreator)GetWindow(typeof(PrefabCreator))).Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        emptyPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", emptyPrefab, typeof(GameObject), false);
        if (emptyPrefab == null)
        {
            EditorGUILayout.HelpBox("Se necesita un Prefab", MessageType.Error);
            GUI.enabled = false;
        }


        //_prefabName =

        prefabIndex = EditorGUILayout.Popup("Prefab Type", prefabIndex, prefabName);
        prefab = prefabName[prefabIndex];

        if (GUILayout.Button("Create"))
        {
            assetPath = AssetDatabase.GetAssetPath(emptyPrefab);
            FileUtil.MoveFileOrDirectory(assetPath, "Assets/Resources/Prefabs/"+ emptyPrefab.name+".prefab");
            AssetDatabase.Refresh(); //Aca refrescamos porque sino teniamos que minimizar.
        }
        EditorGUILayout.EndVertical();
    }
}

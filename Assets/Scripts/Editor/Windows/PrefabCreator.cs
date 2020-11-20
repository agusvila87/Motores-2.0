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

    string[] pivotPaths;

    GameObject emptyPrefab = null;
    string emptyPrefabPath;
    string _prefabName;
    PrefabTile tileScript;
    GameObject _forward, _back, _left, _right;

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

            prefabIndex = EditorGUILayout.Popup("Prefab Type", prefabIndex, prefabName);
            prefab = prefabName[prefabIndex];

        //ACA QUERIAMOS QUE CUANDO SE APRETE EL BOTÓN SE AGREGE EL SCRIPT, YA LO SOLUCIONAMOS.
        //tileScript = (PrefabTile)EditorGUILayout.ObjectField("Select Tile Script", tileScript, typeof(PrefabTile), false);
        //if (tileScript == null)
        //{
        //    EditorGUILayout.HelpBox("Se necesita el script Tile", MessageType.Error);
        //    GUI.enabled = false;
        //}

        //ACA QUERIAMOS QUE EL USUARIO AGREGE LOS PIVOTS PERO NO SE PUEDE AGREGAR ALGO QUE NO ES PREFAB.
        //_forward = (GameObject)EditorGUILayout.Field("Forward Pivot", _forward, typeof(GameObject), false);
        //_back = (GameObject)EditorGUILayout.ObjectField("Back Pivot", _back, typeof(GameObject), false);
        //_right = (GameObject)EditorGUILayout.ObjectField("Right Pivot", _right, typeof(GameObject), false);
        //_left = (GameObject)EditorGUILayout.ObjectField("Left Pivot", _left, typeof(GameObject), false);

        //if (GUILayout.Button("Set Pivots"))
        //{
        //    emptyPrefab.GetComponent<PrefabTile>().forward.transform.position = _forward.transform.position;
        //    emptyPrefab.GetComponent<PrefabTile>().back.transform.position = _back.transform.position;
        //    emptyPrefab.GetComponent<PrefabTile>().right.transform.position = _right.transform.position;
        //    emptyPrefab.GetComponent<PrefabTile>().left.transform.position = _left.transform.position;
        //}


        if (emptyPrefab.GetComponent<PrefabTile>() == null)
        {
            if (GUILayout.Button("Add Component Tile"))
            {
                //_forward = Resources.Load()
                emptyPrefab.AddComponent<PrefabTile>();

                _forward.transform.SetParent(emptyPrefab.transform);
                //GameObject Foward = new GameObject();
                //Foward.name = "Foward";
                //Foward.transform.SetParent(emptyPrefab.transform);
            }
            EditorGUILayout.HelpBox("Se necesita agregar el componente PrefabTile", MessageType.Warning);
            //GUI.enabled = false;
        }

        if (GUILayout.Button("Create"))
        {
            emptyPrefab.name = emptyPrefab.name + " " +prefab;
            assetPath = AssetDatabase.GetAssetPath(emptyPrefab);
            FileUtil.MoveFileOrDirectory(assetPath, "Assets/Resources/Prefabs/"+ emptyPrefab.name+".prefab");
            
            AssetDatabase.Refresh(); //Aca refrescamos porque sino teniamos que minimizar.
        }
        EditorGUILayout.EndVertical();
    }
}

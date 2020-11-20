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
    //GameObject _forward, _back, _left, _right;

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

        prefabIndex = EditorGUILayout.Popup("Tipo de Prefab", prefabIndex, prefabName);
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


        //if (emptyPrefab !=null)
        //{
        //    if(emptyPrefab.GetComponent<PrefabTile>() == null)
        //    {
        //        if (GUILayout.Button("Agregar Componente PrefabTile"))
        //        {
        //            emptyPrefab.AddComponent<PrefabTile>();
        //            //GameObject Foward = new GameObject();
        //            //Foward.name = "Foward";
        //            //Foward.transform.SetParent(emptyPrefab.transform);
        //        }
        //        else
        //        {
        //            EditorGUILayout.HelpBox("Se necesita agregar el componente PrefabTile", MessageType.Warning);
        //        }
        //    }
        //}

        if (GUILayout.Button("Crear Prefabs"))
        {
            var PrefabInstantiate = Instantiate(emptyPrefab);
            PrefabInstantiate.name = emptyPrefab.name + " " + prefab;
            PrefabInstantiate.AddComponent<PrefabTile>();
            PrefabInstantiate.transform.position = Vector3.zero;
            GameObject Foward = new GameObject();
            Foward.name = "Foward";
            Foward.transform.SetParent(PrefabInstantiate.transform);
            GameObject Back = new GameObject();
            Back.name = "Back";
            Back.transform.SetParent(PrefabInstantiate.transform);
            GameObject Right = new GameObject();
            Right.name = "Right";
            Right.transform.SetParent(PrefabInstantiate.transform);
            GameObject Left = new GameObject();
            Left.name = "Left";
            Left.transform.SetParent(PrefabInstantiate.transform);

            //assetPath = AssetDatabase.GetAssetPath(emptyPrefab);
            assetPath = "Assets/Resources/Prefabs/" + PrefabInstantiate.name + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(PrefabInstantiate, assetPath);
            //FileUtil.MoveFileOrDirectory(assetPath, "Assets/Resources/Prefabs/"+ emptyPrefab.name+".prefab");
            DestroyImmediate(PrefabInstantiate);
            AssetDatabase.Refresh(); //Aca refrescamos porque sino teniamos que minimizar.
        }

        GUILayout.Space(100);
        EditorGUILayout.HelpBox("Recordatorio: Una ves creado el Prefab y acomodar los pivots, tambien deberas guiarlos en el Script: PrefabTile",MessageType.Info);
        GUILayout.Space(1);
        EditorGUILayout.HelpBox("El Prefab creado lo podes encontrar en /Resources/Prefabs/ ", MessageType.Info);
        EditorGUILayout.EndVertical();
    }
}

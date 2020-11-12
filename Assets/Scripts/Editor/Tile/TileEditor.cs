using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.VersionControl;

[CustomEditor(typeof(Tile))]
public class TileEditor : Editor
{
    private Tile tgt;

    private void OnEnable()
    {
        tgt = (Tile)target;

    }

    void OnSceneGUI()
    {
        Handles.BeginGUI();
        var addValue = 200 / Vector3.Distance(Camera.current.transform.position, tgt.transform.position);
        CreateButton(tgt.currentTileName.ToString(), tgt.transform.position, Vector3.zero, Vector3.zero);
        RotateButton("o", tgt.transform.position, tgt.transform);
        if (tgt.forward && (!Physics.Raycast(tgt.transform.position, tgt.transform.forward, 20, layerMask: 9)))
        {
            CreateButton("+", tgt.forward.transform.position * 2, tgt.forward.transform.forward, tgt.forward.transform.position);
        }
        if (tgt.right && (!Physics.Raycast(tgt.transform.position, tgt.transform.right, 20, layerMask: 9)))
        {
            CreateButton("+", tgt.right.transform.position * 2, tgt.right.transform.forward, tgt.right.transform.position);
        }
        if (tgt.left && (!Physics.Raycast(tgt.transform.position, -tgt.transform.right, 20, layerMask: 9)))
        { 
            CreateButton("+", tgt.left.transform.position * 2, tgt.left.transform.forward, tgt.left.transform.position);
        } 
        Handles.EndGUI();
    }
   

    private void CreateButton(string text, Vector3 pos, Vector3 dir, Vector3 nPos)
    {
        var _pos = Camera.current.WorldToScreenPoint(pos);
        var size = 1000 / Vector3.Distance(Camera.current.transform.position, pos);
        var rect = new Rect(_pos.x - size / 2, Screen.height - _pos.y - size, size, size /2);

        if (GUI.Button(rect, text))
        {
            if (PrefabWindow.selectedObject != null)
            {
                Debug.Log(PrefabWindow.selectedObject.name);
                var t = (Tile)Resources.Load("Prefabs/" + PrefabWindow.selectedObject.name, typeof(Tile));
                var _tile = Instantiate(t);
                t.transform.forward = dir;
                t.transform.position = nPos + (t.transform.forward.normalized * Vector3.Distance(t.back.transform.position, t.transform.position));
                Selection.activeObject = t;
                SceneView.lastActiveSceneView.LookAt(t.transform.position);
            }
            else
            {
                EditorWindow.GetWindow<PrefabWindow>().Show();
            }
        }
    }

    private void RotateButton(string text, Vector3 pos, Transform rot)
    {
        var _pos = Camera.current.WorldToScreenPoint(pos);
        var size = 1500 / Vector3.Distance(Camera.current.transform.position, pos);
        var rect = new Rect(_pos.x - size / 2, Screen.height - _pos.y - size, size/4, size / 4);

        if (GUI.Button(rect, text))
        {
            Tile t = new Tile();
            switch (PrefabWindow.prefab)
            {
                case "Pared":
                    t = (Tile)Resources.Load("Prefabs/Pared", typeof(Tile));
                    break;
                case "Final":
                    t = (Tile)Resources.Load("Prefabs/Final", typeof(Tile));
                    break;
                case "Cruce":
                    t = (Tile)Resources.Load("Prefabs/Cruce", typeof(Tile));
                    break;
                case "ParedConcava":
                    t = (Tile)Resources.Load("Prefabs/ParedConcava", typeof(Tile));
                    break;
                case "PisoTecho":
                    t = (Tile)Resources.Load("Prefabs/PisoTecho", typeof(Tile));
                    break;
                case "PuertaAbierta":
                    t = (Tile)Resources.Load("Prefabs/PuertaAbierta", typeof(Tile));
                    break;
            }
            rot.localEulerAngles += new Vector3(0, 90, 0);
        }
    }
}

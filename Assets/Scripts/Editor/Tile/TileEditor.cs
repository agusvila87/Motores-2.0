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
        //var addValue = 300 / Vector3.Distance(Camera.current.transform.position, tgt.transform.position);

        //var _pos = Camera.current.WorldToScreenPoint(tgt.transform.position);
        //var size = 2000 / Vector3.Distance(Camera.current.transform.position, tgt.transform.position);
        //var r = new Rect(_pos.x, Screen.height - _pos.y - 100, 45, 45);
        //GUI.Button(r, tgt.currentTileName.ToString());
        //CreateButton(tgt.currentTileName.ToString(), tgt.transform.position, Vector3.zero, Vector3.zero);
            RotateButton("o", tgt.transform.position, tgt.transform);
        if (tgt.forward && (!Physics.Raycast(tgt.transform.position, tgt.transform.forward, 20, layerMask: 9)))
        {
            CreateButton("+", tgt.forward.transform.position * 1, tgt.forward.transform.forward);
        }
        if (tgt.right && (!Physics.Raycast(tgt.transform.position, tgt.transform.right, 20, layerMask: 9)))
        {
            CreateButton("+", tgt.right.transform.position * 1, tgt.right.transform.forward);
        }
        if (tgt.left && (!Physics.Raycast(tgt.transform.position, -tgt.transform.right, 20, layerMask: 9)))
        { 
            CreateButton("+", tgt.left.transform.position *1, tgt.left.transform.forward);
        } 
        Handles.EndGUI();
    }
   

    private void CreateButton(string text, Vector3 pos, Vector3 dir) //el npos es al pedo 
    {
        var _pos = Camera.current.WorldToScreenPoint(pos);
        var size = 2000 / Vector3.Distance(Camera.current.transform.position, pos);
        var r = new Rect(_pos.x, Screen.height -_pos.y - 50, 45, 45);
        if (GUI.Button(r, text))
        {
            if (PrefabWindow.selectedObject != null)
            {
                var t = (Tile)Resources.Load("Prefabs/" + PrefabWindow.selectedObject.name, typeof(Tile));
                var _tile = Instantiate(t);
                _tile.transform.forward = dir;
                Vector3 TPos = _tile.transform.forward.normalized * Vector3.Distance(_tile.back.transform.position, _tile.transform.position);
                _tile.transform.position = pos + TPos;

                Selection.activeGameObject = _tile.gameObject;
                SceneView.lastActiveSceneView.LookAt(_tile.transform.position);
            }
            else
            {
                EditorWindow.GetWindow<PrefabWindow>().Show();
            }
        }
    }

    private void RotateButton(string text, Vector3 pos, Transform rot)
    {
        //var _pos = Camera.current.WorldToScreenPoint(pos);
        //var size = 1500 / Vector3.Distance(Camera.current.transform.position, pos);
        //var rect = new Rect(_pos.x - size / 2, Screen.height - _pos.y - size, 20, 20);
        var _pos = Camera.current.WorldToScreenPoint(tgt.transform.position);
        var size = 2000 / Vector3.Distance(Camera.current.transform.position, tgt.transform.position);
        var r = new Rect(_pos.x - 50, Screen.height - _pos.y - 100, 45, 45);
        if (GUI.Button(r, text))
        {
            rot.localEulerAngles += new Vector3(0, 90, 0);
        }
    }
}

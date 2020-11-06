using UnityEngine;
using System.Collections;
using UnityEditor;

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
        CreateButton(tgt.currentTileName.ToString(), tgt.transform.position, Vector3.zero, Vector3.zero);
        RotateButton("o", tgt.transform.position, tgt.transform);
        var addValue = 250 / Vector3.Distance(Camera.current.transform.position, tgt.transform.position);
        if (tgt.forward && (!Physics.Raycast(tgt.transform.position, tgt.transform.forward, 20, layerMask: 9)))
        {
            CreateButton("+", tgt.forward.transform.position, tgt.forward.transform.forward, tgt.forward.transform.position);
        }
        //if (tgt.back && (Physics.Raycast(tgt.transform.position, -tgt.transform.forward, 20)))
        //{
        //    CreateButton("+", -tgt.transform.forward * addValue, tgt.back.transform.forward, tgt.back.transform.position);         
        //}
        if (tgt.right && (!Physics.Raycast(tgt.transform.position, tgt.transform.right, 20, layerMask: 9)))
        {
            CreateButton("+", tgt.right.transform.position, tgt.right.transform.forward, tgt.right.transform.position);
        }
        if (tgt.left && (!Physics.Raycast(tgt.transform.position, -tgt.transform.right, 20, layerMask: 9)))
        { 
            CreateButton("+", tgt.left.transform.position, tgt.left.transform.forward, tgt.left.transform.position);
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
            Tile t = new Tile();
            if (t != null)
            {
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
                Tile _tile = Instantiate(t);
                _tile.transform.forward = dir;
                _tile.transform.position = nPos + (_tile.transform.forward.normalized * Vector3.Distance(_tile.back.transform.position, _tile.transform.position));
                Selection.activeObject = _tile;
                SceneView.lastActiveSceneView.LookAt(_tile.transform.position);
            }
            else
            {
                EditorGUILayout.LabelField("Necesitas ir a LevelWindows/Prefab Editor", EditorStyles.wordWrappedLabel);
                GUILayout.Space(20);
                if (GUILayout.Button("Ya capté la movida pá"))
                {

                }


            }
        }
    }

    private void RotateButton(string text, Vector3 pos, Transform rot)
    {
        var _pos = Camera.current.WorldToScreenPoint(pos);
        var size = 2000 / Vector3.Distance(Camera.current.transform.position, pos);
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

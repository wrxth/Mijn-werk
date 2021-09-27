using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmos : MonoBehaviour
{

    
    public static void DrawRay(Vector3 _pos, Vector3 _dir, Color _color)
    {
        if (_dir.sqrMagnitude > 0.001f)
        {
            Debug.DrawRay(_pos, _dir, _color);
            //UnityEditor.Handles.color = _color;
            //UnityEditor.Handles.DrawSolidDisc(_pos + _dir, Vector3.up, 0.25f);
        }
    }
    public static void DrawLabel(Vector3 _pos, string _label, Color _color)
    {
        //UnityEditor.Handles.BeginGUI();
        //UnityEditor.Handles.color = _color;
        //UnityEditor.Handles.Label(_pos, _label);
        //UnityEditor.Handles.EndGUI();
    }
}

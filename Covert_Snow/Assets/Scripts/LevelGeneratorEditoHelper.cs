using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditoHelper : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelGenerator lg = (LevelGenerator)target;
        if(GUILayout.Button("Generate Level"))
        {
            lg.GenerateLevel();
        }
    }
}

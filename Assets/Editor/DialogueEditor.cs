
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(DialogueScriptableObject))]
public class DialogueEditor : Editor
{
    string _basePath = "Assets/SerializedDialogs/";
    SerializedProperty _dialogues;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Separator();

        if (GUILayout.Button("Save to json"))
        {
            // SerializedProperty items = 
            string json = JsonUtility.ToJson(((DialogueScriptableObject)target));
            var date = DateTime.Now;
            var path = _basePath + target.name + "_" + date.ToString("yyyy_MM_dd__HH_mm_ss") + ".json";
            Debug.Log(json);
            StreamWriter writer = new StreamWriter(path);
            writer.Write(json);
            writer.Close();
        }

        var jsonFiles = Directory.EnumerateFiles(_basePath, "*.json");

        foreach (var item in jsonFiles)
        {
            if (GUILayout.Button("Load " + item))
            {
                StreamReader reader = new StreamReader(item);
                var json = reader.ReadToEnd();
                reader.Close();
                JsonUtility.FromJsonOverwrite(json, target);
            }
        }
    }
}

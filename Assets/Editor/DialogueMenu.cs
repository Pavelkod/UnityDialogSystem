using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class DialogueMenu : MonoBehaviour
{
    [MenuItem("Dialogue system/EditDialogue")]
    private static void SelectDilogueEditor()
    {
        var path = Application.dataPath + "/Dialogues/";
        var file = Directory.EnumerateFiles(path, "*.asset").FirstOrDefault();
        if (string.IsNullOrEmpty(file))
        {
            Debug.Log("Can't find Dialogue asset. Please make new one through create menu in project window.");
            return;
        }
        var obj = AssetDatabase.LoadMainAssetAtPath("Assets" + file.Substring(Application.dataPath.Length));
        Selection.activeObject = obj;
    }
}

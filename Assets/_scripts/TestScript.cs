using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Dialogue _dialogue;
    void OnGUI()
    {
        if (GUILayout.Button("Spawn Dialogue"))
        {
            _dialogue.LoadDialogue();
            // Debug.Log("Dialog pushed");
        }
    }
}

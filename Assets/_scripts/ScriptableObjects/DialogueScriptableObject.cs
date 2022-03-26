using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public List<DialogueElement> DialogueElements;
}

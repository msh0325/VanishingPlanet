using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class DialogueData : ScriptableObject
{
    [TextArea(3,5)] public string[] dialogLines;
    [TextArea] public string[] selectLines;
}

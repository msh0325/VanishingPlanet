using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class DialogueData : ScriptableObject
{
    [Header("플레이어 지문")]
    [TextArea(3,5)] public string[] dialogLines;
    [Header("선택지 지문")]
    [TextArea] public string[] selectLines;
    [Header("탐험(Explore) = 1 / 방치(neglect) = 2")]
    public int[] points;
}

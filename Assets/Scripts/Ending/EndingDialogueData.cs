using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEndingDialogue",menuName = "Dialogue/EndingDialogue")]
public class EndingDialogueData : ScriptableObject
{
    [Header("건물 선택지 지문")]
    [TextArea] public string[] buildingLines;
    [Header("이동수단 선택지 지문")]
    [TextArea] public string[] carLines;
    [Header("식물 선택지 지문")]
    [TextArea] public string[] plantLines;
    [Header("전쟁 선택지 지문")]
    [TextArea] public string[] gunLines;
    [Header("해골 선택지 지문")]
    [TextArea] public string[] skullLines;
    [Header("결과 지문")]
    [TextArea] public string[] resultLine;
}

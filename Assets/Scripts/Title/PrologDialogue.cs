using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPrologDialog",menuName = "Dialogue/Prolog")]

public class PrologDialogue : ScriptableObject
{
    [Header("프롤로그 스크립트 이미지")]
    public Sprite[] faces;
    [Header("프롤로그 스크립트")]
    [TextArea] public string[] prologLines;
}

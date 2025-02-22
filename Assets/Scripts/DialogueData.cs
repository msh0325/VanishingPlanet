using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class DialogueData : ScriptableObject
{
    [Header("얼굴 이미지")]
    public Sprite[] sprites;
    [Header("플레이어 지문")]
    [TextArea(3,5)] public string[] dialogLines;
    [Header("선택지 지문")]
    [TextArea] public string[] selectLines;
    [Header("탐험(Explore) = 1 / 방치(neglect) = 2")]
    public int[] points;
    [Header("플레이어가 선택한 선택지 번호")]
    public int selectedNum;
    [Header("미니게임 번호(0~4)")]
    public int number;
    [Header("선택지 이후 지문 이미지")]
    public Sprite[] afterSprites;
    [Header("선택지 이후 지문")]
    [TextArea] public string[] afterLines;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{

    GameManager gm;
    [Header("미니게임 오브젝트")]
    [SerializeField] public GameObject[] miniGameMaps;
    [Header("미니게임 시작 위치")]
    [SerializeField] public Vector3 [] pcPos;
    [SerializeField] public GameObject pc;
    [SerializeField] public GameObject minigame;
    [SerializeField] public GameObject miniCamera;
    [SerializeField] public int num;
    // Start is called before the first frame update
    void Start()
    {
        gm =GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

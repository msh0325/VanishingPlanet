using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{

    GameManager gm;
    [Header("미니게임 맵")]
    [SerializeField] public GameObject[] miniGameMaps;
    [Header("미니게임 오브젝트")]
    [SerializeField] public GameObject[] objects;
    [Header("미니게임 플레이어 시작 위치")]
    [SerializeField] public Vector3 [] pcPos;
    [SerializeField] public GameObject pc;
    [SerializeField] public GameObject minigame;
    [SerializeField] public GameObject miniCamera;
    [SerializeField] public int num;
    [SerializeField] public bool droneON = false;
    public bool isTele = false;
    // Start is called before the first frame update
    void Start()
    {
        gm =GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // 미니게임 플레이할 때 R키를 누르면 맵 초기화
        if(gm.isPlaying && Input.GetKeyDown(KeyCode.R)){
            Debug.Log("reset game");
            pc.GetComponent<MiniGamePC>().ResetPlayer(pcPos[num]);
            objects[num].GetComponent<MiniGameObject>().ResetPos();
            droneON = false;
        }
    }

    public void SetMiniGame(){
        minigame.SetActive(true);
        miniGameMaps[num].SetActive(true);
        pc.transform.localPosition = pcPos[num];
        pc.GetComponent<MiniGamePC>().SaveFirstPos();
        pc.GetComponent<MiniGamePC>().isMoving = false;
        objects[num].GetComponent<MiniGameObject>().SetPos();
    }

    public void EndMiniGame(){
        pc.GetComponent<MiniGamePC>().ResetSprite();
        miniGameMaps[num].SetActive(false);
        minigame.SetActive(false);
    }

}

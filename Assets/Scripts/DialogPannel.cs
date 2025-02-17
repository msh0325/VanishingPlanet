using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPannel : MonoBehaviour
{
    [SerializeField] Button selectbtn1;
    [SerializeField] Button selectbtn2;
    [SerializeField] Button selectbtn3;
    GameManager gm;
    void Start()
    {
        gm = GameManager.instance;
        // 1번 선택지
        selectbtn1.onClick.AddListener(()=>{
            Debug.Log("1번 선택");
            gm.pc.countPoints(0);
            gm.endDialog();
            gm.pc.dialog.selectedNum = 0;
            gm.StartMiniGame(gm.pc.miniNum());
        });

        // 2번 선택지
        selectbtn2.onClick.AddListener(()=>{
            Debug.Log("2번 선택");
            gm.pc.countPoints(1);
            gm.endDialog();
            gm.pc.dialog.selectedNum = 1;
            gm.StartMiniGame(gm.pc.miniNum());
        });

        // 3번 선택지
        selectbtn3.onClick.AddListener(()=>{
            Debug.Log("3번 선택");
            gm.pc.countPoints(2);
            gm.endDialog();
            gm.pc.dialog.selectedNum = 2;
            gm.AfterDialogue(gm.pc.dialog,2);
            //gm.ShowEnding();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

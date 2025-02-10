using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPannel : MonoBehaviour
{
    [SerializeField] Button selectbtn1;
    [SerializeField] Button selectbtn2;
    [SerializeField] Button selectbtn3;
    void Start()
    {
        // 1번 선택지
        selectbtn1.onClick.AddListener(()=>{
            Debug.Log("1번 선택");
            GameManager.instance.pc.countPoints(0);
            GameManager.instance.endDialog();
        });

        // 2번 선택지
        selectbtn2.onClick.AddListener(()=>{
            Debug.Log("2번 선택");
            GameManager.instance.pc.countPoints(1);
            GameManager.instance.endDialog();
        });

        // 3번 선택지
        selectbtn3.onClick.AddListener(()=>{
            Debug.Log("3번 선택");
            GameManager.instance.pc.countPoints(2);
            GameManager.instance.endDialog();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

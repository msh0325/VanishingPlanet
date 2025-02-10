using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // 일정 시간 이후 특정 선택지 나타내기 위한 타이머
    
    public bool isStart = false;
    public float times;

    void Start()
    {
        times = 0f;
    }

    void Update()
    {
        if(isStart){
            times += Time.deltaTime;
            if(times >= 10f){
                GameManager.instance.selectBtn.SetActive(true);
            }
        }
    }
}

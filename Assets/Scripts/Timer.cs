using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // 지금은 한개만 들어있어서 필요하지 않을수도 있지만, 나중을 위해 연습
    // DontDestroyOnLoad에 여러 객체가 있을 때 중복 소환을 방지하기 위한 리스트
    private static List<string> dontDestroyObjs = new List<string>();
    void Awake(){
        // 리스트에 현 게임오브젝트와 이름이 같은것이 있다면 (즉 같은 오브젝트가 있다면) 파괴
        if(dontDestroyObjs.Contains(gameObject.name)){
            Destroy(gameObject);
            return;
        }

        dontDestroyObjs.Add(gameObject.name);        
        DontDestroyOnLoad(gameObject);
    }
}

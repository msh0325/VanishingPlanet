using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBtn : MonoBehaviour
{
    [SerializeField] MiniGameManager miniGM;
    
    // 드론 버튼을 누르면 드론과 조사로봇 둘중 하나를 조사할 수있음
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player"){
            miniGM.droneON = true;
        }
        if(collider.tag == "Drone"){
            miniGM.droneON = false;
        }
    }
}

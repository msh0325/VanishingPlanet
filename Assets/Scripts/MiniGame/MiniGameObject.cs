using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiniGameObject : MonoBehaviour
{
    [SerializeField] public GameObject[] box;
    [SerializeField] public GameObject drone;
    [SerializeField] Vector3 []firstboxPos;
    [SerializeField] Vector3 firstDronePos;


    public void SetPos(){
        if(box == null) return;
        else{
            for(int i=0;i<box.Length;i++){
            firstboxPos[i] = box[i].transform.position;
            }
        }
        if(drone != null){
            firstDronePos = drone.transform.position;
        }
    }

    public void ResetPos(){
        if(box != null){
            for(int i=0;i<box.Length;i++){
            box[i].transform.position = firstboxPos[i];
            }
        }
        if(drone != null){
            drone.transform.position = firstDronePos;
            drone.GetComponent<Drone>().SaveFirstPos();
            drone.GetComponent<Drone>().ResetSprite();
        }
    }
}

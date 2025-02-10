using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMove : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] float xPos, yPos, zPos;
    [SerializeField] float speed = 9f;
    Vector3 Camera_pos;

    void Start(){
        yPos = gameObject.transform.position.y;
        zPos = gameObject.transform.position.z;
    }

    void FixedUpdate(){
        // 나중에 맵을 만든 후 수정하러 오기
        float Xlimit = 2000;
        float Xclamp;

        Xclamp = Mathf.Clamp(target.transform.position.x + xPos,-10,Xlimit);
        Camera_pos = new Vector3(Xclamp,yPos,zPos);

        transform.position = Vector3.Lerp(transform.position,Camera_pos,Time.deltaTime*speed);

    }
}

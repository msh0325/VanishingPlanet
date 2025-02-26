using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float speed = 0.005f;
    private Renderer rend;
    private Vector2 offset = Vector2.zero;
    private float lastTargetX;

    void Start()
    {
        rend = GetComponent<Renderer>();
        lastTargetX = target.position.x;
        offset = rend.material.mainTextureOffset;
    }

    void Update()
    {
        float targetMovement = target.position.x - lastTargetX;
        if(Mathf.Abs(targetMovement)>0.01f){
            offset.x += -targetMovement * speed;
            rend.material.mainTextureOffset = offset;
        }
        lastTargetX = target.position.x;
    }
}

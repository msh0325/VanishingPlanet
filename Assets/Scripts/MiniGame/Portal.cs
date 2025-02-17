using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] MiniGameManager miniGM;
    [SerializeField] GameObject targetPortal;
    public bool onPC = false;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = targetPortal.transform.localPosition;
        Debug.Log(targetPos);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player"&& !(miniGM.isTele)){
            miniGM.isTele = true;
            targetPortal.GetComponent<Portal>().onPC = true;
            collider.gameObject.transform.localPosition = targetPos;
            collider.GetComponent<MiniGamePC>().ResetPlayer(targetPos);
        }   
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player" && onPC){
            miniGM.isTele = false;
            onPC = false;
        }
    }
}

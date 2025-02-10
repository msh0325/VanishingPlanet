using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorePoint : MonoBehaviour
{
    private Player pc;
    public DialogueData dialogueData;

    public void Interact(){
        GameManager.instance.startDialog(dialogueData);
    }

    // 플레이어가 조사 범위 내에 들어옴
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){

            Debug.Log("상호작용 가능");
            pc = collider.gameObject.GetComponent<Player>();
            if(pc != null){
                pc.dialog = dialogueData;
                pc.findPoint = true;
                GameManager.instance.explorePoint = gameObject;
            }
        }
    }

    // 플레이어가 조사 범위 밖으로 나감
    private void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            Debug.Log("상호작용 불가능");
            if(pc != null){
                pc.findPoint = false;
            }
        }
    }
    // 조사 끝낸 포인트 콜라이더 없애서 상호작용 막기
    public void Finish_Explore(){
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}

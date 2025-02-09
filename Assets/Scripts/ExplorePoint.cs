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

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){

            Debug.Log("상호작용 가능");
            pc = collider.gameObject.GetComponent<Player>();
            if(pc != null){
                pc.dialog = dialogueData;
                pc.findPoint = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            Debug.Log("상호작용 불가능");
            if(pc != null){
                pc.findPoint = false;
            }
        }
    }
}

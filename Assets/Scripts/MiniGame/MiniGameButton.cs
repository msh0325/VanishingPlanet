using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameButton : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Sprite opendoor;
    [SerializeField] Sprite closedoor;

    // 플레이어나 박스가 버튼을 눌렀을 때 작동
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" || collider.tag == "Box"){
            Debug.Log("pressed button");
            door.GetComponent<SpriteRenderer>().sprite = opendoor;
            door.GetComponent<Collider2D>().enabled = false;
        }
    }

    // 플레이어나 박스가 버튼을 벗어났을 때 작동
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player" || collider.tag == "Box"){
            Debug.Log("unpressed button");
            door.GetComponent<SpriteRenderer>().sprite = closedoor;
            door.GetComponent<Collider2D>().enabled = true;
        }
    }
}

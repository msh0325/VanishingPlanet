using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float speed = 10.0f;
    Vector2 velocity;
    Rigidbody2D rigid;
    public DialogueData dialog;
    public bool findPoint = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizon = Input.GetAxisRaw("Horizontal");
        velocity = new Vector2(horizon,0);

        if(Input.GetKeyDown(KeyCode.E)&&findPoint){
            Debug.Log("pressed E");
            GameManager.instance.startDialog(dialog);
        }
    }

    void FixedUpdate(){
        rigid.velocity = new Vector2(velocity.x*speed,rigid.velocity.y);
    }

}

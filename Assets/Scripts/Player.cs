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
    public int exploreCount,neglectCount = 0;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 키보드 입력 받아서 스프라이트 뒤집기 & 상호작용 키
        if(!(GameManager.instance.isTalking)){
            float horizon = Input.GetAxisRaw("Horizontal");
            velocity = new Vector2(horizon,0);

            if(Input.GetKeyDown(KeyCode.E)&&findPoint){
                Debug.Log("pressed E");
                GameManager.instance.startDialog(dialog);
            }

            if(horizon > 0){
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            else if(horizon < 0){
                transform.rotation = Quaternion.Euler(0,180,0);
            }
        }
        
    }

    // 플레이어 이동
    void FixedUpdate(){
        if(!(GameManager.instance.isTalking)){
            rigid.velocity = new Vector2(velocity.x*speed,rigid.velocity.y);
        }
        else if(GameManager.instance.isTalking){
            rigid.velocity = Vector2.zero;
        }
    }

    // 엔딩때 사용할 포인트 세기
    public void countPoints(int index){
        if(dialog.points[index] == 1){
            exploreCount +=1;
        }
        else if(dialog.points[index] == 2){
            neglectCount +=1;
        }
    }

}

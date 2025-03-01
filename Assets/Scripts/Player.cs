using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float speed = 10.0f;
    [SerializeField] BackGround bg;
    [SerializeField] Animator animator;
    Vector2 velocity;
    Rigidbody2D rigid;
    public DialogueData dialog;
    public bool findPoint = false;
    public bool onRocket = false;
    public int exploreCount,neglectCount = 0;
    private float horizon;
    GameManager gm;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gm = GameManager.instance;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 키보드 입력 받아서 스프라이트 뒤집기 & 상호작용 키
        if(!(gm.isTalking)&&!(gm.isPlaying)){
            horizon = Input.GetAxisRaw("Horizontal");
            velocity = new Vector2(horizon,0);

            if(Input.GetKeyDown(KeyCode.E)&&findPoint){
                Debug.Log("pressed E");
                gm.startDialog(dialog);
            }

            // 조사하지 않고 로켓 상호작용시 히든엔딩
            // 나중에 추가하고 주석 풀기
            /*if(Input.GetKeyDown(KeyCode.E)&&onRocket&&exploreCount + neglectCount == 0){
                Debug.Log("hidden");
                SceneManager.LoadScene("HiddenEnding");
            }*/

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

        if(!(gm.isTalking)&&!(gm.isPlaying)){
            rigid.velocity = new Vector2(velocity.x*speed,rigid.velocity.y);
            // horizon의 값이 0이 아니면(즉, 움직일 시) true 반환 > run 애니 실행
            // horizon의 값이 0이라면 (즉, 가만히 있을 시) false 반환 > idle 애니 실행
            animator.SetBool("isRun",horizon != 0);
        }
        else if(gm.isTalking||gm.isPlaying){
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

    public int miniNum(){
        return dialog.number;
    }
    public int selectedNum(){
        return dialog.selectedNum;
    }

}

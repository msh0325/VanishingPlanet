using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiniGamePC : MonoBehaviour
{
    [SerializeField] public float speed = 8.0f;
    [SerializeField] public Tilemap obstaclesTile;
    [SerializeField] private Sprite[] pc;
    SpriteRenderer renderer;
    GameManager gm;
    private Vector3 targetPos;
    private GameObject box;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // 움직일 때는 입력 무시
        if(isMoving) return;

        Vector3 direction = Vector3.zero;
        
        // 누르는 키에 따라 방향 정하기
        if(Input.GetKeyDown(KeyCode.W)){
            direction = Vector3.up;
            ChangeSprite(0);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            direction = Vector3.left;
            ChangeSprite(1);
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            direction = Vector3.down;
            ChangeSprite(2);
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            direction = Vector3.right;
            ChangeSprite(3);
        }

        if(direction != Vector3.zero){
            Vector3 newPos = targetPos + direction;

            if(CanMove(newPos)){
                Debug.Log(targetPos);
                targetPos = newPos;
                StartCoroutine(Move());
            }
            else if(CanMoveBox(newPos,direction)){
                Debug.Log("Box Move");
                StartCoroutine(BoxMove(newPos,direction));
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Goal에 닿으면 미니게임 끝
        if(collider.tag == "Goal"){
            gm.EndMiniGame();
        }
    }

    // 플레이어가 움직일 방향으로 움직일 수있는지
    private bool CanMove(Vector3 pos){

        // 혹시 태그 가져올때 하나만 가져올 수도 있으니 overlapboxall로 모든 collider 가져온 후 확인하기
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos,Vector2.one * 0.8f,0f);
        if(colliders.Length == 0) return false;

        foreach(Collider2D col in colliders){
            // tag가 Obstacles면 안움직임
            if(col.tag == "Obstacles" || col.tag == "Box"){
                return false;
            }
        }

        foreach(Collider2D col in colliders){
            // tag가 Ground면 움직임
            if(col.tag == "Ground"){
                return true;
            }
        }
        return false;
    }

    // 플레이어가 움직일 방향에 박스가 있는지
    private bool CanMoveBox(Vector3 pos, Vector3 direct){
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos,Vector2.one * 0.8f,0f);
        if(colliders.Length == 0) return false;

        foreach(Collider2D col in colliders){
            Debug.Log(col.tag);
            if(col.tag == "Box"){
                Vector3 nextBoxPos = pos + direct;
                return CanMove(nextBoxPos);
            }
        }
        return false;
    }

    // 누르는 방향에 따라 스프라이트 변경
    private void ChangeSprite(int num){
        renderer.sprite = pc[num];
    }

    // PC의 초기 위치 저장
    public void SaveFirstPos(){
        targetPos = transform.position;
    }

    private IEnumerator BoxMove(Vector3 playerNewPos, Vector3 direct){
        isMoving = true;

        Vector3 boxNewPos = playerNewPos + direct;

        Collider2D[] hit = Physics2D.OverlapBoxAll(playerNewPos,Vector2.one * 0.8f,0f);
        foreach(Collider2D col in hit){
            if(col.tag == "Box"){
                box = col.gameObject;
            }
        }
        while((box.transform.position - boxNewPos).sqrMagnitude > Mathf.Epsilon){
            box.transform.position = Vector3.MoveTowards(box.transform.position,boxNewPos,speed * Time.deltaTime);
            yield return null;
        }
        box.transform.position = boxNewPos;

        isMoving = false;
    }

    private IEnumerator Move(){
        isMoving = true;

        // Mathf.Epsilon(한없이 0에 가까운 수)
        while((transform.position - targetPos).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position, targetPos,speed * Time.deltaTime);
            yield return null;
        }
        // PC 위치를 정확히 지정
        transform.position = targetPos;
        isMoving = false;
    }
}

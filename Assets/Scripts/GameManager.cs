using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public Player pc;
    [SerializeField] public GameObject dialogUI;
    [SerializeField] public TMP_Text dialogText;
    [SerializeField] public GameObject btnUI;
    [SerializeField] public GameObject selectBtn;
    [SerializeField] public TMP_Text[] btnText;
    [SerializeField] public Button dialogBtn;
    [SerializeField] public Timer timer;
    private string[] currentDialog;
    private int currentIndex;
    public GameObject explorePoint;
    private bool isTyping = false;
    public bool isTalking = false;
    private int endingPoint = 0;
    private Coroutine typeCoroutine;

    void Awake(){
        if (instance == null){
            instance = this;
        }
        dialogUI.SetActive(false);
        btnUI.SetActive(false);

        dialogBtn.onClick.AddListener(()=>{
            ShowNextLine();
        });
    }


    // 스크립트 출력 시작
    public void startDialog(DialogueData dialogueData){
        isTalking = true;
        currentDialog = dialogueData.dialogLines;
        currentIndex = 0;
        dialogUI.SetActive(true);
        for(int i=0;i<3;i++){
            btnText[i].text = dialogueData.selectLines[i];
        }
        ShowNextLine();
    }

    // 스크립트 다음으로 넘기기
    public void ShowNextLine(){

        if(isTyping){
            Debug.Log("typing-click");
            StopCoroutine(typeCoroutine);
            dialogText.text = currentDialog[currentIndex-1];
            isTyping = false;
        }
        else{
                if(currentIndex < currentDialog.Length){
                    typeCoroutine = StartCoroutine(TypeText(currentDialog[currentIndex]));
                    currentIndex++;
                }
        }
        if(currentIndex == currentDialog.Length && !isTyping){
            ShowSelectBtn();
            timer.isStart = true;
            timer.times = 0f;
        }
    }

    // 선택지 버튼 보이기
    public void ShowSelectBtn(){
        btnUI.SetActive(true);
    }

    // 스크립트 끝
    public void endDialog(){
        explorePoint.GetComponent<ExplorePoint>().Finish_Explore();
        explorePoint = null;
        dialogUI.SetActive(false);
        selectBtn.SetActive(false);
        btnUI.SetActive(false);
        timer.isStart = false;
        isTalking = false;
        endingPoint = pc.exploreCount + pc.neglectCount;
        if(endingPoint >= 11){
            ShowEnding();
        }
    }
    
    // 엔딩 포인트 달성 시 엔딩 보여주기
    private void ShowEnding(){
        if(pc.exploreCount > pc.neglectCount) {
            SceneManager.LoadScene("ExploreEnding");
        }
        else if(pc.exploreCount < pc.neglectCount) {
            SceneManager.LoadScene("NeglectEnding");
        }
    }
    
    // 스크립트 글자 하나하나 타이핑하기
    private IEnumerator TypeText(string text){
        isTyping = true;
        dialogText.text = "";

        foreach(char letter in text){
            dialogText.text +=letter;
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
        
    }
}

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
    [SerializeField] public GameObject minigame;
    [SerializeField] public GameObject miniUI;
    [SerializeField] public GameObject miniCamera;
    [SerializeField] public GameObject mainCamera;
    [SerializeField] public TMP_Text[] btnText;
    [SerializeField] public Button dialogBtn;
    [SerializeField] public Timer timer;
    [SerializeField] DialogueData endingDialog;
    [SerializeField] Image blackImg;
    private string[] currentDialog;
    private int currentIndex;
    public GameObject explorePoint;
    private bool isTyping = false;
    public bool isTalking = false;
    public bool isPlaying = false;
    private bool isEnding = false;
    private int endingPoint = 0;
    private Coroutine typeCoroutine;

    void Awake(){
        if (instance == null){
            instance = this;
        }
        dialogUI.SetActive(false);
        btnUI.SetActive(false);
        minigame.SetActive(false);

        dialogBtn.onClick.AddListener(()=>{
            OnDialogClick();
        });
    }


    // 스크립트 출력 시작
    public void startDialog(DialogueData dialogueData){
        isTalking = true;
        currentDialog = dialogueData.dialogLines;
        currentIndex = 0;
        dialogUI.SetActive(true);
        for(int i=0;i<3;i++){
            if(isEnding) break;
            btnText[i].text = dialogueData.selectLines[i];
        }
        ShowNextLine();
    }

    // 대화창 클릭했을 때 스크립트 출력중이면 한번에 출력 & 엔딩이면 화면 어두워지고 씬 전환
    private void OnDialogClick(){
        if(isTyping){
            Debug.Log("typing-click");
            StopCoroutine(typeCoroutine);
            dialogText.text = currentDialog[currentIndex-1];
            isTyping = false;
            ShowSelectBtn();
        }
        else{
            if (!isEnding){
                ShowNextLine();
            }
            else{
                StartCoroutine(FadeWindows(blackImg));
            }
        }
    }

    // 스크립트 다음으로 넘기기 & 마지막 씬일때 선택지 보여주기
    public void ShowNextLine(){
        if (isTyping) return;

        if(currentIndex < currentDialog.Length){
            typeCoroutine = StartCoroutine(TypeText(currentDialog[currentIndex]));
            currentIndex++;
        }
        
    }

    // 선택지 버튼 보이기
    public void ShowSelectBtn(){
        if(currentIndex == currentDialog.Length && !isTyping && !isEnding && !(timer.isStart)){
            btnUI.SetActive(true);
            timer.isStart = true;
            timer.times = 0f;
        }
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

    // 1,2번 선택지 선택 시 미니게임 출력
    public void StartMiniGame(){
        minigame.SetActive(true);
        isPlaying = true;
        miniCamera.SetActive(true);
        mainCamera.SetActive(false);
    }

    // 미니게임 끝날 시 실행
    public void EndMiniGame(){
        minigame.SetActive(false);
        isPlaying=false;
        mainCamera.SetActive(true);
        miniCamera.SetActive(false);
    }
    
    // 엔딩 포인트 달성 시 엔딩 스크립트
    private void ShowEnding(){
        // 마무리 대사
        isEnding = true;
        startDialog(endingDialog);
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
        ShowSelectBtn();
    }
    private IEnumerator FadeWindows(Image img){
        dialogBtn.enabled = false;
        float fadeCount = 0;
        while (fadeCount < 1.0f){
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            img.color = new Color(0,0,0,fadeCount);
        }
        if(pc.exploreCount > pc.neglectCount) {
                SceneManager.LoadScene("ExploreEnding");
            }
            else if(pc.exploreCount < pc.neglectCount) {
                SceneManager.LoadScene("NeglectEnding");
            }
    }
}

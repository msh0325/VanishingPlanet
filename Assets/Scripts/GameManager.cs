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
    [SerializeField] public Image face;
    [SerializeField] public GameObject btnUI;
    [SerializeField] public GameObject selectBtn;
    [SerializeField] public MiniGameManager miniManager;
    [SerializeField] public GameObject mainCamera;
    [SerializeField] public TMP_Text[] btnText;
    [SerializeField] public Button dialogBtn;
    [SerializeField] public Timer timer;
    [SerializeField] DialogueData endingDialog;
    [SerializeField] Image blackImg;
    private string[] currentDialog;
    private Sprite[] sprites;
    private int currentIndex;
    public GameObject explorePoint;
    private bool isTyping = false;
    public bool isTalking = false;
    public bool isPlaying = false;
    private bool isEnding = false;
    private bool isAfter = false;
    private int endingPoint = 0;
    private Coroutine typeCoroutine;

    void Awake(){
        if (instance == null){
            instance = this;
        }
        dialogUI.SetActive(false);
        btnUI.SetActive(false);
        miniManager.minigame.SetActive(false);

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
        sprites = dialogueData.sprites;
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
            if(!isAfter){
                dialogText.text = currentDialog[currentIndex-1];
                isTyping = false;
                ShowSelectBtn();
            }
            else if(isAfter){
                dialogText.text = currentDialog[currentIndex];
                isTyping = false;
            }
        }
        else{
            if (isAfter){
                endDialog();
            }
            else if(!isEnding){
                ShowNextLine();
            }
            else if(isEnding){
                if(currentIndex < currentDialog.Length){
                    ShowNextLine();
                }
                else{
                    StartCoroutine(FadeWindows(blackImg));
                }
                //StartCoroutine(FadeWindows(blackImg));
            }
        }
    }

    // 스크립트 다음으로 넘기기 & 마지막 씬일때 선택지 보여주기
    public void ShowNextLine(){
        if (isTyping) return;
        if(!isAfter){
            if(currentIndex < currentDialog.Length){
            face.sprite = sprites[currentIndex];
            typeCoroutine = StartCoroutine(TypeText(currentDialog[currentIndex]));
            currentIndex++;
            }
        }
        else if(isAfter){
            face.sprite = sprites[currentIndex];
            typeCoroutine = StartCoroutine(TypeText(currentDialog[currentIndex]));
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
        if(explorePoint !=null){
            explorePoint.GetComponent<ExplorePoint>().Finish_Explore();
            explorePoint = null;
        }
        dialogUI.SetActive(false);
        selectBtn.SetActive(false);
        btnUI.SetActive(false);
        timer.isStart = false;
        isTalking = false;
        endingPoint = pc.exploreCount + pc.neglectCount;
        if(isAfter){
            isAfter = false;
            ShowEnding();
        }
    }

    // 1,2번 선택지 선택 시 미니게임 출력
    public void StartMiniGame(int num){
        isPlaying = true;
        miniManager.num = num;
        mainCamera.SetActive(false);
        miniManager.SetMiniGame();
    }

    // 미니게임 끝날 시 실행
    public void EndMiniGame(){
        isPlaying=false;
        miniManager.EndMiniGame();
        mainCamera.SetActive(true);
        AfterDialogue(pc.dialog,pc.selectedNum());
    }
    
    // 미니게임 엔딩 또는 3번째 선택지 선택시 스크립트 실행
    public void AfterDialogue(DialogueData dialog, int num){
        isTalking = true;
        isAfter = true;
        dialogUI.SetActive(true);
        currentDialog = dialog.afterLines;
        currentIndex = num;
        sprites = dialog.afterSprites;
        ShowNextLine();
    }
    
    // 엔딩 포인트 달성 시 엔딩 스크립트
    public void ShowEnding(){
        // 마무리 대사
        if(endingPoint >=5){
            isEnding = true;
            startDialog(endingDialog);
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
        if(!isAfter){
            ShowSelectBtn();
        }
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

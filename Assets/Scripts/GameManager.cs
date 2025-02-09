using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public GameObject dialogUI;
    [SerializeField] public TMP_Text dialogText;
    [SerializeField] public GameObject btnUI;
    [SerializeField] public GameObject selectBtn;
    [SerializeField] public TMP_Text[] btnText;
    [SerializeField] public Button dialogBtn;
    [SerializeField] public Timer timer;
    private string[] currentDialog;
    private int currentIndex;
    private bool isTyping = false;
    public bool isTalking = false;

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

    public void ShowNextLine(){

        if(isTyping) return;

        if(currentIndex < currentDialog.Length){
            StartCoroutine(TypeText(currentDialog[currentIndex]));
            currentIndex++;
        }
        else {
            
        }
    }
    public void ShowSelectBtn(){
        btnUI.SetActive(true);
    }
    public void endDialog(){
        dialogUI.SetActive(false);
        selectBtn.SetActive(false);
        btnUI.SetActive(false);
        timer.isStart = false;
        isTalking = false;
    }
    private IEnumerator TypeText(string text){
        isTyping = true;
        dialogText.text = "";

        foreach(char letter in text){
            dialogText.text +=letter;
            yield return new WaitForSeconds(0.04f);
        }
        isTyping = false;
        if(currentIndex == currentDialog.Length){
            ShowSelectBtn();
            timer.isStart = true;
            timer.times = 0f;
        }
    }
}

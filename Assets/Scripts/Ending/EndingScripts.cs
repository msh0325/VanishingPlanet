using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScripts : MonoBehaviour
{
    [SerializeField] GameObject endingData;
    [SerializeField] EndingDialogueData endingScripts;
    [SerializeField] TMP_Text exploreText;
    [SerializeField] TMP_Text resultText;
    [SerializeField] Image img;
    [SerializeField] Button btn;
    [SerializeField] TextAudio typing;
    [SerializeField] GameObject[] ending;
    private GameObject nowEnding;
    
    private bool isResult = false;
    private bool scriptend = false;
    private string soundName = "Result";
    private float soundCooldown = 0.15f;
    private float lastSoundTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        endingData = GameObject.Find("EndingData");
        typing = GameObject.Find("SoundManager").GetComponent<TextAudio>();
        EndingData data = endingData.GetComponent<EndingData>();
        TypingEnding(data,endingScripts);

        btn.onClick.AddListener(()=>{
            if(scriptend){
                StartCoroutine(FadeWindows(img));
            }
        });
    }

    private void TypingEnding(EndingData data, EndingDialogueData dialog){
        string exDialog;
        exDialog = 
        $" - {dialog.buildingLines[data.selectnum[0]]}\n\n" +
        $" - {dialog.carLines[data.selectnum[1]]}\n\n" + 
        $" - {dialog.plantLines[data.selectnum[2]]}\n\n" + 
        $" - {dialog.gunLines[data.selectnum[3]]}\n\n" + 
        $" - {dialog.skullLines[data.selectnum[4]]}";
        string result = dialog.resultLine[data.resultnum];
        nowEnding = ending[data.resultnum];
        StartCoroutine(CoroutineSequence(exDialog,result));
    }

    private IEnumerator CoroutineSequence(string exploreString, string resultString){
        yield return StartCoroutine(TypeText(exploreString));
        isResult = true;
        yield return StartCoroutine(TypeText(resultString));
        nowEnding.SetActive(true);
    }

    private IEnumerator TypeText(string text){
        typing.SetCharacterBeep(soundName);
        if(!isResult){
            exploreText.text = "";
            foreach(char letter in text){
                exploreText.text += letter;
                if(Time.time - lastSoundTime >= soundCooldown){
                    typing.PlayBeep();
                    lastSoundTime = Time.time;
                }
                yield return new WaitForSeconds(0.8f);
            }
        }
        else if(isResult){
            resultText.text = "탐사 결과 : ";
            foreach(char letter in text){
                resultText.text += letter;
                if(Time.time - lastSoundTime >= soundCooldown){
                    typing.PlayBeep();
                    lastSoundTime = Time.time;
                }
                yield return new WaitForSeconds(0.8f);
            }
            scriptend = true;
        }
    }
    private IEnumerator FadeWindows(Image img){
        float fadeCount = 0;
        while (fadeCount < 1.0f){
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            img.color = new Color(0,0,0,fadeCount);
        }
        SceneManager.LoadScene("TitleScene");
    }
}

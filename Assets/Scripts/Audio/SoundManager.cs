using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider BGM;
    [SerializeField] Slider Effect;
    public AudioSource audioSource;
    public AudioSource effectSource;
    public AudioClip[] source;
    public float bgm = 0.8f;
    private float bgmFull = 1f;
    public float effect = 0.8f;
    private float effectFull = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        effectSource = gameObject.GetComponent<TextAudio>().audioSource;

        BGM.value = bgm / bgmFull;
        audioSource.volume = BGM.value;

        Effect.value = effect / effectFull;
        effectSource.volume = Effect.value;

        SceneManager.sceneLoaded += OnSceneLoaded;

        BGM.onValueChanged.AddListener((value)=>{
            SetBGMVolume(BGM.value);
        });

        Effect.onValueChanged.AddListener((value)=>{
            SetEffectVolume(Effect.value);
        });

    }

    public void SetBGMVolume(float volume){
        bgm = volume;
        audioSource.volume = volume / bgmFull;
    }
    public void SetEffectVolume(float volume){
        effect = volume;
        effectSource.volume = volume / effectFull;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        ChangeClip(scene.name);
    }
    
    private void ChangeClip(string name){
        if(name == "TitleScene"){
            audioSource.clip = source[0];
        }
        else if(name == "ExploreScene"){
            audioSource.clip = source[1];
        }
        else if(name == "NormalEnding" || name == "HiddenEnding"){
            audioSource.Stop();
            return;
        }
        audioSource.Play();
    }


}

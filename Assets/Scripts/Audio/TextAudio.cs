using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAudio : MonoBehaviour
{
    public AudioSource audioSource;
    private Dictionary<string, AudioClip> beepsounds;
    private string nowCharacter;
    [SerializeField] public AudioClip speech;
    [SerializeField] public AudioClip typing;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        beepsounds = new Dictionary<string, AudioClip>{
            {"Player",speech},
            {"NPC",speech}
        };
    }

    public void SetCharacterBeep(string name){
        nowCharacter = name;
    }
    public void PlayBeep(){
        switch (nowCharacter){
            case "Player" :
            audioSource.volume = 1.2f;
                audioSource.pitch = 1.0f;
                audioSource.PlayOneShot(speech);
                break;
            case "NPC" :
                audioSource.volume = 1.2f;
                audioSource.pitch = 1.5f;
                audioSource.PlayOneShot(speech);
                break;
            case "Result" :
                audioSource.volume = 0.6f;
                audioSource.pitch = 1.0f;
                audioSource.PlayOneShot(typing);
                break;
        }
        
    }
}

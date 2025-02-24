using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAudio : MonoBehaviour
{
    public AudioSource audioSource;
    private Dictionary<string, AudioClip> beepsounds;
    private string nowCharacter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        beepsounds = new Dictionary<string, AudioClip>{
            {"Player", GenerateBeep(220,0.15f)},
            {"NPC", GenerateBeep(440,0.15f)}
        };
        audioSource.volume = 0.4f;
    }

    public void SetCharacterBeep(string name){
        nowCharacter = name;
    }

    public void PlayBeep(){
        if(nowCharacter != null && beepsounds.ContainsKey(nowCharacter)){
            audioSource.PlayOneShot(beepsounds[nowCharacter]);
        }
    }

    AudioClip GenerateBeep(float frequency, float duration){
        int sampleRate = 44100;
        int sampleLength = (int)(sampleRate * duration);
        float[] samples = new float[sampleLength];

        for (int i=0; i<sampleLength;i++){
            samples[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / (float) sampleRate);
        }

        AudioClip clip = AudioClip.Create("Beep", sampleLength, 1, sampleRate, false);
        clip.SetData(samples,0);
        return clip;
    }
}

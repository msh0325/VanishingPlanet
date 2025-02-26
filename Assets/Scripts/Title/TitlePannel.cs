using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlePannel : MonoBehaviour
{
    [SerializeField] public Button startBtn;
    [SerializeField] public Button optionBtn;
    [SerializeField] public Button exitBtn;
    [SerializeField] private GameObject soundPannel;
    [SerializeField] GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(()=>{
            GameObject data = GameObject.Find("EndingData");
            if(data != null){
                data.GetComponent<EndingData>().ResetData();
            }
            SceneManager.LoadScene("ExploreScene");
        });

        optionBtn.onClick.AddListener(()=>{
            block.SetActive(true);
            soundPannel.SetActive(true);
        });

        exitBtn.onClick.AddListener(()=>{
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlePannel : MonoBehaviour
{
    [SerializeField] public Button startBtn;
    [SerializeField] public Button exitBtn;

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

        exitBtn.onClick.AddListener(()=>{
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

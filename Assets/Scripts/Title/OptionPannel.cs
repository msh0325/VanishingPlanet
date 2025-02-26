using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPannel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button cancleBtn;
    [SerializeField] GameObject block;
    void Start()
    {
        cancleBtn.onClick.AddListener(()=>{
            Time.timeScale = 1;
            block.SetActive(false);
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

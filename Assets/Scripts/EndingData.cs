using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingData : MonoBehaviour
{
    [SerializeField] public int[] selectnum ;
    [SerializeField] public int resultnum;

    // Start is called before the first frame update
    void Start()
    {
        selectnum = new int[5];
    }

    public void ResetData(){
        for(int i=0;i<5;i++){
            selectnum[i] = 0;
        }
        resultnum = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlUI : MonoBehaviour
{
    public GameObject UI;

    
    void Start()
    {
        if (UI != null)
            UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void closeUI()
    {
        UI.SetActive(false);
    }
}

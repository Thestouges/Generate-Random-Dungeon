using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseClickEvent : MonoBehaviour
{
    bool clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && clicked == false)
        {
            SceneManager.LoadScene("SampleScene");
            clicked = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
        }
    }
}

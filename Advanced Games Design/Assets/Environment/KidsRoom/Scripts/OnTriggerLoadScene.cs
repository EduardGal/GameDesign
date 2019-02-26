﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadScene : MonoBehaviour
{

    public GameObject enterText;
    public string levelToLoad;

    void Start()
    {
        enterText.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider plyr)
    {
        if (plyr.gameObject.tag == "PlayerOne")
        {
            enterText.SetActive(true);
            if (Input.GetButtonDown("Use"))
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "PlayerOne")
        {
            enterText.SetActive(false);
        }
    }
}
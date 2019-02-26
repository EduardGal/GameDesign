using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingScreen : Photon.MonoBehaviour
    
{
    AsyncOperation op;
    public bool isLoaded;
    public Image loading;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.LoadSceneAsync(1).progress < 0)
        {
            op = SceneManager.LoadSceneAsync(1);
            loading.transform.localScale = new Vector3(0.1f, -.5f, 0.3f);
       
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //isLoaded = PhotonNetwork.LoadLevelAsync(1).isDone;
        float progress = PhotonNetwork.LoadLevelAsync(1).progress;
        loading.transform.localScale = new Vector3(progress / 50, -.5f, 0.3f);
        op.allowSceneActivation = false;
        if (isLoaded == true)
        {
            op.allowSceneActivation = true;
           
        }
    }




}

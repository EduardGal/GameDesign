using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class photonHandler : MonoBehaviour {

    public photonButtons photonB;

    public GameObject mainPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(this.transform);

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 60;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }


    public void moveScene()
    {
        PhotonNetwork.LoadLevel("Framandi v1");
    }

    public void CreateNewRoom()
    {
        PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }


    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Framandi v1")
        {
            spawnPlayer();
        }

    }

    private void spawnPlayer()
    {
        int randomY;
        int randomX;

        randomX = Random.Range(40, 50);
        randomY = Random.Range(240, 250);
        PhotonNetwork.Instantiate(mainPlayer.name, new Vector3(randomX, 0.8f, randomY), mainPlayer.transform.rotation, 0);
    }
}

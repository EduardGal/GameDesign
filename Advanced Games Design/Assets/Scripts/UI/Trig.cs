 using UnityEngine;
using UnityEngine.SceneManagement;
 
 public class Trig : MonoBehaviour
{
     public GameObject uiObject;
	 
     // Use this for initialization
     void Start () { 
		 
     uiObject.SetActive(false);
     }
     
     void OnTriggerEnter (Collider player)
     {
		 if(player.gameObject.tag == "PlayerOne")
		 {
			 
	uiObject.SetActive(true);
			 AudioListener.volume = 1.0f;
			 FindObjectOfType<AudioManager>().Play("Objective");
     }
 
    else
	{
				 AudioListener.volume = 1.0f;
     }
}
}
	 
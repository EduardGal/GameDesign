 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 
 public class PauseMenu : MonoBehaviour {
	 public static bool GameIsPaused = false;
	 
	 public GameObject pauseMenuUI;
	 
	 void Update()
	 {
		 if(Input.GetKeyDown(KeyCode.Escape))
		 {
			 if(GameIsPaused)
			 {
				 Resume();
			 }
			 else
			 {
				 Pause();
				 
				 
				 
			 }
			 
		 }
	 }
	 	public void Resume()
	 {
				 pauseMenuUI.SetActive(false);
		 Time.timeScale = 1f;
		 GameIsPaused = false;
		 AudioListener.volume = 1f;
 
	 }
	 
	 void Pause()
	 {
		 AudioListener.volume = 0.25f;
		 		 pauseMenuUI.SetActive(true);
		 Time.timeScale = 0f;
		 GameIsPaused = true;
         Cursor.lockState = CursorLockMode.None;
		 
	 }
	 
	 public void LoadMenu()
	 {
		 SceneManager.LoadScene("MainMenu");
		 
	 }
	 public void QuitGame()
	 {
		 Application.Quit();
		 
	 }
	 
 }
 

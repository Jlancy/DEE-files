using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	// Use this for initialization
	public Text text;
	bool test;
	void Start () {
	
	}
	 
	public void Save()
	{
		
	}
	public void Quit()
	{
		Application.Quit();
	}
	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	public void Setting()
	{
		
	}

	// mod to load save file
	public void LoadGame()
	{
		StartCoroutine(LoadLevel());	
	}
	public void NewGame()
	{
		
	}

	IEnumerator LoadLevel()
	{
		
		AsyncOperation async = SceneManager.LoadSceneAsync("TestScene");
		while(!async.isDone)
		{
			text.text = async.progress.ToString();

			yield return null;
		}

		 

	}
}

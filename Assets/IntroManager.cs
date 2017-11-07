using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{

	public Button btnNewGame;
	public Button btnContinue;

	// Use this for initialization
	void Start ()
	{
		btnNewGame.onClick.AddListener (() => {
			Constants.fromBeginning = true;
			SceneManager.LoadScene ("[PlayScene]");
		});
		btnContinue.onClick.AddListener (() => {
			Constants.fromBeginning = false;
			SceneManager.LoadScene ("[PlayScene]");
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

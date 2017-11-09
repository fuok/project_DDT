using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	public static IntroManager Instance{ get; private set; }

	public Button btnNewGame;
	public Button btnContinue;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		btnNewGame.onClick.AddListener (() => {
			Constants.FromBeginning = true;
			SceneManager.LoadScene ("[LoadingScene]");
		});
		btnContinue.onClick.AddListener (() => {
			Constants.FromBeginning = false;
			SceneManager.LoadScene ("[LoadingScene]");
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

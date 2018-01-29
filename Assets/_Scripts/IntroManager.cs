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
	public Button btnCustomGirl;
	public InputField inputFieldPlayerNumber;

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
			Constants.PlayerNumber = int.Parse (inputFieldPlayerNumber.text);
			SceneManager.LoadScene ("[LoadingScene2]");
		});
		btnContinue.onClick.AddListener (() => {
			Constants.FromBeginning = false;
			SceneManager.LoadScene ("[LoadingScene2]");
		});
		btnCustomGirl.onClick.AddListener (() => {
			UIManager.Instance.Open (typeof(PanelCustomGirl));
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

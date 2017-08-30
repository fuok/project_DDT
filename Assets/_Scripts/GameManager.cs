using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public Text txtDiceResult;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
//		DontDestroyOnLoad (gameObject);
		DiceManager.instance.diceEvent += ShowDiceResult;
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	void ShowDiceResult (string para)
	{
		txtDiceResult.text = para;
	}
}

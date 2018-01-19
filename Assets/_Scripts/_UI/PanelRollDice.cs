using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRollDice : UIBase
{
	public static PanelRollDice Instance{ private set; get; }

	public Button btnRoll;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	void Start ()
	{
		btnRoll.onClick.AddListener (delegate() {
			GameManager.Instance.RollDice ();
			btnRoll.onClick.RemoveAllListeners ();//避免重复点

			Invoke ("Close", 0.5f);
		});
	}

	//	void Update ()
	//	{
	//
	//	}

	void Close ()
	{
		GameObject.DestroyImmediate (gameObject);
	}
}

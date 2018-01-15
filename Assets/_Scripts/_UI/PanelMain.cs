using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMain : UIBase
{
	public static PanelMain Instance{ private set; get; }

	public Button btnPackage;
	public Button btnRoll;
	//	public Text txtDiceResult;
	//	public Text txtCurrentPlayer;

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
//		txtCurrentPlayer.text = GameManager.Instance.GetCurrentPlayer ().Name + ",回合开始！";
		PanelUiContainer.Instance.setTopMessage (GameManager.Instance.GetCurrentPlayer ().Name + ",回合开始！");

		btnPackage.onClick.AddListener (() => {
			UIManager.Instance.Open (typeof(PanelDrugPackage));
		});
		btnRoll.onClick.AddListener (delegate() {
			GameManager.Instance.RollDice ();
			btnRoll.onClick.RemoveAllListeners ();//避免重复点
		});

	}

	//	void Update ()
	//	{
	//
	//	}
}

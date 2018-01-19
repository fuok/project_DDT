using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMain : UIBase
{
	public static PanelMain Instance{ private set; get; }

	public Text txtPlayerName;
	public Text txtPlayerHealth;
	public Text txtPlayerMoney;

	public Button btnPackage;
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
		PanelUiContainer.Instance.setTopMessage (GameManager.Instance.GetCurrentPlayer ().Name + ",回合开始！");

		btnPackage.onClick.AddListener (() => {
			UIManager.Instance.Close (this.GetType ());
			UIManager.Instance.Open (typeof(PanelDrugPackage));
		});
		btnRoll.onClick.AddListener (delegate() {
			GameManager.Instance.RollDice ();
			btnRoll.onClick.RemoveAllListeners ();//避免重复点
		});

		Refresh ();
	}

	//	void Update ()
	//	{
	//
	//	}

	public void Refresh ()
	{
		txtPlayerName.text = GameManager.Instance.GetCurrentPlayer ().Name;
		txtPlayerHealth.text = GameManager.Instance.GetCurrentPlayer ().Health.ToString ();
		txtPlayerMoney.text = GameManager.Instance.GetCurrentPlayer ().Money.ToString ();
	}
}

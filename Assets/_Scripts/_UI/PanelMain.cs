using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMain : UIBase
{
	public static PanelMain Instance{ private set; get; }

	public Button btnRoll;
	public Text txtDiceResult;
	public Text txtCurrentPlayer;
	public Text[] txtPlayerInfo;

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
		txtCurrentPlayer.text = GameManager.Instance.GetCurrentPlayer ().Name + ",回合开始！";
		txtDiceResult.text = "";
		
		btnRoll.onClick.AddListener (GameManager.Instance.RollDice);
	}

	void Update ()
	{


		//		txtDiceResult.text = currentPlayer.Name + "掷出" + num + ",前进到" + target;

		//玩家信息展示
		for (int i = 0; i < 4; i++) {
//			txtPlayerInfo [i].text = mPlayerList [i].Name + "\n" + "金钱:" + mPlayerList [i].Money;
		}
	}

	public void Refrsh ()
	{
		//TODO
	}
}

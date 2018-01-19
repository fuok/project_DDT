using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStartTurn : UIBase
{
	public static PanelStartTurn Instance{ private set; get; }

	public Text txtRoundNumber;
	public Text txtPlayerName;

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
		txtRoundNumber.text = string.Format ("第{0}回合", PlayerPrefs.GetInt (Constants.GAME_ROUND_NUMBER));
		txtPlayerName.text = string.Format ("{0} 行动开始", GameManager.Instance.GetCurrentPlayer ().Name);

		StartCoroutine (Wait4Anim ());
	}

	//	void Update ()
	//	{
	//
	//	}

	public override void SetParams (params object[] args)
	{
		base.SetParams (args);

	}

	private IEnumerator Wait4Anim ()
	{
		yield return new WaitForSeconds (1f);
		GameManager.Instance.SetAction (Constants.ACTION_START_TURN_OUT);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMain : UIBase
{
	public static PanelMain Instance{ private set; get; }

	public Button btnPackage;
	public Button btnRoll;
	public Text txtDiceResult;
	public Text txtCurrentPlayer;
	//	public Text[] txtPlayerInfo;

	public Transform[] mPlayerInfoField;

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

		btnPackage.onClick.AddListener (() => {
			UIManager.Instance.Open (typeof(PanelDrugPackage));
		});
		btnRoll.onClick.AddListener (delegate() {
			GameManager.Instance.RollDice ();
			btnRoll.onClick.RemoveAllListeners ();//避免重复点
		});

		StartCoroutine (RefreshCoroutine ());
	}

	void Update ()
	{

//		txtDiceResult.text = currentPlayer.Name + "掷出" + num + ",前进到" + target;

//		if (Input.GetKeyDown (KeyCode.P)) {
//			Refrsh ();
//		}
	}

	private IEnumerator RefreshCoroutine ()
	{
		Refrsh ();
		yield return new WaitForSeconds (0.2f);
		StartCoroutine (RefreshCoroutine ());
	}

	public void Refrsh ()
	{
		//玩家信息展示
		for (int i = 0; i < mPlayerInfoField.Length; i++) {
			mPlayerInfoField [i].Find ("Text Name").GetComponent<Text> ().text = GameManager.Instance.GetAllPlayer () [i].Name;
			mPlayerInfoField [i].Find ("Text Money").GetComponent<Text> ().text = GameManager.Instance.GetAllPlayer () [i].Money.ToString ();
			mPlayerInfoField [i].Find ("Text Girl").GetComponent<Text> ().text = GameManager.Instance.GetPlayerGirl (i).Count.ToString ();
			mPlayerInfoField [i].Find ("Text Health").GetComponent<Text> ().text = GameManager.Instance.GetAllPlayer () [i].Health.ToString ();
		}
	}
}

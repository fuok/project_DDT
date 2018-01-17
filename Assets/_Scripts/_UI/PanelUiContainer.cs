using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUiContainer : UnitySigleton<PanelUiContainer>
{
//	public static PanelUiContainer Instance{ get; private set; }

	public Transform[] mPlayerInfoField;

	public Text txtTopMessage;

//	void Awake ()
//	{
//		if (Instance == null) {
//			Instance = this;
//		} else if (Instance != this) {
//			Destroy (gameObject);
//		}
//	}

	//	void Start ()
	//	{
	//
	//	}

	//	void Update ()
	//	{
	//
	//	}

	public void RefrshUI ()
	{
		//玩家信息展示
		for (int i = 0; i < mPlayerInfoField.Length; i++) {
			if (i < GameManager.Instance.GetAllPlayer ().Count) {
				mPlayerInfoField [i].gameObject.SetActive (true);
				//角色属性
				mPlayerInfoField [i].Find ("name bg/Text Name").GetComponent<Text> ().text = GameManager.Instance.GetAllPlayer () [i].Name;
				mPlayerInfoField [i].Find ("Text Money").GetComponent<Text> ().text = GameManager.Instance.GetAllPlayer () [i].Money.ToString ();
				mPlayerInfoField [i].Find ("Text Girl").GetComponent<Text> ().text = GameManager.Instance.GetPlayerGirl (i).Count.ToString ();
				mPlayerInfoField [i].Find ("Text Health").GetComponent<Text> ().text = GameManager.Instance.GetAllPlayer () [i].Health.ToString ();
			} else {
				mPlayerInfoField [i].gameObject.SetActive (false);
			}
		}
	}

	public void setTopMessage (string str)
	{
		txtTopMessage.text = str;
	}
}

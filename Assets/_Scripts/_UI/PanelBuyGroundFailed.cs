using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyGroundFailed : UIBase
{
	public static PanelBuyGroundFailed Instance{ private set; get; }

	public Button btnBuyGroundNoMoney;

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
		btnBuyGroundNoMoney.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_BUY_GROUND_FAILED_CONFIRM);
		});
	}

	//TODO,需要传参数，区分没钱的情况和美女人的情况，区别只是在于文字描述不同。

}

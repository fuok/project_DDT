using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyGround : UIBase
{
	public static PanelBuyGround Instance{ private set; get; }

	public Button btnBuyGroundYes;
	public Button btnBuyGroundNo;

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

		btnBuyGroundYes.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_BUY_GROUND_YES);
		});
		btnBuyGroundNo.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_BUY_GROUND_NO);
		});
	}
}

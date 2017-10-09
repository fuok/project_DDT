using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyDrug : UIBase
{
	public static PanelBuyDrug Instance{ private set; get; }

	public Button btnBuyDrugConfirm;

	private Drug[] dList = new Drug[2];

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		//测试数据
		Drug d1 = new Drug (1, 100, "营养快线", "", "");
		Drug d2 = new Drug (2, 200, "汇仁肾宝", "", "");
		dList.SetValue (d1, 0);
		dList.SetValue (d2, 1);

		for (int i = 0; i < dList.Length; i++) {
			print (dList [i].name);
		}
	}

	void Start ()
	{
		btnBuyDrugConfirm.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_BUY_DRUG_CONFIRM);
		});

//		print (GameManager.Instance.GetCurrentPlayer ().GetItemList ().ToString ());
	}

	void Update ()
	{
		
	}
}

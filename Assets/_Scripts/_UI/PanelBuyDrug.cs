using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyDrug : UIBase
{
	public static PanelBuyDrug Instance{ private set; get; }

	public Button[] btnItemList;
	public Text[] txtItemName;
	public Text[] txtItemPrice;

	public Button btnBuyDrugConfirm;

	private Drug[] drugList = new Drug[2];

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		//测试数据
		Drug d1 = new Drug (1, 150, "营养快线", "", "");
		Drug d2 = new Drug (2, 350, "汇仁肾宝", "", "");
		drugList.SetValue (d1, 0);
		drugList.SetValue (d2, 1);

	
	}

	void Start ()
	{
		for (int i = 0; i < drugList.Length; i++) {
			txtItemName [i].text = drugList [i].name;
			txtItemPrice [i].text = drugList [i].price.ToString ();
			switch (i) {//因为lambda内部不能使用变量i，所以使用这种方法对应每种商品
			case 0:
				btnItemList [i].onClick.AddListener (() => {
					print ("0");
				});
				break;

			case 1:
				btnItemList [i].onClick.AddListener (() => {
					print ("1");
				});
				break;

			default:
				break;
			}

		}
			
		btnBuyDrugConfirm.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_BUY_DRUG_CONFIRM);
		});

	}

	void Update ()
	{
		
	}
}

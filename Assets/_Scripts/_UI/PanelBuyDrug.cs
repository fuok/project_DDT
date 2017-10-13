using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyDrug : UIBase
{
	public static PanelBuyDrug Instance{ private set; get; }

	public Transform[] mDrugField;

	public Transform[] mPackageField;

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
		Refrsh ();

		for (int i = 0; i < drugList.Length; i++) {
//			txtItemName [i].text = drugList [i].name;
			mDrugField [i].Find ("Button Item/Text").GetComponent<Text> ().text = drugList [i].name;
//			txtItemPrice [i].text = drugList [i].price.ToString ();
			mDrugField [i].Find ("Text Price").GetComponent<Text> ().text = drugList [i].price.ToString ();
			switch (i) {//因为lambda内部不能使用变量i，所以使用这种方法对应每种商品
			case 0:
				mDrugField [i].Find ("Button Item").GetComponent<Button> ().onClick.AddListener (() => {
					if (GameManager.Instance.GetCurrentPlayer ().IsSpaceAvailable ()) {
						GameManager.Instance.GetCurrentPlayer ().AddItem (1);
					}
						
					Refrsh ();
				});
				break;

			case 1:
				mDrugField [i].Find ("Button Item").GetComponent<Button> ().onClick.AddListener (() => {
					if (GameManager.Instance.GetCurrentPlayer ().IsSpaceAvailable ()) {
						GameManager.Instance.GetCurrentPlayer ().AddItem (2);
					}

					Refrsh ();
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

	public void Refrsh ()
	{
		for (int i = 0; i < mPackageField.Length; i++) {
			mPackageField [i].Find ("Text Item").GetComponent<Text> ().text = Drug.GetName (GameManager.Instance.GetCurrentPlayer ().GetItemList () [i]);
		}
	}
}

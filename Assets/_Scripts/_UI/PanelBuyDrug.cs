using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyDrug : UIBase
{
	//	public static PanelBuyDrug Instance{ private set; get; }

	public Transform[] mDrugField;

	public Transform[] mPackageField;

	public Button btnBuyDrugConfirm;

	private Drug[] mGoodsList = new Drug[2];

	void Awake ()
	{
		base.Awake ();
//		if (Instance == null) {
//			Instance = this;
//		} else if (Instance != this) {
//			Destroy (gameObject);
//		}

		//测试数据
		mGoodsList.SetValue (Drug.GetDrug (1), 0);
		mGoodsList.SetValue (Drug.GetDrug (2), 1);

	
	}

	void Start ()
	{
		Refrsh ();

		for (int i = 0; i < mGoodsList.Length; i++) {
//			txtItemName [i].text = drugList [i].name;
			mDrugField [i].Find ("Button Item/Text").GetComponent<Text> ().text = mGoodsList [i].name;
//			txtItemPrice [i].text = drugList [i].price.ToString ();
			mDrugField [i].Find ("Text Price").GetComponent<Text> ().text = mGoodsList [i].price.ToString ();
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
			mPackageField [i].Find ("Text Item").GetComponent<Text> ().text = Drug.GetDrug (GameManager.Instance.GetCurrentPlayer ().GetItemList () [i]).name;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDrugPackage : UIBase
{
	public static PanelDrugPackage Instance{ private set; get; }

	public Button btnBack;
	public Transform[] mPackageField;

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
		Refrsh ();

		btnBack.onClick.AddListener (() => {
			UIManager.Instance.Close (this.GetType ());
		});

		for (int i = 0; i < mPackageField.Length; i++) {
			switch (i) {
			case 0:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.GetCurrentPlayer ().UseItem (0);
					Refrsh ();
				});
				break;
			case 1:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.GetCurrentPlayer ().UseItem (1);
					Refrsh ();
				});
				break;
			case 2:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.GetCurrentPlayer ().UseItem (2);
					Refrsh ();
				});
				break;
			case 3:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.GetCurrentPlayer ().UseItem (3);
					Refrsh ();
				});
				break;
			case 4:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.GetCurrentPlayer ().UseItem (4);
					Refrsh ();
				});
				break;
			case 5:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.GetCurrentPlayer ().UseItem (5);
					Refrsh ();
				});
				break;
			default:
				break;
			}
		}
	}

	void Update ()
	{
		
	}

	public void Refrsh ()
	{
		int[] dList = GameManager.Instance.GetCurrentPlayer ().GetItemList ();

		for (int i = 0; i < dList.Length; i++) {
			mPackageField [i].Find ("Text").GetComponent<Text> ().text = Drug.GetDrug (dList [i]).name;
		}
	}
}

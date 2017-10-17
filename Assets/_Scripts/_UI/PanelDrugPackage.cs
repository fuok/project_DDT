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
					//	GameManager.Instance.GetCurrentPlayer ().AddItem (1);
					print ("1");
					Refrsh ();
				});
				break;
			case 1:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					//	GameManager.Instance.GetCurrentPlayer ().AddItem (1);
					print ("2");
					Refrsh ();
				});
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			case 5:
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
			mPackageField [i].Find ("Text").GetComponent<Text> ().text = Drug.GetName (dList [i]);
		}
	}
}

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
		btnBack.onClick.AddListener (() => {
			UIManager.Instance.Close (this.GetType ());
		});

		for (int i = 0; i < mPackageField.Length; i++) {
			switch (i) {
			case 0:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.SetAction (Constants.ACTIVATE_PACKAGE_0);
				});
				break;
			case 1:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.SetAction (Constants.ACTIVATE_PACKAGE_1);
				});
				break;
			case 2:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.SetAction (Constants.ACTIVATE_PACKAGE_2);
				});
				break;
			case 3:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.SetAction (Constants.ACTIVATE_PACKAGE_3);
				});
				break;
			case 4:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.SetAction (Constants.ACTIVATE_PACKAGE_4);
				});
				break;
			case 5:
				mPackageField [i].Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
					GameManager.Instance.SetAction (Constants.ACTIVATE_PACKAGE_5);
				});
				break;
			default:
				break;
			}
		}

		StartCoroutine (RefreshCoroutine ());
	}

	void Update ()
	{
		
	}

	private IEnumerator RefreshCoroutine ()
	{
		Refrsh ();
		yield return new WaitForSeconds (0.2f);
		StartCoroutine (RefreshCoroutine ());
	}

	public void Refrsh ()
	{
		int[] dList = GameManager.Instance.GetCurrentPlayer ().GetItemList ();

		for (int i = 0; i < dList.Length; i++) {
			mPackageField [i].Find ("Text").GetComponent<Text> ().text = Drug.GetDrug (dList [i]).name;
		}
	}
}

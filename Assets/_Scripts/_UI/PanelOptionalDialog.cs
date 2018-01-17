using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionalDialog : UIBase
{
//	public static PanelOptionalDialog Instance{ private set; get; }

	public Text txtTitle;
	public Text txtContent;
	public Button btnCancel;
	public Button btnConfirm;

//	void Awake ()
//	{
//		if (Instance == null) {
//			Instance = this;
//		} else if (Instance != this) {
//			Destroy (gameObject);
//		}
//
//	}

	void Start ()
	{
		btnCancel.onClick.AddListener (delegate() {
			UIManager.Instance.Close (this.GetType ());
		});
	}

	void Update ()
	{
		
	}

	public override void SetParams (UIBase.ConfirmDelegate func, params object[] args)
	{
		base.SetParams (func, args);

		if (args.Length == 2) {
			txtTitle.text = args [0].ToString ();
			txtContent.text = args [1].ToString ();
		}

		if (func != null) {
			btnConfirm.onClick.AddListener (delegate() {
				func ();
				UIManager.Instance.Close (this.GetType ());
			});
		}
	}

}

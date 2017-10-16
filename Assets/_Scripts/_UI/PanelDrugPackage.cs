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
	}

	void Update ()
	{
		
	}

	public void Refrsh ()
	{
		
	}
}

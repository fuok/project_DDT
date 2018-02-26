using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSetting : UIBase
{
	public static PanelSetting Instance{ private set; get; }

	public Button btnBack;
	public Button btnLangCN, btnLangEN;

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
			IntroManager.Instance.panelIntroMain.SetActive (true);
		});

		btnLangCN.onClick.AddListener (() => {
			LocalizationManager.Instance.SetLang ("CN");
			Refresh ();
		});
		btnLangEN.onClick.AddListener (() => {
			LocalizationManager.Instance.SetLang ("EN");
			Refresh ();
		});
	}

	//	void Update ()
	//	{
	//
	//	}

	private void Refresh ()
	{
		gameObject.SetActive (false);
		gameObject.SetActive (true);
	}
}

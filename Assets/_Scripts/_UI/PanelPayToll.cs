using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPayToll : UIBase
{
	public static PanelPayToll Instance{ private set; get; }

	public Text txtPayToll;
	public Button btnPayToll;

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

		btnPayToll.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_PAY_TOLL_CONFIRM);
		});
	}

	public override void SetParams (params object[] args)
	{
		base.SetParams (args);
		txtPayToll.text = args [0] as string;
	}
}

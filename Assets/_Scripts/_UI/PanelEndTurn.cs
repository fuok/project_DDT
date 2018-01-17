using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEndTurn : UIBase
{
	public Button btnEndTurn;

//	public static PanelEndTurn Instance{ private set; get; }

//	void Awake ()
//	{
//		if (Instance == null) {
//			Instance = this;
//		} else if (Instance != this) {
//			Destroy (gameObject);
//		}
//	}


	void Start ()
	{

		btnEndTurn.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_END_TURN_CONFIRM);
		});
		
	}

	public override void SetParams<T> (ref T arg, params object[] args)
	{
		base.SetParams (ref arg, args);	
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShowMessage : UIBase
{
	public static PanelShowMessage Instance{ private set; get; }

	public GameObject panelGirlDialog;
	public RawImage imgGirlPortrait;
	public Text txtGirlName;
	public Text txtContent;

	List<Girl> girl2Leave = new List<Girl> ();

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
		
	}

	public override void SetParams<T> (ref T arg, params object[] args)
	{
		base.SetParams (ref arg, args);

		girl2Leave = arg as List<Girl>;
		for (int i = 0; i < girl2Leave.Count; i++) {
			print (girl2Leave [i].Name + "_" + girl2Leave [i].Patient + "_" + girl2Leave [i].Pressure);
		}
//		GameManager.Instance.SetAction (Constants.ACTION_SHOW_MESSAGE_OUT);
		ShowMessage ();
	}

	private void ShowMessage ()//循环出现
	{
		if (girl2Leave.Count > 0) {
			panelGirlDialog.SetActive (true);
			//属性中自定义的部分
			if (girl2Leave [0].UseCustom == 0) {
				txtGirlName.text = girl2Leave [0].Name;
				imgGirlPortrait.texture = Resources.Load<Texture> (string.Format ("Girl Image Default/girl_{0}_portrait_default", girl2Leave [0].Index.ToString ()));
			} else {
			}
			//不需要自定义的部分
		}
	}

	private void SetBuilding ()//循环出现
	{
		
	}
}

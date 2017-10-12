using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStartTurn : UIBase
{
	public static PanelStartTurn Instance{ private set; get; }

//	public Text txt

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public override void SetParams (params object[] args)
	{
		base.SetParams (args);

	}
}

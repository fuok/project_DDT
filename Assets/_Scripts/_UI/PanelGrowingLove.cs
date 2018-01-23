using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGrowingLove : UIBase
{
	public static PanelGrowingLove Instance{ private set; get; }

	private List<Girl> mGirlListLove = new List<Girl> ();
	private List<Girl> mGirlListPressure = new List<Girl> ();
	//区别选择了爱情还是压力
	private bool selectLove;

	public GameObject panelEnterGrowing;
	public Image[] imgGirlsLove;
	public Image[] imgGirlsPressure;
	public Button btnLove, btnPressure;



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
		btnLove.onClick.AddListener (() => {
			selectLove = true;
		});
		btnPressure.onClick.AddListener (() => {
			selectLove = false;
		});
	}

	//	void Update ()
	//	{
	//
	//	}
}

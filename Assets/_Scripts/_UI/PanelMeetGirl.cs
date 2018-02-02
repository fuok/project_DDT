using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMeetGirl : UIBase
{
	public static PanelMeetGirl Instance{ private set; get; }

	public RawImage imgGirlPortrait;
	public Text txtGirlName;
	public Text txtGirlSalary;
	public Button btnMeetGirlYes;
	public Button btnMeetGirlNo;

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
		//Unboxing,回合制决定了主缓存中的Player和Ground是可以随意获取的，
		//但Girl并不具备固定位置，虽然也可以通过GetLeisureGirl函数获取，但这里还是选择直接传引用参数
		Girl leisureGirl = arg as Girl;
		//属性中自定义的部分使用CustomHelper获取
		txtGirlName.text = CustomHelper.Instance.GetGirlName (leisureGirl);
		CustomHelper.Instance.SetGirlPortrait (imgGirlPortrait, leisureGirl);
		//不需要自定义的部分直接获取
		txtGirlSalary.text = "薪水:" + leisureGirl.Salary;

		btnMeetGirlYes.onClick.AddListener (() => {
			leisureGirl.SetOwner (GameManager.Instance.GetCurrentPlayer ().Index);
			GameManager.Instance.SetAction (Constants.ACTION_MEET_GIRL_YES);
		});
		btnMeetGirlNo.onClick.AddListener (() => {
			GameManager.Instance.SetAction (Constants.ACTION_MEET_GIRL_NO);
		});
	}
}

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
	//	public Text txtContent;
	public ShowConversationType1 showConversation;

	List<Girl> girl2Leave = new List<Girl> ();
	string[] contentStr;

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
		//对话框数组翻页,TODO
		contentStr = new string[]{ };

		panelGirlDialog.SetActive (true);
//		ShowMessage ();
		Invoke ("ShowMessage", 0.1f);
	}

	public override void SetParams<T> (ref T arg, params object[] args)
	{
		base.SetParams (ref arg, args);
		girl2Leave = arg as List<Girl>;
		for (int i = 0; i < girl2Leave.Count; i++) {
			print (girl2Leave [i].Name + "_" + girl2Leave [i].Patient + "_" + girl2Leave [i].Pressure);
		}

//		ShowMessage ();
	}

	private void ShowMessage ()//循环出现
	{
		if (girl2Leave.Count > 0) {
			//属性中自定义的部分使用CustomHelper获取
			txtGirlName.text = CustomHelper.Instance.GetGirlName (girl2Leave [0]);
			CustomHelper.Instance.SetGirlPortrait (imgGirlPortrait, girl2Leave [0]);
			//不需要自定义的部分直接获取
			//...

			showConversation.ShowWriteTypeStr ("受不了了，我要和你分手！！！", OnStrContentDisplayed);

//			txtContent.text = "受不了了，我要和你分手！！！";
	
		} else {
			GameManager.Instance.SetAction (Constants.ACTION_SHOW_MESSAGE_OUT);
		}
	}

	//文字滚动结束
	void OnStrContentDisplayed ()
	{
		//弹框提示
		UIManager.Instance.Open (typeof(PanelSimpleDialog), () => {
			//分手的具体逻辑
			girl2Leave [0].SetFree ();
			girl2Leave.Remove (girl2Leave [0]);
			ShowMessage ();
		}, "", girl2Leave [0].Name + "和你分手了");
	}

	private void SetWorkingMenber ()//循环出现
	{
		
	}
}

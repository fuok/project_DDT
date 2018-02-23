using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelShowMessage : UIBase
{
	public static PanelShowMessage Instance{ private set; get; }

	//要现实的文字内容
	public string[] contents;
	//显示控件
	public GameObject panelGirlDialog;
	public RawImage imgGirlPortrait;
	public Text txtGirlName;
	public Text txtContent;
	private Tweener tweenerText;
	//跳过文字显示过程
	private bool isTextShowing;
	//弹出的按钮等等，感觉也不需要
	public GameObject go2Show;

	List<Girl> girl2Leave = new List<Girl> ();

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	public override void SetParams<T> (ref T arg, params object[] args)
	{
		base.SetParams (ref arg, args);
		girl2Leave = arg as List<Girl>;
		for (int i = 0; i < girl2Leave.Count; i++) {
			print (girl2Leave [i].Name + "_" + girl2Leave [i].Patient + "_" + girl2Leave [i].Pressure);
		}
	}

	void Start ()
	{
		InitTweener ();
		//对话框数组翻页,TODO
		contents = new string[]{ };

		panelGirlDialog.SetActive (true);
		ShowMessage ();//Start()是在SetParams的后面，所以显示要后进行。
	}

	/// <summary>
	/// 初始化文字tweener
	/// </summary>
	private void InitTweener ()
	{
		tweenerText = txtContent.DOText ("", 0f, true, ScrambleMode.None, null);
		tweenerText.SetAutoKill (false);
		tweenerText.SetLoops (1);
		tweenerText.SetEase (Ease.Linear);
		tweenerText.OnComplete (() => {
			//				tweener.Kill (true);
			print ("text done");
			isTextShowing = false;

//			TextComplete ();
			OnStrContentDisplayed ();
		});
		tweenerText.Pause ();//这里必须先暂停，否则后面restart重设duration也没用,而是会继续使用这里的0f
	}

	private void ShowMessage ()//循环出现
	{
		if (girl2Leave.Count > 0) {
			//属性中自定义的部分使用CustomHelper获取
			txtGirlName.text = CustomHelper.Instance.GetGirlName (girl2Leave [0]);
			CustomHelper.Instance.SetGirlPortrait (imgGirlPortrait, girl2Leave [0]);
			//不需要自定义的部分直接获取
			//...

//			showConversation.ShowWriteTypeStr ("受不了了，我要和你分手！！！", OnStrContentDisplayed);

			string str = "受不了了，我要和你分手！！！";
			print (str);
			//		txtContent.text = str;
			tweenerText.ChangeValues ("", str, str.Length / 20f);//直接使用ChangeValues//使用富文本后出字速度明显慢了//注意最后的参数是float时间才是正常的
			tweenerText.Restart ();
			isTextShowing = true;
	
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

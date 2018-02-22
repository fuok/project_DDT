using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//用于对话框writeType形式展示文字，以及文字展示完成后调用后续方法
public class ShowConversation : MonoBehaviour
{
	//要现实的文字内容
	public string[] contents;
	//显示控件
	public Text txtContent;
	private Tweener tweenerText;
	//跳过文字显示过程
	private bool isTextShowing;
	//弹出的按钮等等，感觉也不需要
	public GameObject go2Show;
	//回调事件
	public delegate void ShowTextHandler ();

	event ShowTextHandler TextComplete;

	void Start ()
	{
		
	}

	//	void Update ()
	//	{
	//
	//	}
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
		});
		tweenerText.Pause ();//这里必须先暂停，否则后面restart重设duration也没用,而是会继续使用这里的0f
	}
}

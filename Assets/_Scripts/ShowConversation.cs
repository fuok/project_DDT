using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//用于对话框writeType形式展示文字，以及文字展示完成后调用后续方法
public class ShowConversation : MonoBehaviour
{
	//要现实的文字内容
	public string[] contents;
	//显示控件
	public Text txtContent;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//using System.Reflection;

public class UIManager : MonoBehaviour
{

	public static UIManager Instance{ private set; get; }

	public Transform transCanvas;
	//所有panel都要注册在此
	public PanelStartTurn panelStartTurn;
	public PanelMeetGirl panelMeetGirl;
	public PanelBuyGround panelBuyGround;
	public PanelBuyGroundNoMoney panelBuyGroundNoMoney;
	public PanelPayToll panelPayToll;
	public PanelBuyDrug panelBuyDrug;
	public PanelDrugPackage panelDrugPackage;
	public PanelEndTurn panelEndTurn;
	public PanelMain panelMain;
	public PanelOptionalDialog panelOptionalDialog;

	ArrayList uiList = new ArrayList ();

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		//把全部界面添加进来
		uiList.Add (panelStartTurn);
		uiList.Add (panelMain);
		uiList.Add (panelMeetGirl);
		uiList.Add (panelBuyGround);
		uiList.Add (panelBuyGroundNoMoney);
		uiList.Add (panelPayToll);
		uiList.Add (panelBuyDrug);
		uiList.Add (panelDrugPackage);
		uiList.Add (panelEndTurn);
		uiList.Add (panelOptionalDialog);
	}

	void Start ()
	{

	}

	void Update ()
	{
		
	}

	/// <summary>
	/// 打开对应界面,引用参数是修改主缓存数据，只包括Player、Girl、Ground三种
	/// </summary>
	/// <param name="type">对应的UI类型.</param>
	/// <param name="arg">主缓存</param>
	/// <param name="args">其他参数.</param>
	public void Open<T> (Type type, ref T arg, params object[] args)
	{
//		print (panelMeetGirl.GetType ().Name);

		foreach (var item in uiList) {
			if (item.GetType ().Equals (type)) {
//				print ("对比成功");
//				GameObject ui = GameObject.Instantiate<GameObject> (((UIBase)item).gameObject, transCanvas);
				UIBase ui = GameObject.Instantiate<UIBase> ((UIBase)item, transCanvas);
				ui.SetParams (ref arg, args);
				break;
			} else {
//				print ("对比失败");
			}
		}
	}

	public void Open (Type type, UIBase.ConfirmDelegate func, params object[] args)
	{
		foreach (var item in uiList) {
			if (item.GetType ().Equals (type)) {
				UIBase ui = GameObject.Instantiate<UIBase> ((UIBase)item, transCanvas);
				ui.SetParams (func, args);
				break;
			} else {
				//				print ("对比失败");
			}
		}
	}

	public void Open (Type type, params object[] args)
	{
		foreach (var item in uiList) {
			if (item.GetType ().Equals (type)) {
//				print ("找到页面");
				UIBase ui = GameObject.Instantiate<UIBase> ((UIBase)item, transCanvas);
				ui.SetParams (args);
				break;
			} else {
//				print ("找不到页面");
			}
		}
	}

	/// <summary>
	/// 关闭对应界面
	/// </summary>
	/// <param name="type">Type.</param>
	public void Close (Type type)
	{

		foreach (var item in transCanvas.GetComponentsInChildren(type)) {
			GameObject.Destroy (item.gameObject);
		}

	}
}

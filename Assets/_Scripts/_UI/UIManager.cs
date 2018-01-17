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

	//不必使用ArrayList,指定父类型传入子类元素，是可以获取各个子类类型的。
	List<UIBase> uiList = new List<UIBase> ();

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

	//	void Update ()
	//	{
	//
	//	}

	/// <summary>
	/// 打开对应界面,引用参数是修改主缓存数据，只包括Player、Girl、Ground三种
	/// </summary>
	/// <param name="type">对应的UI类型.</param>
	/// <param name="arg">主缓存,这里传ref的必要性值得商榷，因为通过单例也可以获取主缓存</param>
	/// <param name="args">其他参数.</param>
	public void Open<T> (Type type, ref T arg, params object[] args)
	{
		try {
			GetMatchUI (type).SetParams (ref arg, args);
		} catch (Exception ex) {
			print (ex.Message);
		}
	}

	/// <summary>
	/// Dialog专用的方法，区别在于不需要从额外Action返回，直接委托传入方法
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="func">Func.</param>
	/// <param name="args">其他参数.</param>
	public void Open (Type type, UIBase.ConfirmDelegate func, params object[] args)
	{
		try {
			GetMatchUI (type).SetParams (func, args);
		} catch (Exception ex) {
			print (ex.Message);
		}
	}

	/// <summary>
	/// 最基础的方法
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="args">其他参数.</param>
	public void Open (Type type, params object[] args)
	{
		try {
			GetMatchUI (type).SetParams (args);
		} catch (Exception ex) {
			print (ex.Message);
		}
	}

	/// <summary>
	/// 关闭对应界面
	/// </summary>
	/// <param name="type">Type.</param>
	public void Close (Type type)
	{
		foreach (var item in transCanvas.GetComponentsInChildren(type)) {
			GameObject.DestroyImmediate (item.gameObject);
		}
	}

	private UIBase GetMatchUI (Type type)
	{
		UIBase ui = null;
		//这里只能遍历，而不能用Collection Find方法，因为Find必须指定类型，而用UIBase父类不能实例化子类对象
		foreach (var item in uiList) {
			if (item.GetType ().Equals (type)) {
				ui = GameObject.Instantiate<UIBase> ((UIBase)item, transCanvas);
				ui.name = item.name;
				break;
			}
		}
		if (ui) {
			print ("对比成功");
		} else {
			print ("对比失败");
		}
		return ui;
	}
}

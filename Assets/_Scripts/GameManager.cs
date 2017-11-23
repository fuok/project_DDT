using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance{ get; private set; }

	//棋子移动
	[Header ("棋子移动")]
	[SerializeField]
	private CarLogic[] mCarList;
	[SerializeField]
	private MapNode[] mNodeList;
	//当前的逻辑角色
	Player currentPlayer;
	Ground currentGround;
	public GameObject mStepTarget;

	//骰子相机控制
	[Header ("骰子相机控制")]
	public GameObject mDiceCameraContainer;
	public Camera[] mDiceCams;

	//缓存游戏数据,当游戏中断，这部分需要持久化
	private List<Player> mPlayerList = new List<Player> ();
	private List<Ground> mGroundList;
	private List<Girl> mGirlList = new List<Girl> ();

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		//以下读取数据.一存一取是多余，为了好维护先这样

		//读取Player
		mPlayerList = PlayerBean.Instance.GetPlayerListFromDB ();
		for (int i = 0; i < mPlayerList.Count; i++) {
			//注册玩家事件
			mPlayerList [i].PlayerActionEvent += GameManager.Instance.OnPlayerActionChanged;
		}
		//添加玩家到TurnBase模块
		TurnManager.Instance.SetPlayerQueue (ref mPlayerList);

		//读取Girl
		mGirlList = GirlBean.Instance.GetGirlListFromDB ();

		//Ground
		mGroundList = GroundBean.Instance.GetGroundListFromDB ();
		for (int i = 0; i < mGroundList.Count; i++) {
			if (mGroundList [i].Owner == -2) {
				mNodeList [i].mBuilding.SetNeutralBuilding (mGroundList [i].Type);
			}
		}

		//检查一下数据长度，看看对不对
		if (mPlayerList.Count > 1 && mGirlList.Count > 9 && mGroundList.Count == 32) {
			print ("数据无异常");
		} else {
			print ("数据异常");
		}
			
	}

	void Start ()
	{
		//棋子状态监听
		foreach (var item in mCarList) {
			item.carMoveEvent += OnCarStatusChanged;
		}

		//游戏开始
		StartTurn ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SetAction (Constants.ACTION_GAME_OVER);
		}
	}

	//---------------------------------------------------------------------------------------------------------

	void StartTurn ()
	{
		//回合开始获取currentPlayer,定位currentGround
		currentPlayer = TurnManager.Instance.GetCurrentPlayer ();
		currentGround = mGroundList [currentPlayer.Position];

		//回合计数
		if (currentPlayer.Index == 0) {//新的一轮开始
			PlayerPrefs.SetInt (Constants.GAME_ROUND_NUMBER, PlayerPrefs.GetInt (Constants.GAME_ROUND_NUMBER, 0) + 1);
		}

		SetAction (Constants.ACTION_START_TURN_IN);
	}

	void EndTurn ()
	{
		TurnManager.Instance.StopPlayerMoving ();
	}

	public void RollDice ()
	{
		resultChecked = false;
		DiceManager.Instance.diceEvent += OnDiceResult;//这个方法常驻监听，不是太好，但如果不这样数值会不准，因为骰子的状态不稳定,除非确定稳定后再回调
		mDiceCameraContainer.SetActive (true);
		GameObject[] dices = DiceManager.Instance.RollDice ();
		for (int i = 0; i < dices.Length; i++) {
			dices [i].layer = 8 + i;//两个骰子对应的layer是8、9
			mDiceCams [i].GetComponent<SmoothFollow> ().target = dices [i].transform;	
		}
	}

	//输出骰子点数，只要骰子动作在监听，这里就会一直调用，而且输出的数值是会变的，需要进一步检查才能确认
	void OnDiceResult (int rslt)
	{
		print ("掷出" + rslt);
		StartCoroutine (CheckResult (rslt));
	}

	private int tempRslt;
	private bool resultChecked;

	IEnumerator CheckResult (int rslt)
	{
		tempRslt = rslt;
		yield return new WaitForSeconds (1f);//延迟0.5秒检查稳定后的数值

		if (rslt == tempRslt) {//持续1秒不变，判断数值稳定
			DiceManager.Instance.diceEvent -= OnDiceResult;
			if (resultChecked == false) {
				resultChecked = true;

				//计算移动后所处的Node序列
				int target = (currentGround.Index + rslt) % mNodeList.Length;
				print (currentPlayer.Name + "掷出" + rslt + ",前进到" + target);
				//隐藏骰子
				mDiceCameraContainer.SetActive (false);
				//显示目标箭头
				if (mStepTarget) {
					Vector3 vScreen = Camera.main.WorldToScreenPoint (mNodeList [target].transform.position);
					float canvasX = vScreen.x - Camera.main.pixelWidth / 2;
					float canvasY = vScreen.y - Camera.main.pixelHeight / 2;
					float deltaY = Camera.main.pixelHeight / 20;//显示高度抬高一点
					mStepTarget.transform.localPosition = new Vector3 (canvasX, canvasY + deltaY, 0f);
					mStepTarget.SetActive (true);
				}
				//移动
				CarStep (target, rslt);
			}
		}
	}

	//获得点数后，开始移动棋子
	private void CarStep (int targetIndex, int stepNum)
	{
		CarLogic car = mCarList [currentPlayer.Index];
//		yield return new WaitForSeconds (0.5f);
		//每个玩家能取得的数据包括，当前的Player、当前的Ground，以及全体的Player、Ground列表
		currentPlayer.Position = targetIndex;//逻辑移动
		currentGround = mGroundList [targetIndex];//逻辑移动
		car.GoStep (mNodeList [targetIndex], stepNum);//传入目标Node和显示步数，棋子开始移动
	}

	//棋子移动后的响应函数
	void OnCarStatusChanged (CarStatus status)
	{
		switch (status) {
		case CarStatus.Stop:
			//隐藏箭头
			if (mStepTarget) {
				mStepTarget.SetActive (false);
			}
			//角色停住后进入交互阶段
			SetAction (Constants.ACTION_MEET_GIRL);
			//TEST
//			SetAction (Constants.ACTION_END_TURN);
			break;
		default:
			break;
		}
	}

	//------ Action部分 ---------------------------------

	public void SetAction (string action)
	{
		currentPlayer.SetAction (action);
	}

	//玩家角色事件回调函数
	//为了程序框架和玩法内容完全分离、所有玩法相关的操作都通过Action字符串互相串联，玩法相关的函数也全部在这里调用
	//这部分的完善建立在流程完善的基础上,TODO
	public void OnPlayerActionChanged (string action)
	{
		//开始玩家交互
		switch (action) {
		//
		case Constants.ACTION_MEET_GIRL:
			//尝试获取新女友
			Girl leisureGirl = GetLeisureGirl ();
			if (Utils.RandomMeetGirl () && leisureGirl != null) {
				print ("遇到女孩");
				UIManager.Instance.Open (typeof(PanelMeetGirl), ref leisureGirl);

			} else {
				SetAction (Constants.ACTION_ARRIVE_GROUND);
			}
			break;
		case Constants.ACTION_MEET_GIRL_YES:
			UIManager.Instance.Close (typeof(PanelMeetGirl));
			SetAction (Constants.ACTION_ARRIVE_GROUND);
			break;
		case Constants.ACTION_MEET_GIRL_NO:
			UIManager.Instance.Close (typeof(PanelMeetGirl));
			SetAction (Constants.ACTION_ARRIVE_GROUND);
			break;
		//
		case Constants.ACTION_ARRIVE_GROUND:
			if (currentGround.Owner == -2) {
				//中立区域
				print ("中立区域");
				switch (currentGround.Type) {
				case 1:
					//商店
					SetAction (Constants.ACTION_BUY_DRUG);
					break;
				default:
					break;
				}
			} else if (currentGround.Owner == -1) {
				//空白区域
				print ("空白区域");
				if (currentPlayer.Money >= currentGround.Price) {
					SetAction (Constants.ACTION_BUY_GROUND);
				} else {
					SetAction (Constants.ACTION_BUY_GROUND_NO_MONEY);
				}
			} else if (currentGround.Owner == currentPlayer.Index) {
				//自己区域
				print ("自己区域");
				SetAction (Constants.ACTION_END_TURN);//活动结束
			} else {
				//他人区域
				print (mPlayerList [currentGround.Owner].Name + " 的区域");
				SetAction (Constants.ACTION_PAY_TOLL);
			}
			break;
		//
		case Constants.ACTION_BUY_GROUND:
			UIManager.Instance.Open (typeof(PanelBuyGround));
			break;
		case Constants.ACTION_BUY_GROUND_YES:
			BuyGround ();
			UIManager.Instance.Close (typeof(PanelBuyGround));
			SetAction (Constants.ACTION_END_TURN);//活动结束
			break;
		case Constants.ACTION_BUY_GROUND_NO:
			UIManager.Instance.Close (typeof(PanelBuyGround));
			SetAction (Constants.ACTION_END_TURN);//活动结束
			break;
		//
		case Constants.ACTION_BUY_GROUND_NO_MONEY:
			UIManager.Instance.Open (typeof(PanelBuyGroundNoMoney));
			break;
		case Constants.ACTION_BUY_GROUND_NO_MONEY_CONFIRM:
			UIManager.Instance.Close (typeof(PanelBuyGroundNoMoney));
			SetAction (Constants.ACTION_END_TURN);//活动结束
			break;
		//
		case Constants.ACTION_PAY_TOLL:
			string str = string.Format ("到达了{0}的地盘，需要支付{1}过路费。", mPlayerList [currentGround.Owner].Name, currentGround.Level * 2000);
			UIManager.Instance.Open (typeof(PanelPayToll), str);
			break;
		case Constants.ACTION_PAY_TOLL_CONFIRM:
			//计算过路费,TODO,price计算后面再搞
//			int cost = Mathf.Clamp (currentGround.Level * 2000, 0, currentPlayer.Money);
			int cost = currentGround.Level * 2000;//允许玩家负债
			currentPlayer.AddMoney (-cost);
			mPlayerList [currentGround.Owner].AddMoney (cost);
			//玩家破产,破产算法不严谨，暂时不设定，最终胜负不看这个。

			UIManager.Instance.Close (typeof(PanelPayToll));
			SetAction (Constants.ACTION_END_TURN);//活动结束
			break;
		//
//		case Constants.ACTION_BREAKDOWN:
//			panelBreakdown.SetActive (true);
//			txtBreakdown.text = currentPlayer.Name + "破产了。。";
//			break;
//		case Constants.ACTION_BREAKDOWN_CONFIRM:
//			panelBreakdown.SetActive (false);
//			SetAction (Constants.ACTION_END_TURN);//活动结束
//			break;
		//
		case Constants.ACTION_BUY_DRUG:
			UIManager.Instance.Open (typeof(PanelBuyDrug));
			break;
		case Constants.ACTION_BUY_DRUG_CONFIRM:
			UIManager.Instance.Close (typeof(PanelBuyDrug));
			SetAction (Constants.ACTION_END_TURN);//活动结束
			break;
		//
		case Constants.ACTION_END_TURN:
			print ("回合结束");
			UIManager.Instance.Close (typeof(PanelMain));
			UIManager.Instance.Open (typeof(PanelEndTurn));

			//保存本回合数据
			currentPlayer.DataSave ();
			for (int i = 0; i < mGirlList.Count; i++) {
				mGirlList [i].DataSave ();
			}
			for (int i = 0; i < mGroundList.Count; i++) {
				mGroundList [i].DataSave ();
			}
			break;
		case Constants.ACTION_END_TURN_CONFIRM:
			UIManager.Instance.Close (typeof(PanelEndTurn));
			EndTurn ();
			StartTurn ();
			break;
		case Constants.ACTION_START_TURN_IN:
			//TODO,表现形式//			
//			UIManager.Instance.Close (typeof(PanelMain));
			UIManager.Instance.Open (typeof(PanelStartTurn));
			break;
		case Constants.ACTION_START_TURN_OUT:
			UIManager.Instance.Close (typeof(PanelStartTurn));
			UIManager.Instance.Open (typeof(PanelMain));
			break;
		case Constants.ACTION_GAME_OVER://游戏结束，主要目的是清空对局数据
			//TODO

			Application.Quit ();
			break;

		//------------道具-----------------

		case Constants.ACTIVATE_PACKAGE_0:
			Drug d0 = Drug.GetDrug (currentPlayer.GetItemList () [0]);
			if (d0.id != 0) {
				UIBase.ConfirmDelegate cd0 = new UIBase.ConfirmDelegate (delegate() {
					currentPlayer.DeleteItem (0);
					SetAction (d0.action);
				});
				UIManager.Instance.Open (typeof(PanelOptionalDialog), cd0, d0.name, d0.desc);
			}
			break;
		case Constants.ACTIVATE_PACKAGE_1:
			Drug d1 = Drug.GetDrug (currentPlayer.GetItemList () [1]);
			if (d1.id != 0) {
				UIBase.ConfirmDelegate cd1 = new UIBase.ConfirmDelegate (delegate() {
					currentPlayer.DeleteItem (1);
					SetAction (d1.action);
				});
				UIManager.Instance.Open (typeof(PanelOptionalDialog), cd1, d1.name, d1.desc);
			}
			break;
		case Constants.ACTIVATE_PACKAGE_2:
			Drug d2 = Drug.GetDrug (currentPlayer.GetItemList () [2]);
			if (d2.id != 0) {
				UIBase.ConfirmDelegate cd2 = new UIBase.ConfirmDelegate (delegate() {
					currentPlayer.DeleteItem (2);
					SetAction (d2.action);
				});
				UIManager.Instance.Open (typeof(PanelOptionalDialog), cd2, d2.name, d2.desc);
			}
			break;
		case Constants.ACTIVATE_PACKAGE_3:
			Drug d3 = Drug.GetDrug (currentPlayer.GetItemList () [3]);
			if (d3.id != 0) {
				UIBase.ConfirmDelegate cd3 = new UIBase.ConfirmDelegate (delegate() {
					currentPlayer.DeleteItem (3);
					SetAction (d3.action);
				});
				UIManager.Instance.Open (typeof(PanelOptionalDialog), cd3, d3.name, d3.desc);
			}
			break;
		case Constants.ACTIVATE_PACKAGE_4:
			Drug d4 = Drug.GetDrug (currentPlayer.GetItemList () [4]);
			if (d4.id != 0) {
				UIBase.ConfirmDelegate cd4 = new UIBase.ConfirmDelegate (delegate() {
					currentPlayer.DeleteItem (4);
					SetAction (d4.action);
				});
				UIManager.Instance.Open (typeof(PanelOptionalDialog), cd4, d4.name, d4.desc);
			}
			break;
		case Constants.ACTIVATE_PACKAGE_5:
			Drug d5 = Drug.GetDrug (currentPlayer.GetItemList () [5]);
			if (d5.id != 0) {
				UIBase.ConfirmDelegate cd5 = new UIBase.ConfirmDelegate (delegate() {
					currentPlayer.DeleteItem (5);
					SetAction (d5.action);
				});
				UIManager.Instance.Open (typeof(PanelOptionalDialog), cd5, d5.name, d5.desc);
			}
			break;
		case Constants.USE_ITEM_1:
			print ("喝了营养快线");
			break;
		case Constants.USE_ITEM_2:
			print ("喝了汇仁肾宝");
			break;
		case Constants.USE_ITEM_3:
			break;
		case Constants.USE_ITEM_4:
			break;
		case Constants.USE_ITEM_5:
			break;
		case Constants.USE_ITEM_6:
			break;
		case Constants.USE_ITEM_7:
			break;
		case Constants.USE_ITEM_8:
			break;
		case Constants.USE_ITEM_9:
			break;
		case Constants.USE_ITEM_10:
			break;
		default:
			break;
		}
	}

	//------ 游戏玩法相关的函数,函数主题都是CurrentPlayer ---------------------------------

	private void BuyGround ()
	{
		currentPlayer.AddMoney (-currentGround.Price);
		currentGround.SetGround (currentPlayer.Index, 1);//TODO,简单处理给个建筑
		mNodeList [currentGround.Index].mBuilding.SetPlayerBuilding (currentPlayer.Index);//目前的Ground操作和基于Node的Building操作其实是逻辑分离的
	}

	private void BreakDown ()
	{
		
	}

	//------- 数据获取,当已有CurrentPlayer时，获取其他玩家列表，以及每个人的Ground和Girl列表,这部分函数要扩展，UI层也要调用,TODO

	public List<Player> GetAllPlayer ()
	{
		return mPlayerList;
	}

	public Player GetCurrentPlayer ()
	{
		return currentPlayer;
	}

	private List<Player> GetOtherPlayer ()
	{
		return mPlayerList.FindAll ((Player p) => {
			return p.Index != currentPlayer.Index;
		});
	}

	private List<Ground> GetPlayerGround (int pIndex)
	{
		return mGroundList.FindAll ((Ground g) => {
			return g.Owner == pIndex;
		});
	}

	public List<Girl> GetPlayerGirl (int pIndex)
	{
		return mGirlList.FindAll ((Girl g) => {
			return g.Owner == pIndex;
		});
	}

	public Girl GetLeisureGirl ()
	{
		return mGirlList.Find ((Girl g) => {
			return g.Owner == -1 && g.LastOwner != currentPlayer.Index;
		});
	}

}

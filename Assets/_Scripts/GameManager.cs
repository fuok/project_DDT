using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance{ get; private set; }

	//棋子移动
	[SerializeField]
	private CarLogic[] mCarList;
	[SerializeField]
	private MapNode[] mNodeList;
	//当前的逻辑角色
	Player currentPlayer;
	Ground currentGround;

	//骰子相机控制
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

		//初始化游戏数据，Player、Ground、Girl都在这里，后期可以放到静态对象里
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 0, "刘玄德(红)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 1, "雷锋(黄)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 2, "奥巴马(蓝)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 3, "金三胖(绿)");

		PlayerPrefs.SetString (Constants.GIRL_SAVE_NAME + 0, "林志玲");
		PlayerPrefs.SetString (Constants.GIRL_SAVE_NAME + 1, "波多野结衣");
		PlayerPrefs.SetString (Constants.GIRL_SAVE_NAME + 2, "张雨桐");
		PlayerPrefs.SetString (Constants.GIRL_SAVE_NAME + 3, "王熙凤");

		//构造测试数据

		//Player
		for (int i = 0; i < 4; i++) {
			Player p = new Player (i, PlayerPrefs.GetString (Constants.PLAYER_SAVE_NAME + i, "player" + i)) {
				Money = Constants.DEFAULT_MONEY,
				Health = Constants.DEFAULT_HEALTH
			};
			//注册玩家事件
			p.PlayerActionEvent += GameManager.Instance.OnPlayerActionChanged;
			mPlayerList.Add (p);
		}
		TurnManager.Instance.SetPlayerQueue (ref mPlayerList);
		//Ground
		mGroundList = new List<Ground> {
			new Ground{ Index = 0, Owner = -2, Type = 0 },//起点
			new Ground{ Index = 1, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 2, Owner = -2, Type = 1  },//
			new Ground{ Index = 3, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 4, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 5, Owner = -2, Type = 1 },//
			new Ground{ Index = 6, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 7, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 8, Owner = -2, Type = 1  },//
			new Ground{ Index = 9, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 10, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 11, Owner = -2, Type = 1  },//
			new Ground{ Index = 12, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 13, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 14, Owner = -2, Type = 1 },//
			new Ground{ Index = 15, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 16, Owner = -2, Type = 1 },//中位
			new Ground{ Index = 17, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 18, Owner = -2, Type = 1  },//
			new Ground{ Index = 19, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 20, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 21, Owner = -2, Type = 1 },//
			new Ground{ Index = 22, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 23, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 24, Owner = -2, Type = 1 },//
			new Ground{ Index = 25, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 26, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 27, Owner = -2, Type = 1  },//
			new Ground{ Index = 28, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 29, Owner = -1, Level = 0, Price = 3000 },
			new Ground{ Index = 30, Owner = -2, Type = 1 },//
			new Ground{ Index = 31, Owner = -1, Level = 0, Price = 3000 }
			
		};
		for (int i = 0; i < mGroundList.Count; i++) {
			if (mGroundList [i].Owner == -2) {
				mNodeList [i].mBuilding.SetNeutralBuilding (mGroundList [i].Type);
			}
		}

		//Girl
		for (int i = 0; i < 4; i++) {
			Girl girl = new Girl {
				Index = i,
				Owner = -1,
				LastOwner = -1,
				Name = PlayerPrefs.GetString (Constants.GIRL_SAVE_NAME + i, "girl" + i), Salary = 1000
			};
			mGirlList.Add (girl);
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
		currentPlayer = TurnManager.Instance.GetCurrentPlayer ();

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
	void OnDiceResult (int para)
	{
		print ("掷出" + para);
		StartCoroutine (CheckResult (para));
	}

	private int tempRslt;
	private bool resultChecked;

	IEnumerator CheckResult (int para)
	{
		tempRslt = para;
		yield return new WaitForSeconds (0.7f);//延迟0.5秒检查稳定后的数值

		if (para == tempRslt) {//判断数值稳定
			DiceManager.Instance.diceEvent -= OnDiceResult;
			if (resultChecked == false) {
				resultChecked = true;

				print ("前进:" + para);
				CarStep (para);
				mDiceCameraContainer.SetActive (false);
			}
		}
	}

	//获得点数后，开始移动棋子
	void CarStep (int num)
	{
		CarLogic car = mCarList [currentPlayer.Index];
		//计算移动后所处的Node序列
		int target = mNodeList.Length > (car.mCurrentNode.mNodeIndex + num) ? car.mCurrentNode.mNodeIndex + num : (car.mCurrentNode.mNodeIndex + num) % mNodeList.Length;
		print (currentPlayer.Name + "掷出" + num + ",前进到" + target);

		//每个玩家能取得的数据包括，当前的Player、当前的Ground，以及全体的Player、Ground列表
		currentGround = mGroundList [target];//逻辑移动
		car.GoStep (mNodeList [target]);//棋子移动
	}

	//棋子移动后的响应函数
	void OnCarStatusChanged (CarStatus status)
	{
		switch (status) {
		case CarStatus.Stop:
			SetAction (Constants.ACTION_MEET_GIRL);//角色停住后进入交互阶段
			break;
		default:
			break;
		}
	}

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
			currentPlayer.DataSave ();
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
			PlayerPrefs.SetInt (Constants.GAME_ROUND_NUMBER, 0);

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

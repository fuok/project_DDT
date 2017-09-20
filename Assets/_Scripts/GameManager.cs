using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance{ get; private set; }

	//棋子移动
	public CarLogic[] mCarList;
	public MapNode[] mNodeList;
	//当前的逻辑角色
	Player currentPlayer;
	Ground currentGround;

	//调试控制
	public Camera camDice;
	public Button btnRoll;
	public Text txtDiceResult;
	public Text txtCurrentPlayer;
	public Text[] txtPlayerInfo;

	//UI
	[Header ("UI")]
	public GameObject panelEndTurn;
	public Button btnEndTurn;
	public GameObject panelBuyGround;
	public Button btnBuyGroundYes;
	public Button btnBuyGroundNo;
	public GameObject panelBuyGroundNoMoney;
	public Button btnBuyGroundNoMoney;

	//缓存游戏数据,相当于从DB读取出来的数据
	private List<Player> mPlayerList = new List<Player> ();
	private List<Ground> mGroundList = new List<Ground> ();

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		//初始化游戏数据，Player、Ground、Girl都在这里，后期可以放到静态对象里
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 0, "刘玄德(红)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 1, "林志玲(黄)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 2, "奥巴马(蓝)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 3, "苍井空(绿)");

		//角色初始化
		for (int i = 0; i < 4; i++) {
			Player p = new Player (i, PlayerPrefs.GetString (Constants.PLAYER_SAVE_NAME + i, "player" + i), Constants.DEFAULT_MONEY);
			//注册玩家事件
			p.PlayerActionEvent += GameManager.Instance.OnPlayerActionChanged;
			mPlayerList.Add (p);
		}

		TurnManager.Instance.SetPlayerQueue (ref mPlayerList);
	}

	void Start ()
	{

		//Test,创造一组Ground数据
		for (int i = 0; i < mNodeList.Length; i++) {
			Ground ground = new Ground{ Index = i, Owner = -1, Level = 0, Price = 3000 };
			mGroundList.Add (ground);
		}
			
		//棋子状态监听
		foreach (var item in mCarList) {
			item.carMoveEvent += OnCarStatusChanged;
		}

		//Init UI
		btnRoll.onClick.AddListener (RollDice);
		btnBuyGroundYes.onClick.AddListener (() => {
			currentPlayer.SetAction (Constants.ACTION_BUY_GROUND_YES);
		});
		btnBuyGroundNo.onClick.AddListener (() => {
			currentPlayer.SetAction (Constants.ACTION_BUY_GROUND_NO);
		});
		btnBuyGroundNoMoney.onClick.AddListener (() => {
			currentPlayer.SetAction (Constants.ACTION_BUY_GROUND_NO_MONEY_CONFIRM);
		});
		btnEndTurn.onClick.AddListener (() => {
			currentPlayer.SetAction (Constants.ACTION_END_TURN_CONFIRM);
		});

		//游戏开始
		//初始化调用一次，所以第一个行动的是Player1，而不是Player0
		EndTurn ();
	}

	void Update ()
	{
		//Test
		for (int i = 0; i < 4; i++) {
			txtPlayerInfo [i].text = mPlayerList [i].Name + "\n" + "金钱:" + mPlayerList [i].Money;
		}
	}

	//---------------------------------------------------------------------------------------------------------

	void EndTurn ()
	{
		TurnManager.Instance.StopPlayerMoving ();
		currentPlayer = TurnManager.Instance.CurrentPlayer;
		txtCurrentPlayer.text = currentPlayer.Name + ",回合开始！";
	}

	void RollDice ()
	{
		resultChecked = false;
		DiceManager.Instance.diceEvent += OnDiceResult;//这个方法常驻监听，不是太好，但如果不这样数值会不准，因为骰子的状态不稳定,除非确定稳定后再回调
		camDice.enabled = true;
		DiceManager.Instance.RollDice ();
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
		yield return new WaitForSeconds (0.5f);//延迟0.5秒检查稳定后的数值

		if (para == tempRslt) {//判断数值稳定
			DiceManager.Instance.diceEvent -= OnDiceResult;
			if (resultChecked == false) {
				resultChecked = true;

				print ("前进:" + para);
				CarStep (para);
				camDice.enabled = false;
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
		txtDiceResult.text = currentPlayer.Name + "掷出" + num + ",前进到" + target;

		//每个玩家能取得的数据包括，当前的Player、当前的Ground，以及全体的Player、Ground列表
		currentGround = mGroundList [target];//逻辑移动
		car.GoStep (mNodeList [target]);//棋子移动
	}

	//棋子移动后的响应函数
	void OnCarStatusChanged (CarStatus status)
	{
		switch (status) {
		case CarStatus.Stop:
			currentPlayer.SetAction (Constants.ACTION_ARRIVE_GROUND);//角色停住后进入交互阶段
			break;
		default:
			break;
		}
	}

	//玩家角色事件回调函数
	//为了程序框架和玩法内容完全分离、所有玩法相关的操作都通过Action字符串互相串联，玩法相关的函数也全部在这里调用
	//这部分的完善建立在流程完善的基础上,TODO
	public void OnPlayerActionChanged (string action)
	{
		//开始玩家交互,TODO
		switch (action) {
		case Constants.ACTION_ARRIVE_GROUND:
			if (currentGround.Owner == -2) {
				//中立区域
				print ("中立区域");
			} else if (currentGround.Owner == -1) {
				//空白区域
				print ("空白区域");
				if (currentPlayer.Money > currentGround.Price) {
					currentPlayer.SetAction (Constants.ACTION_BUY_GROUND);
				} else {
					currentPlayer.SetAction (Constants.ACTION_BUY_GROUND_NO_MONEY);
				}
			} else {
				//有主区域
				print (mPlayerList [currentGround.Owner].Name + " 的区域");
				currentPlayer.SetAction (Constants.ACTION_END_TURN);//活动结束
			}
			break;
		case Constants.ACTION_BUY_GROUND:
			panelBuyGround.SetActive (true);
			break;
		case Constants.ACTION_BUY_GROUND_YES:
			BuyGround ();
			panelBuyGround.SetActive (false);
			currentPlayer.SetAction (Constants.ACTION_END_TURN);//活动结束
			break;
		case Constants.ACTION_BUY_GROUND_NO:
			panelBuyGround.SetActive (false);
			currentPlayer.SetAction (Constants.ACTION_END_TURN);//活动结束
			break;
		case Constants.ACTION_BUY_GROUND_NO_MONEY:
			panelBuyGroundNoMoney.SetActive (true);
			break;
		case Constants.ACTION_BUY_GROUND_NO_MONEY_CONFIRM:
			panelBuyGroundNoMoney.SetActive (false);
			currentPlayer.SetAction (Constants.ACTION_END_TURN);//活动结束
			break;
		case Constants.ACTION_END_TURN:
			print ("回合结束");
			panelEndTurn.SetActive (true);
			break;
		case Constants.ACTION_END_TURN_CONFIRM:
			panelEndTurn.SetActive (false);
			EndTurn ();
			break;
		default:
			break;
		}
	}

	//------ 游戏玩法相关的函数 ---------------------------------

	private void BuyGround ()
	{
		currentPlayer.Money -= currentGround.Price;
		currentGround.SetGround (currentPlayer.Index);
		mNodeList [currentGround.Index].mBuilding.SetBuilding (currentPlayer.Index);//目前的Ground操作和基于Node的Building操作其实是逻辑分离的
	}

}

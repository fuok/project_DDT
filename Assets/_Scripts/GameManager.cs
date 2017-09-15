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
	Player currentPlayer;
	Ground currentGround;

	//调试控制
	public Button btnRoll;
	public Text txtDiceResult;
	public Camera camDice;
	public Text txtCurrentPlayer;

	//UI
	[Header ("UI")]
	public GameObject panelEndTurn;
	public Button btnEndTurn;

	//缓存Ground和Girl
	private List<Ground> groundList = new List<Ground> ();

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

		//Test,创造一组Ground数据
		for (int i = 0; i < mNodeList.Length; i++) {
			Ground ground = new Ground{ Index = i, Owner = -1, Level = 0, Price = 1000 };
			groundList.Add (ground);
		}


		//棋子状态监听
		foreach (var item in mCarList) {
			item.carMoveEvent += OnCarStatusChanged;
		}

		//Init UI

		btnRoll.onClick.AddListener (RollDice);
		btnEndTurn.onClick.AddListener (() => {
			panelEndTurn.SetActive (false);
		});

		//游戏开始
		//初始化调用一次，所以第一个行动的是Player1，而不是Player0
		EndTurn ();
	}

	void Update ()
	{
		
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
		yield return new WaitForSeconds (0.5f);

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

		int target = mNodeList.Length > (car.mCurrentNode.mNodeIndex + num) ? car.mCurrentNode.mNodeIndex + num : (car.mCurrentNode.mNodeIndex + num) % mNodeList.Length;
		print (currentPlayer.Name + "掷出" + num + ",前进到" + target);
		txtDiceResult.text = currentPlayer.Name + "掷出" + num + ",前进到" + target;

		//每个玩家能取得的数据包括，当前的Player、当前的Ground，以及全体的Player、Ground列表
		currentGround = groundList [target];//逻辑移动
		car.GoStep (mNodeList [target]);//棋子移动
	}

	//棋子移动后的响应函数
	void OnCarStatusChanged (CarStatus status)
	{
		switch (status) {
		case CarStatus.Stop:
//			print ("车子停了");

			currentPlayer.SetAction (Constants.ACTION_ARRIVE_GROUNG);

			break;
		default:
			break;
		}
	}

	//玩家角色事件回调函数
	public void OnPlayerActionChanged (string action)
	{
		//开始玩家交互,TODO
		switch (action) {
		case Constants.ACTION_ARRIVE_GROUNG:
			if (currentGround.Owner == -2) {
				//中立区域
			} else if (currentGround.Owner == -1) {
				//空白区域
				currentPlayer.SetAction (Constants.ACTION_BUY_GROUNG);
			} else {
				//有主区域
			}

			break;
		case Constants.ACTION_BUY_GROUNG:
			print ("买地");

			currentPlayer.SetAction (Constants.ACTION_END_TURN);
			break;

		case Constants.ACTION_END_TURN:
			print ("回合结束");
			EndTurn ();
			panelEndTurn.SetActive (true);
			break;


		default:
			break;
		}
	}

}

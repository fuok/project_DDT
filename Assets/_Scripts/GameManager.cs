using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance{ get; private set; }

	//角色移动
	public CarLogic mCar;
	public MapNode[] mNodeList;

	//调试控制
	public Button btnRoll;
	public Text txtDiceResult;
	public Camera camDice;
	public Button btnNextTurn;
	public Text txtCurrentPlayer;

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
		btnRoll.onClick.AddListener (() => {
			resultChecked = false;
			DiceManager.Instance.diceEvent += ShowDiceResult;//这个方法常驻监听，不是太好，但如果不这样数值会不准，因为骰子的状态不稳定,除非确定稳定后再回调
			camDice.enabled = true;
			DiceManager.Instance.RollDice ();
		});

		btnNextTurn.onClick.AddListener (() => {
			txtCurrentPlayer.text = TurnManager.Instance.CurrentPlayer.Name + ",正在行动";//TODO
			TurnManager.Instance.StopPlayerMoving ();
		});
	}

	void Update ()
	{
		
	}

	//---------------------------------------------------------------------------------------------------------

	//输出骰子点数，只要骰子动作在监听，这里就会一直调用，而且输出的数值是会变的，需要进一步检查才能确认
	void ShowDiceResult (int para)
	{
		txtDiceResult.text = "value=" + para;
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
			DiceManager.Instance.diceEvent -= ShowDiceResult;
			if (resultChecked == false) {
				resultChecked = true;

				print ("前进:" + para);
				CarStep (para);
				camDice.enabled = false;
			}
		}
	}

	//
	void CarStep (int num)
	{
		int target = mNodeList.Length > (mCar.mCurrentNode.mNodeIndex + num) ? mCar.mCurrentNode.mNodeIndex + num : (mCar.mCurrentNode.mNodeIndex + num) % mNodeList.Length;
		print ("掷出" + num + ",前进到" + target);
		mCar.GoStep (mNodeList [target]);
	}
}

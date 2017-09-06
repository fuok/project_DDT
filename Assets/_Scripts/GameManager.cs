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

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
//		DontDestroyOnLoad (gameObject);
		DiceManager.Instance.diceEvent += ShowDiceResult;
	}

	void Start ()
	{
		btnRoll.onClick.AddListener (() => {
			camDice.enabled = true;
			DiceManager.Instance.RollDice ();
		});
	}

	void Update ()
	{
		
	}

	void ShowDiceResult (string para)
	{
		if (para.Contains ("=")) {//临时处理，之后要改，不能一直刷新状态,实际上原Demo中有对于Dice对象物理状态稳定的判断,TODO
			txtDiceResult.text = para;
			camDice.enabled = false;
		}
	}

	public void CarStep (int num)
	{
		int target = mNodeList.Length > (mCar.mCurrentNode.mNodeIndex + num) ? mCar.mCurrentNode.mNodeIndex + num : (mCar.mCurrentNode.mNodeIndex + num) % mNodeList.Length;
		print ("目标：" + target);
		mCar.GoStep (mNodeList [target]);
	}
}

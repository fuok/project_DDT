using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//骰子控制，以后UI部分不放在这里
public class DiceManager : MonoBehaviour
{
	public static DiceManager Instance{ get; private set; }

	public delegate void DiceHandler (int rslt);

	public event DiceHandler diceEvent;

	public Transform spawnPoint;
	//	private string galleryDie = "d6-red-dots";

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
		StartCoroutine (DiceCoroutine ());
	}

	void Update ()
	{

	}

	//不要常驻Update，放到单独协程里
	private IEnumerator DiceCoroutine ()
	{
		restart:
		yield return new WaitForSeconds (0.1f);
		if (diceEvent != null) {
			//	可进一步判断AsString返回值
			//			diceEvent (Dice.AsString (""));
			int rslt = Dice.Value ("");
			if (rslt > 0) {
				diceEvent (rslt);//改用直接取值
			}
		}
		goto restart;
	}

	//控制骰子的数量和种类,这里写死是2个，种类也是写死d6、dots，但颜色可以改变
	public GameObject[] RollDice (int color)
	{
		Dice.Clear ();
//		string[] a = galleryDie.Split ('-');
		string colorStr;
		switch (color) {
		case 0:
			colorStr = "red";
			break;
		case 1:
			colorStr = "yellow";
			break;
		case 2:
			colorStr = "blue";
			break;
		case 3:
			colorStr = "green";
			break;
		default:
			colorStr = "red";
			break;
		}
		return Dice.Roll ("2" + "d6", "d6-" + colorStr + "-dots", spawnPoint.transform.position, Force ());
	}

	private Vector3 Force ()
	{
		Vector3 rollTarget = Vector3.zero + new Vector3 (2 + 7 * Random.value, .5F + 4 * Random.value, -2 - 3 * Random.value);
		return Vector3.Lerp (spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
	}

}

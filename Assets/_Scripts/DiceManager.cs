using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//骰子控制，以后UI部分不放在这里
public class DiceManager : MonoBehaviour
{
	public static DiceManager Instance{ get; private set; }

	public delegate void DiceHandler (int param);

	public event DiceHandler diceEvent;

	public Transform spawnPoint;
	private string galleryDie = "d10-green";

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

	//控制骰子的数量和种类
	public void RollDice ()
	{
		Dice.Clear ();
		string[] a = galleryDie.Split ('-');
		Dice.Roll ("1" + a [0], galleryDie, spawnPoint.transform.position, Force ());
	}

	private Vector3 Force ()
	{
		Vector3 rollTarget = Vector3.zero + new Vector3 (2 + 7 * Random.value, .5F + 4 * Random.value, -2 - 3 * Random.value);
		return Vector3.Lerp (spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
	}

}

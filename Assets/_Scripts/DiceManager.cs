using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
	public static DiceManager instance;

	public delegate void DiceHandler (string param);

	public event DiceHandler diceEvent;

	public Button btnRoll;
	//	public Text txtRollResult;
	public Transform spawnPoint;
	private string galleryDie = "d6-green";

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void Start ()
	{
		btnRoll.onClick.AddListener (RollDice);
	}

	void Update ()
	{
		if (diceEvent != null) {
			//	可进一步判断AsString返回值
			diceEvent (Dice.AsString (""));
		}
//		txtRollResult.text = Dice.AsString ("");
	}

	void RollDice ()
	{
		Dice.Clear ();
		string[] a = galleryDie.Split ('-');
		Dice.Roll ("2" + a [0], galleryDie, spawnPoint.transform.position, Force ());
	}

	private Vector3 Force ()
	{
		Vector3 rollTarget = Vector3.zero + new Vector3 (2 + 7 * Random.value, .5F + 4 * Random.value, -2 - 3 * Random.value);
		return Vector3.Lerp (spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
	}

}

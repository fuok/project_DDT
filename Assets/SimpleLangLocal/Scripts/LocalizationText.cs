using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode ()]需要从Start获取全局对象，直接调用没用
public class LocalizationText : MonoBehaviour
{
	public string key;

	void Start ()
	{
		if (!string.IsNullOrEmpty (key)) {
			GetComponent<Text> ().text = LocalizationManager.Instance.GetWord (key);
		}
	}

	void OnEnable ()
	{
		print ("OnEnable ()");
		if (!string.IsNullOrEmpty (key)) {
			GetComponent<Text> ().text = LocalizationManager.Instance.GetWord (key);
		}
	}

	//现在还不能实时刷新当前页面的
	//	void Update ()
	//	{
	//		GetComponent<Text> ().text = LocalizationManager.Instance.GetWord (key);
	//	}
}

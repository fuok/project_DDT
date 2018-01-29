using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CustomHelper:UnitySigleton<CustomHelper>
{

	public void SetGirlPortrait (RawImage img, Girl girl)
	{
		string localPath = Constants.SAVE_PATH + string.Format ("girl_{0}_portrait_custom.png", girl.Index.ToString ());
		if (PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1 && File.Exists (localPath)) {//自定义
			//Constants.SAVE_PATH + DateTime.Now.ToFileTime ().ToString () + ".png";
			StartCoroutine (LoadLocalImage (img, localPath));
		} else {//默认
			img.texture = Resources.Load<Texture> (string.Format ("Girl Image Default/girl_{0}_portrait_default", girl.Index.ToString ()));
		}
	}

	IEnumerator LoadLocalImage (RawImage img, string filePath)
	{
		WWW www = new WWW ("file://" + filePath);
		yield return www;
		img.texture = www.texture;
	}

	public string GetGirlName (Girl girl)
	{
		return null;
	}
}

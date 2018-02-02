using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CustomHelper:UnitySigleton<CustomHelper>
{
	/// <summary>
	/// 在指定位置加载女生头像
	/// </summary>
	/// <param name="img">Image.</param>
	/// <param name="girl">Girl.</param>
	public void SetGirlPortrait (RawImage img, Girl girl)
	{
		string localPath = Constants.SAVE_PATH + string.Format (Constants.CUSTOM_GIRL_PORTRAIT_PATH, girl.Index.ToString ());
		if (PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1 && File.Exists (localPath)) {//自定义
			//Constants.SAVE_PATH + DateTime.Now.ToFileTime ().ToString () + ".png";
			StartCoroutine (LoadLocalImage (img, localPath));
		} else {//默认
			img.texture = Resources.Load<Texture> (string.Format (Constants.DEFAULT_GIRL_PORTRAIT_PATH, girl.Index.ToString ()));
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
		string name;
		string localName = PlayerPrefs.GetString (string.Format (Constants.CUSTOM_GIRL_NAME, girl.Index.ToString ()), "");
		if (PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1 && !string.IsNullOrEmpty (localName)) {//自定义
			name = localName;
		} else {//默认
			name = girl.Name;
		}
		return name;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomHelper
{

	public static string GetPortraitPath (Girl girl)
	{
		string path;
		if (PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 0) {
			path = string.Format ("Girl Image Default/girl_{0}_portrait_default", girl.Index.ToString ());
		} else {
			//Constants.SAVE_PATH + DateTime.Now.ToFileTime ().ToString () + ".png";
//			path=Constants.SAVE_PATH+
		}


		return null;
	}

	IEnumerator LoadLocalImage (RawImage img, string filePath)
	{
		WWW www = new WWW ("file://" + filePath);
		yield return www;
		img.texture = www.texture;
	}
}

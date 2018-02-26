using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Utils
{

	/// <summary>
	/// 获取制定随机事件的发生结果
	/// </summary>
	/// <returns><c>true</c>, if random result was gotten, <c>false</c> otherwise.</returns>
	/// <param name="percent">获取事件的概率(百分比)</param>
	private static bool GetRandomResult (float percent)
	{
		//Random.value方法返回一个0—1的随机数
		float randomPoint = UnityEngine.Random.value * 100;
		Debug.Log ("事件概率：" + percent + ",随机得到：" + randomPoint);
		return percent > randomPoint;
	}

	//游戏事件随机函数,不同函数传入的概率不一样

	//遇到新女友
	public static bool RandomMeetGirl ()
	{
		return GetRandomResult (50f);
	}

	//------------------------------------------------------------------------------------------------------

	public static string WriteRenderTexture2File (RenderTexture rt)
	{
		RenderTexture.active = rt;
		Texture2D tex2D = new Texture2D (rt.width, rt.height, TextureFormat.RGB24, false);
		tex2D.ReadPixels (new Rect (0, 0, rt.width, rt.height), 0, 0);
		tex2D.Apply ();
		RenderTexture.active = null;
		string path = Constants.SAVE_PATH + DateTime.Now.ToFileTime ().ToString () + ".png";
		File.WriteAllBytes (path, tex2D.EncodeToPNG ());
		return path;
	}

	public static string WriteTexture2D2File (Texture2D tex2D, string savePath)
	{
//		RenderTexture.active = rt;
//		Texture2D tex2D = new Texture2D (rt.width, rt.height, TextureFormat.RGB24, false);
//		tex2D.ReadPixels (new Rect (0, 0, rt.width, rt.height), 0, 0);
//		tex2D.Apply ();
//		RenderTexture.active = null;
//		string path = savePath + ".png";
		File.WriteAllBytes (savePath, tex2D.EncodeToPNG ());
		return savePath;
	}

	public static void DeleteFile (string path)
	{
		if (File.Exists (path)) {
			File.Delete (path);
		}
	}

	public static Color Hex2RGB (string hexStr)
	{
		//value = #ab364f
		//		Debug.Log (hexStr);
		int r = Convert.ToInt32 ("0x" + hexStr.Substring (1, 2), 16);
		int g = Convert.ToInt32 ("0x" + hexStr.Substring (3, 2), 16);
		int b = Convert.ToInt32 ("0x" + hexStr.Substring (5, 2), 16);
		Color color = new Color (r / 255f, g / 255f, b / 255f);
		//		Debug.Log (color.ToString ());
		return color;
	}
}

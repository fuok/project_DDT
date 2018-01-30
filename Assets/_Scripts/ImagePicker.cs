using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ImageAndVideoPicker;

public class ImagePicker : MonoBehaviour
{
	//	public Button btnPickImg;
	//	public RawImage imgMain;

	public delegate void ImageLoadedHandler (int index,Texture2D rsltTexture);

	event ImageLoadedHandler ImageLoaded;

	//	void Start ()
	//	{
	//		btnPickImg.onClick.AddListener (() => {
	//			#if UNITY_ANDROID
	//			AndroidPicker.BrowseImage (true);
	//			#elif UNITY_IPHONE
	//			IOSPicker.BrowseImage (true); // true for pick and crop
	//			#endif
	//		});
	//	}

	//	void Update ()
	//	{
	//
	//	}

	public void OpenImage (ImageLoadedHandler handler)
	{
		ImageLoaded -= handler;

		#if UNITY_ANDROID
		AndroidPicker.BrowseImage (true);
		#elif UNITY_IPHONE
		IOSPicker.BrowseImage (true); // true for pick and crop
		#endif

		ImageLoaded += handler;
	}

	void OnEnable ()
	{
		PickerEventListener.onImageSelect += OnImageSelect;
		PickerEventListener.onImageLoad += OnImageLoad;
		PickerEventListener.onVideoSelect += OnVideoSelect;
		PickerEventListener.onError += OnError;
		PickerEventListener.onCancel += OnCancel;
	}

	void OnDisable ()
	{
		PickerEventListener.onImageSelect -= OnImageSelect;
		PickerEventListener.onImageLoad -= OnImageLoad;
		PickerEventListener.onVideoSelect -= OnVideoSelect;
		PickerEventListener.onError -= OnError;
		PickerEventListener.onCancel -= OnCancel;
	}


	void OnImageSelect (string imgPath, ImageAndVideoPicker.ImageOrientation imgOrientation)
	{
		Debug.Log ("Image Location : " + imgPath);
	}


	void OnImageLoad (string imgPath, Texture2D tex, ImageAndVideoPicker.ImageOrientation imgOrientation)
	{
		Debug.Log ("Image Location : " + imgPath);
//		imgMain.texture = tex;//这里图片是拉伸显示，最好是有crop功能。
		//替换进来的图片本地保存,默认图和替换图的存储机制相同,通过标记判断读取哪个,TODO
		ImageLoaded (0, tex);
	}

	void OnVideoSelect (string vidPath)
	{
		Debug.Log ("Video Location : " + vidPath);
		Handheld.PlayFullScreenMovie ("file://" + vidPath, Color.blue, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
	}

	void OnError (string errorMsg)
	{
		Debug.Log ("Error : " + errorMsg);
	}

	void OnCancel ()
	{
		Debug.Log ("Cancel by user");
	}
}

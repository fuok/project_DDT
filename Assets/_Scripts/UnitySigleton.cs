using UnityEngine;

public class UnitySigleton<T> : MonoBehaviour where T : class
{
	public static T Instance{ private set; get; }
	//线程安全使用
	private static readonly object _lockObj = new object ();

	protected virtual void Awake ()
	{
		lock (_lockObj) {
			if (Instance == null) {
				Instance = this as T;
			} else {
				DestroyImmediate (this);
				return;
			}
		}
	}

	protected virtual void OnDestroy ()
	{
		lock (_lockObj) {
			if (Instance == this as T) {
				Instance = null;
			}
		}
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum CarStatus
{
	Moving,
	Stop
}

//棋子逻辑，棋子和Player概念不同
public class CarLogic : MonoBehaviour
{
	public MapNode mCurrentNode;
	public MapNode mTargetNode;
	public CarStatus status = CarStatus.Stop;
	[Range (20f, 100f)]
	public float mSpeed = 20f;
	//移动气泡
	public GameObject mStepPop;
	public Text txtStepNum;

	public delegate void CarMoveHandler (CarStatus status);

	public event CarMoveHandler carMoveEvent;

	void Start ()
	{
//		transform.position = mCurrentNode.transform.position;
//		SetPosition (mCurrentNode);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (status == CarStatus.Moving) {

			if (mCurrentNode == mTargetNode) {
				//到达目的地，要在GM里监听此状态
				status = CarStatus.Stop;
				//传递状态
				carMoveEvent (status);
				//隐藏移动气泡
				mStepPop.SetActive (false);
			} else {
				
				transform.position = Vector3.MoveTowards (transform.position, mCurrentNode.AftNode.transform.position, mSpeed * Time.deltaTime);
				transform.LookAt (mCurrentNode.AftNode.transform.position, Vector3.up);

				if (Vector3.Distance (transform.position, mCurrentNode.AftNode.transform.position) < 0.1f) {
					//走向下一目标节点
					mCurrentNode = mCurrentNode.AftNode;
					//气泡步数减1
					txtStepNum.text = (int.Parse (txtStepNum.text) - 1).ToString ();
				}
			}

			RefreshPop ();

		}
	}

	//--------------------------------------------------------------------------------------------

	/// <summary>
	/// 游戏开始时给车子定位
	/// </summary>
	/// <param name="node">Node.</param>
	public void SetPosition (MapNode node)
	{
		print ("SetPosition:" + node.gameObject.name);
		mCurrentNode = node;
		transform.position = mCurrentNode.transform.position;
		transform.rotation = mCurrentNode.transform.rotation;
	}

	public void PrepareGo (MapNode target, int stepNum)
	{
		mTargetNode = target;
		//显示气泡提示剩余步数
		RefreshPop ();
		txtStepNum.text = stepNum.ToString ();
		mStepPop.SetActive (true);

		StartCoroutine (GoGoGo ());
	}

	IEnumerator GoGoGo ()
	{
		yield return new WaitForSeconds (0.5f);
		status = CarStatus.Moving;
	}

	//Step气泡跟随本体
	private void RefreshPop ()
	{
		if (mStepPop) {
			Vector3 vScreen = Camera.main.WorldToScreenPoint (transform.position);
			float canvasX = vScreen.x - Camera.main.pixelWidth / 2;
			float canvasY = vScreen.y - Camera.main.pixelHeight / 2;
			float deltaY = Camera.main.pixelHeight / 20;//显示高度抬高一点
			mStepPop.transform.localPosition = new Vector3 (canvasX, canvasY + deltaY, 0f);
		}
	}

}

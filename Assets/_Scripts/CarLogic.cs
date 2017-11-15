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
		transform.position = mCurrentNode.transform.position;
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


			//Step气泡跟随
			if (mStepPop) {
				Vector3 vScreen = Camera.main.WorldToScreenPoint (transform.position);
				//		print ("vScreen=" + vScreen.ToString ());
				float canvasX = vScreen.x - Camera.main.pixelWidth / 2;
				float canvasY = vScreen.y - Camera.main.pixelHeight / 2;
				mStepPop.transform.localPosition = new Vector3 (canvasX, canvasY + 100, 0f);//还未做适配,TODO
			}

		}
	}

	public void GoStep (MapNode target, int stepNum)
	{
		mTargetNode = target;
		status = CarStatus.Moving;
		//显示气泡提示角色位置
		mStepPop.SetActive (true);
		txtStepNum.text = stepNum.ToString ();
	}
}

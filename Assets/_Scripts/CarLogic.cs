using UnityEngine;
using System.Collections;

public enum CarStatus
{
	Moving,
	Stop
}

public class CarLogic : MonoBehaviour
{
	public MapNode mCurrentNode;
	public MapNode mTargetNode;
	public CarStatus status = CarStatus.Stop;
	[Range (5f, 20f)]
	public float mSpeed = 5f;

	// Use this for initialization
	void Start ()
	{
		transform.position = mCurrentNode.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (status == CarStatus.Moving) {

			if (mCurrentNode == mTargetNode) {
				//到达
				status = CarStatus.Stop;
			} else {
				
				transform.position = Vector3.MoveTowards (transform.position, mCurrentNode.AftNode.transform.position, mSpeed * Time.deltaTime);
				transform.LookAt (mCurrentNode.AftNode.transform.position, Vector3.up);

				if (Vector3.Distance (transform.position, mCurrentNode.AftNode.transform.position) < 0.1f) {
					mCurrentNode = mCurrentNode.AftNode;
					
				}
			}

		}
	}

	public void GoStep (MapNode target)
	{
		mTargetNode = target;
		status = CarStatus.Moving;
	}
}

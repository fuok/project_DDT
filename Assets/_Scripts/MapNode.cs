using UnityEngine;
using System.Collections;

public class MapNode : MonoBehaviour
{

	public int mNodeIndex;

	public MapNode BefNode;
	//	[HideInInspector]
	public MapNode AftNode;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawSphere (transform.position, 0.5f);
	}
}

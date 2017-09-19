using UnityEngine;
using System.Collections;

public class MapNode : MonoBehaviour
{

	public int mNodeIndex;

	public MapNode BefNode;
	//	[HideInInspector]
	public MapNode AftNode;

	//建筑
	public Building mBuilding;

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
		Gizmos.color = Color.green;
		Gizmos.DrawSphere (transform.position, 4f);
	}
}

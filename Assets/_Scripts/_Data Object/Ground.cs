using UnityEngine;
using System.Collections;

public class Ground
{
	public int Index{ set; get; }

	public int Owner{ set; get; }

	public int Level{ set; get; }

	public int Price{ set; get; }



	public void SetGround (int owner, int level = 0)
	{
		this.Owner = owner;
		this.Level = level;
	}
}

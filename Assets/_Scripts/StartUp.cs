using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUp : MonoBehaviour
{
	public GameObject mStaticHolder;

	// Use this for initialization
	void Start ()
	{
		GameObject.DontDestroyOnLoad (mStaticHolder);
		SceneManager.LoadScene ("[LoadingScene1]");
	}

}

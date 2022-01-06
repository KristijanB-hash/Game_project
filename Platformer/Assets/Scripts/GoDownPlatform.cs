using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDownPlatform : MonoBehaviour
{
	private PlatformEffector2D effector;

	void Start()
	{
		effector=GetComponent<PlatformEffector2D>();
	}

	void Update()
	{

		if(Input.GetKey(KeyCode.DownArrow))
		{
			effector.rotationalOffset=180f;
		}

		/*if(Input.GetKey(KeyCode.UpArrow))  //drug nACIN DA SE NAPRAVI ISTOTO
		{
			effector.rotationalOffset=0f;
		}*/

		if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            effector.rotationalOffset=0f;
        }
	}
}

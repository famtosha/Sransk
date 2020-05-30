﻿using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.LWRP;

public class LegacyLightDisabling : MonoBehaviour
{


	Light2D testLight;
	public float minWaitTime;
	public float maxWaitTime;

	void Start()
	{
		testLight = GetComponent<Light2D>();
		StartCoroutine(Flashing());
	}

	IEnumerator Flashing()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
			testLight.enabled = !testLight.enabled;

		}
	}
}
﻿using UnityEngine;
using System.Collections;

public class Spawner : Timer {
	[SerializeField]
	private GameObject[] baseObjects;
	[SerializeField]
	private float delay;
	
	public override void Start () {
		timeOut = delay;
		base.Start ();
	}
	
	public override void Callback () {
		Vector2 max = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		CreateEnemy (max.x, max.y, 1f);
	}

	private GameObject CreateEnemy() {
        int index = Random.Range(0, baseObjects.Length);
		return (GameObject)Instantiate (baseObjects[index]);
	}

	private GameObject CreateEnemy(float x, float y, float z) {
		GameObject newObject = CreateEnemy ();
		newObject.transform.position = new Vector3 (x, y, z);
		return newObject;
	}
}

﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		// layer 11 - Bullet
		if (collider.gameObject.layer == 10) {
			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}
}

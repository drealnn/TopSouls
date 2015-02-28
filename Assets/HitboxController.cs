﻿using UnityEngine;
using System.Collections;

public class HitboxController : MonoBehaviour {

	public int damage;
	public int knockbackVel;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("CONTACT");
		DoDamage (other);
	}

	void DoDamage(Collider2D other)
	{
		if(other.CompareTag("Enemy"))
			other.GetComponent<EnemyPlaceholderController>().TakeDamage(damage);
		if(other.CompareTag("Player"))
			other.GetComponent<PlayerController>().TakeDamage(damage);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

﻿using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	
	public int damage;
	public int force;
	public float collisionCooldown;
	public AudioClip hitSound;
	public RaycastHit2D hit;

	private AudioSource source;
	private float lastCollision;

	void OnTriggerEnter2D(Collider2D other)
	{
		hit = Physics2D.Raycast (transform.position, other.transform.position);
		Debug.Log (hit.collider.tag);
		if(other.CompareTag("Shield"))
		{
			if(Time.time-lastCollision >=  collisionCooldown)
			{
				lastCollision = Time.time;
				DoDamage (other);
				Push (other);
			}
		}

		else if(other.CompareTag("Player") || 
			   (other.CompareTag("Enemy") && !this.CompareTag("EnemyWeapon")) ||
			   (other.CompareTag("runner") && !this.CompareTag("EnemyWeapon"))||
			   (other.CompareTag("boss") && !this.CompareTag("EnemyWeapon"))) 
			{
				if(Time.time-lastCollision >=  collisionCooldown)
				{
					lastCollision = Time.time;
					DoDamage (other);
					Push (other);
				}
			}
	}

	void Push(Collider2D other)
	{
		Vector2 dir = (other.transform.position - transform.parent.position).normalized;
	
		//Debug.DrawRay (other.transform.position, dir);
	
		if(other.CompareTag("Shield"))
			other.GetComponentInParent<PlayerController>().GetPushed (dir*force, true);
		else if(other.CompareTag("Enemy"))
			other.GetComponent<EnemyPlaceholderController>().GetPushed (dir*force, false);
		else if (other.CompareTag ("runner"))
			other.GetComponent<runnerController> ().GetPushed (dir*force, false);
		else if (other.CompareTag ("boss"))
			other.GetComponent<bossController> ().GetPushed (dir*force, false);
		else if(other.CompareTag("Player"))
			other.GetComponent<PlayerController>().GetPushed (dir*force, false);
	}
	
	void DoDamage(Collider2D other)
	{
		source.PlayOneShot (hitSound);

		if (other.CompareTag ("Shield"))
			other.GetComponentInParent<PlayerController> ().TakeDamage (damage, true);
		else if (other.CompareTag ("Enemy")) 
			other.GetComponent<EnemyPlaceholderController> ().TakeDamage (damage, false);
		else if (other.CompareTag ("runner"))
			other.GetComponent<runnerController> ().TakeDamage (damage, false);
		else if (other.CompareTag ("boss"))
			other.GetComponent<bossController> ().TakeDamage (damage, false);
		else if(other.CompareTag("Player"))
			other.GetComponent<PlayerController>().TakeDamage(damage, false);
	}

	void Start()
	{
		source = GetComponent<AudioSource> ();
	}
}

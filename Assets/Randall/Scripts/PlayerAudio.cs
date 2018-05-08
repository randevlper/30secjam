using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

	public Character playerCharacter;
	public GunController playerGun;

	public AudioClip hit;
	public AudioClip death;
	public AudioClip shoot;

	// Use this for initialization
	void Awake()
	{
		playerCharacter.onDeath += OnDeath;
		playerCharacter.onHit += OnHit;
		playerGun.onShoot += OnShoot;
	}

	void OnHit(HitData hitData)
	{
		ServiceLocator.instance.audioManager.Play(hit,transform.position);
	}

	void OnDeath()
	{
		ServiceLocator.instance.audioManager.Play(death,transform.position);
	}

	void OnShoot()
	{
		ServiceLocator.instance.audioManager.Play(shoot,transform.position);
	}
}

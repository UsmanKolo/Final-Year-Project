using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WalkJumpFire : MonoBehaviour
{

	Rigidbody2D rb;
	float dirX;

	[SerializeField]
	float moveSpeed = 5f, bulletSpeed = 500f;


	public Transform barrel;
	public Rigidbody2D bullet;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		dirX = CrossPlatformInputManager.GetAxis("Horizontal");

		if (CrossPlatformInputManager.GetButtonDown("Shoot"))
			Fire();
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
	}

	void Fire()
	{
		var firedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
		firedBullet.AddForce(barrel.up * bulletSpeed);
	}
}
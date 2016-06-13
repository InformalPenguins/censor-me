﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private LayerMask whatIsGround;
	[SerializeField]
	private GameObject newBullet;
	[SerializeField]
	private LayerMask[] whatIsEnemy;
	[SerializeField]
	private Collider2D groundDetector;

    private Rigidbody2D myRigidBody;
	private Animator myAnimator;
	private bool canShoot;
	private float timeLeft;
    private InputController inputController;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();
		canShoot = true;
        inputController = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        myRigidBody.velocity = new Vector2(moveSpeed + gameController.GameSpeed(), myRigidBody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
		isGrounded = Physics2D.IsTouchingLayers (groundDetector, whatIsGround);
		myAnimator.SetBool ("isJumping", !isGrounded);

		// Jump
		if ((Input.GetKeyDown(KeyCode.Z) || inputController.IsJumping()) && isGrounded)
		{
			myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
		}

		// Shoot
		if ((Input.GetKeyDown(KeyCode.M) || inputController.IsShooting()) && canShoot) {
			GameObject aBullet = (GameObject)Instantiate (newBullet);
			aBullet.transform.position = new Vector3 (myRigidBody.position.x + 4f, myRigidBody.position.y - 4f, 1f);
			canShoot = false;
		}

        // release sooter
        if (!inputController.IsShooting()) {
            canShoot = true;
        }
    }

	void OnCollisionEnter2D(Collision2D collider) {
		// layer 11 - Enemy
        // layer 12 - Bounds
        // layer 13 - Coin
		if (collider.gameObject.layer == 11 || collider.gameObject.layer == 12) {
			Die ();
		} else if (collider.gameObject.layer == 13) {
            Destroy(collider.gameObject);
        }
    }

	void Die() {
		// basically restart the scene
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public float GetWidth()
	{
		return GetComponent<Renderer>().bounds.size.x;
	}
}

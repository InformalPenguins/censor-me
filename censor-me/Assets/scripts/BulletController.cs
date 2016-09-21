using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	[SerializeField]
	private float speed;
	
	private Transform myTransform;
    private GameController gameController;

    // Use this for initialization
    void Start () {
		myTransform = GetComponent<Transform> ();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

	void FixedUpdate () {
		// movement
		myTransform.Translate (Vector2.right * (speed + gameController.GameSpeed()) * Time.deltaTime);

		// destroy when is out of the screen
		Vector2 max = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		if (myTransform.position.x > max.x) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		// layer 8 - Ground
		if (collider.gameObject.layer == 8) {
			Destroy(gameObject);
		}
	}
}

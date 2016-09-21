using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {
	[SerializeField]
	private GameObject[] basePlatforms;
	[SerializeField]
	private float distanceBetween;
    [SerializeField]
    private bool increaseDistanceBetween;
    [SerializeField]
    private bool randomizeDistanceBetween;
    [SerializeField]
    private GameController gameController;

	private GameObject oldPlatform;
	private float minY, maxY, lastYUsed, initialY = -5f, initialZ = 2f;

	// Use this for initialization
	void Start () {
		minY = initialY - 10f;
		maxY = initialY + 10f;
		lastYUsed = initialY;
		oldPlatform = CreatePlatform (-25f, lastYUsed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        float ocuppiedSpaceBoundary = oldPlatform.transform.position.x +
            oldPlatform.GetComponent<Collider2D> ().bounds.size.x +
            distanceBetween;

        if (increaseDistanceBetween) {
            float upperLimit = gameController.GameSpeed() / 3;
            if (randomizeDistanceBetween) {
                ocuppiedSpaceBoundary += Random.Range(0, upperLimit);
            } else {
                ocuppiedSpaceBoundary += upperLimit;
            }
        }

        Vector2 max = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		if (ocuppiedSpaceBoundary - 10 < max.x) {
			float newY = lastYUsed + Random.Range (-1, 2) * 3;
			if (newY < minY || newY > maxY) {
				newY = lastYUsed;
			}
			oldPlatform = CreatePlatform(ocuppiedSpaceBoundary, newY);
			lastYUsed = newY;
		}
	}

	private GameObject CreatePlatform() {
		int index = Random.Range (0, basePlatforms.Length);
		return (GameObject)Instantiate (basePlatforms[index]);
	}

	private GameObject CreatePlatform(float x, float y) {
		GameObject newPlatform = CreatePlatform();
		newPlatform.transform.position = new Vector3 (x, y, initialZ);
		return newPlatform;
	}

	private GameObject CreatePlatform(float x, float y, int index) {
		GameObject newPlatform = (GameObject)Instantiate (basePlatforms[index]);
		newPlatform.transform.position = new Vector3 (x, y, initialZ);
		return newPlatform;
	}
}

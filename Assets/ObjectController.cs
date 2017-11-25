using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

	private Transform toDrag;
	private float dist;
	private Vector3 offset; 
	private GameObject obj;
	private float fingerDist;

	// Use this for initialization
	void Start () {
		//obj.AddComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.GetTouch(0).position );
			RaycastHit[] hits;
			hits = Physics.RaycastAll (ray);

			for (int i = 0; i < hits.Length; ++i) {
				RaycastHit hit = hits [i];
				if (hit.collider.gameObject.CompareTag ("Picture")) {
					print ("User Tap Object: " + hit.collider.gameObject.name);
					obj = hit.collider.gameObject;
					break;
				}
			}
		}

		if (obj && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero
			// get the touch position from the screen touch to world point
			Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.4f));
			// lerp and set the position of the current object to that of the touch, but smoothly over time.
			//obj.transform.position = Vector3.Lerp(obj.transform.position, touchedPos, 2 * Time.deltaTime);
			//obj.AddComponent<Rigidbody> ();
			Rigidbody rb = obj.GetComponent<Rigidbody> ();
			rb.AddForce ((touchedPos - obj.transform.position) * 5.0f);
		}



		/* Scalaing Gesture */
		if (Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit[] hits;
			hits = Physics.RaycastAll (ray);
			for (int i = 0; i < hits.Length; ++i) {
				RaycastHit hit = hits [i];
				if (hit.collider.gameObject.CompareTag ("Picture")) {
					print ("User Tap Object: " + hit.collider.gameObject.name);
					obj = hit.collider.gameObject;
					break;
				}
			}

			fingerDist = Vector3.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);
		}

		if (obj && Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Moved) 
		{
			float scaleDist = Vector3.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);

			if (scaleDist > fingerDist) {
				float yy = (obj.transform.localScale.y + 0.05f);
				if (yy <= 2)	
					obj.transform.localScale += new Vector3 (0f, 0.05f, 0.05f); 
			} 
			else if (scaleDist < fingerDist) {
				float yy = (obj.transform.localScale.y + 0.05f);
				if (yy >= 0.95f)
					obj.transform.localScale -= new Vector3 (0f, 0.05f, 0.05f);
			}

			fingerDist = scaleDist;
		}

		if (obj && Input.touchCount <= 2 && Input.GetTouch (0).phase == TouchPhase.Ended) {
			//Rigidbody rb = obj.GetComponent<Rigidbody> ();
			//rb.AddForce (Vector3.zero);
			obj = null;
		}
	}
}

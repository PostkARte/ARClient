using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCubeParentController : MonoBehaviour {

	public float unit;

	private Vector3 targetScale;
	private bool isShowingMap;
	private float beginTime;
	private Vector3 deltaDistance;

	// Use this for initialization
	void Start () {
		targetScale = new Vector3 (0.06f, 0.06f, 0.06f);
		deltaDistance = new Vector3 (unit, unit, unit);
		isShowingMap = true;
		beginTime = Time.time;
	}

	// Update is called once per frame
	void FixedUpdate () {
/*		UNITY HAS BUG IN THIS CASE ....
 		WE CANNOT SCALING WHOLE OBJECT FOR BOX PARENT
		if (Time.time - beginTime >= 0.017f) {
			print ("update Time");
			if (isShowingMap && transform.localScale.x < targetScale.x)
				transform.localScale += deltaDistance;
			else if (!isShowingMap) {
				if (transform.localScale.x > targetScale.x)
					transform.localScale -= deltaDistance;
				transform.position = Vector3.Lerp (transform.position, new Vector3 (20, 20, 20), Time.deltaTime);
			}
			
			beginTime = Time.time;
		}
*/
	}

	public void ToggleToShowMap() {
		if (!isShowingMap)
			targetScale = new Vector3 (0.06f, 0.06f, 0.06f);
		else
			targetScale = new Vector3 (0.01f, 0.01f, 0.01f);

		isShowingMap = !isShowingMap;
	}
}

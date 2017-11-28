using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour {

	private float beginTime;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 3.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

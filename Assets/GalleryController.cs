using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GalleryController : MonoBehaviour {

//	public Transform camera;
	public Transform center;
	// Use this for initialization
//	private Renderer renderer;
	private Rigidbody rb;

	void Start() {
		/*
		gameObject.AddComponent<Rigidbody> ();
		Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotationX;
		rb.AddExplosionForce (100, transform.position, 30);
		*/
		//WWW www = new WWW ("http://1.34.198.96:3000/zelda_trim.mp4");
		//yield return www;
		gameObject.AddComponent<VideoPlayer> ();
		VideoPlayer player = gameObject.GetComponent<VideoPlayer> ();
		player.url = "http://1.34.198.96:3000/zelda_trim.mp4";
		player.playOnAwake = true;
		player.isLooping = true;
		player.source = VideoSource.Url;

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void explosion() {
		float stride = 5000f;
		float xx = Random.Range (-stride, stride);
		float zz = Random.Range (-stride, stride);
		gameObject.AddComponent<Rigidbody> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		rb.mass = 1000f;
		rb.AddForce (new Vector3 (xx, stride, zz));
	}
}

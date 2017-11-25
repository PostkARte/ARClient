using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GalleryController : MonoBehaviour {

	public Transform camera;
	public Transform center;
	// Use this for initialization
	private Renderer renderer;

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
		gameObject.AddComponent<Rigidbody> ();
		Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

	// Update is called once per frame
	void Update () {
		//gameObject.transform.RotateAround (center.position, Vector3.up, 10* Time.deltaTime);
		//gameObject.transform.LookAt(camera);
	}
}

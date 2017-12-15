using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMapService : MonoBehaviour {

    private float lat;
    private float lon;

    private int zoom = 13;

    private string mapSelected = "roadmap";
    private int scale = 2;

    // Use this for initialization
    public void LoadMap (float lat, float lon) {
        this.lat = lat;
        this.lon = lon;
        StartCoroutine(Map());
    }
	

    private IEnumerator Map()
    {
        Renderer renderer = GetComponent<Renderer>();

        float x = (int)(renderer.bounds.size.x * 100);
        float z = (int)(renderer.bounds.size.z * 100);

        Debug.Log(x);
        Debug.Log(z);

        string url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
            "&zoom=" + zoom + "&size=" + x + "x" + z + "&scale=" + scale
            + "&maptype=" + mapSelected +
            "&key=AIzaSyDjWFpSKVXK2tXoeafA3YI1gsW9u-vV9X8";
        WWW www = new WWW(url);
        yield return www;

        Debug.Log(www.texture);
        renderer.material.mainTexture = www.texture;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}

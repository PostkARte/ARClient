using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour {

    public GalleryController GalleryController { private get; set; }

    public void OnHit()
    {
        //Show picture
        GalleryController.explosion(gameObject.transform.position);
    }

}

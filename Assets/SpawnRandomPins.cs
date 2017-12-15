using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnRandomPins : MonoBehaviour
{
    private const float OFFSET = 0.6f;
    private const float THRESHOLD = 0.9f;

    public GameObject spawnObj;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    
    public void InitMarkers(List<GalleryController> controllers)
    {
        Renderer renderer = this.GetComponent<Renderer>();

        float x = (renderer.bounds.size.x / 2) - 1f;
        float z = (renderer.bounds.size.z / 2) - 1f;

        //Init as many markes as gallery controllers are generated
        foreach (var controller in controllers)
        {
            GenerateMarker(x, z, controller);
        }
    }

    private void GenerateMarker(float boundX, float boundZ, GalleryController controller)
    {
        //Generate new position
        Vector3 newPosition;

        do
        {
            newPosition = new Vector3(Random.Range(boundX * -1, boundX), OFFSET, Random.Range(boundZ * -1, boundZ)) + this.transform.position;
        } while (spawnedObjects.Any(s => DistanceUnderThreshold(s.transform.position, newPosition)));

        //Random generated position is ok (not overlapping with other markers!)
        //Spawn marker on random spot on plane
        GameObject copy = Instantiate(spawnObj, this.transform.parent.transform);
        copy.transform.position = newPosition;

        spawnedObjects.Add(copy);
        copy.GetComponent<MarkerController>().GalleryController = controller;
    }

    private bool DistanceUnderThreshold(Vector3 spawnedObjPos, Vector3 newPos)
    {
        return Vector3.Distance(spawnedObjPos, newPos) < THRESHOLD;
    }
}

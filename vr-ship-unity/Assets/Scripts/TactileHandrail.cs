using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TactileHandrail : MonoBehaviour
{
    
    public Material idleMaterial;
    public Material activeMaterial;
    public GameObject nextHandRail;
    public GameObject prevHandRail;

    public float blinkPeriod = 0.1f;

    public bool currentTiltDirection = true;
    public bool emergency = true;

    private bool isBlinking = false;
    private List<GameObject> markers = new List<GameObject>();

    void Start()
    {
        Transform markers_parent_transform = gameObject.transform.Find("Markers");
        foreach(Transform marker in markers_parent_transform)
            markers.Add(marker.gameObject);
        
        if (!currentTiltDirection)
            markers.Reverse();
    }

    // Update is called once per frame
    void Update()
    {
        if (emergency)
        {
            if (!isBlinking)
                StartCoroutine(BlinkMarkers());
            TiltMarkers(currentTiltDirection);
        }
    }

    void TiltMarkers(bool tiltDirection)
    {   
        Quaternion rotation = tiltDirection ? Quaternion.Euler(-20f, 0f, 0f) : Quaternion.Euler(20f, 0f, 0f);

        foreach(GameObject marker in markers)
            marker.transform.Find("MovingPart").localRotation = rotation;
    }

     IEnumerator BlinkMarkers()
    {
        isBlinking = true;
        for(int i = 0; i < markers.Count; i++)
        {   
            GameObject mp1 = markers[i % markers.Count].transform.Find("MovingPart").gameObject;
            GameObject mp2 = markers[(i + 1) % markers.Count].transform.Find("MovingPart").gameObject;
            GameObject mp3 = markers[(i + 2) % markers.Count].transform.Find("MovingPart").gameObject;

            mp1.GetComponent<MeshRenderer>().material = activeMaterial;
            mp2.GetComponent<MeshRenderer>().material = activeMaterial;
            mp3.GetComponent<MeshRenderer>().material = activeMaterial;
            yield return new WaitForSeconds(blinkPeriod);

            mp1.GetComponent<MeshRenderer>().material = idleMaterial;
            mp2.GetComponent<MeshRenderer>().material = idleMaterial;
            mp3.GetComponent<MeshRenderer>().material = idleMaterial;
            yield return new WaitForSeconds(blinkPeriod / 2f);
        }
        isBlinking = false;
    }
}

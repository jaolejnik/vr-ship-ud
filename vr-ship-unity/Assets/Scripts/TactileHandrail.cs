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
        foreach(GameObject marker in markers)
        {
            GameObject mp = marker.transform.Find("MovingPart").gameObject;

            mp.GetComponent<MeshRenderer>().material = activeMaterial;
            // yield return new WaitForSeconds(2 / blinkingSpeed);
            yield return new WaitForSeconds(blinkPeriod);

            mp.GetComponent<MeshRenderer>().material = idleMaterial;
            // yield return new WaitForSeconds(1 / blinkingSpeed);
            yield return new WaitForSeconds(blinkPeriod / 2f);
        }
        isBlinking = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TactileHandrail : MonoBehaviour
{
    public Material idleMaterial;
    public Material activeMaterial;
    private float blinkPeriod = 0.15f;
    public bool reversedTiltBlink = false;

    private ControlPanel controlPanel;
    private Coroutine runningCoroutine;
    private List<GameObject> markers = new List<GameObject>();
    private bool emergencyMode = false;
    private bool tiltBlinkDirection = true;
    private bool isBlinking = false;

    void Start()
    {   
        GameObject cp = GameObject.Find("ControlPanel");
        controlPanel = cp.GetComponent<ControlPanel>();

        Transform markers_parent_transform = gameObject.transform.Find("Markers");
        foreach(Transform marker in markers_parent_transform)
            markers.Add(marker.gameObject);
        
        if (reversedTiltBlink)
            ReverseTiltBlink();
    }

    void Update()
    {
        if (controlPanel.emergency != emergencyMode)
        {
            emergencyMode = controlPanel.emergency;
            StartCoroutine(ChangingBlinkMarkers());
            TiltMarkers();
        }

        if (!reversedTiltBlink && tiltBlinkDirection != controlPanel.evacuationDirection)
        {
            ReverseTiltBlink();
            if (isBlinking)
                StopCoroutine(runningCoroutine);
            StartCoroutine(ChangingBlinkMarkers());
            TiltMarkers();
        }
        else if (reversedTiltBlink && tiltBlinkDirection == controlPanel.evacuationDirection)
        {
            ReverseTiltBlink();
            if (isBlinking)
                StopCoroutine(runningCoroutine);
            StartCoroutine(ChangingBlinkMarkers());
            TiltMarkers();
        }

        if(emergencyMode && !isBlinking)
            runningCoroutine = StartCoroutine(RunningMarkers());

    }

    void TiltMarkers()
    {   
        float startTime = Time.time;
        float tilt = 0f;
        if (emergencyMode)
            tilt = tiltBlinkDirection ? -20f : 20f;

        Quaternion rotation = Quaternion.Euler(tilt, 0f, 0f);

        foreach(GameObject marker in markers)
            marker.transform.Find("MovingPart").localRotation = rotation;
    }

    void ReverseTiltBlink()
    {
        tiltBlinkDirection = !tiltBlinkDirection;
        markers.Reverse();
    }

    IEnumerator RunningMarkers()
    {
        int active_markers = 6; 

        isBlinking = true;
        for (int i = 0; i < markers.Count; i++)
        {   
            for (int j = 0; j < active_markers; j++)
            {
                GameObject mp = markers[(i + j) % markers.Count].transform.Find("MovingPart").gameObject;
                mp.GetComponent<MeshRenderer>().material = activeMaterial;
            }            
            yield return new WaitForSeconds(blinkPeriod);

            for (int j = 0; j < active_markers; j++)
            {
                GameObject mp = markers[(i + j) % markers.Count].transform.Find("MovingPart").gameObject;
                mp.GetComponent<MeshRenderer>().material = idleMaterial;
            }            
        }
        isBlinking = false;
    }

    IEnumerator ChangingBlinkMarkers()
    {
        isBlinking = true;
        for (int i = 0; i < 5; i ++)
        {
            foreach (GameObject marker in markers)
            {
                GameObject mp = marker.transform.Find("MovingPart").gameObject;
                mp.GetComponent<MeshRenderer>().material = activeMaterial;
            }
            yield return new WaitForSeconds(blinkPeriod * 5f);

            foreach(GameObject marker in markers)
            {
                GameObject mp = marker.transform.Find("MovingPart").gameObject;
                mp.GetComponent<MeshRenderer>().material = idleMaterial;
            }
            yield return new WaitForSeconds(blinkPeriod);
        }
        isBlinking = false;
    }
}

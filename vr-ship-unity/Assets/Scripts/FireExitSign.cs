using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExitSign : MonoBehaviour
{
    private ControlPanel controlPanel;
    private bool emergencyMode = false;
    private bool direction = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cp = GameObject.Find("ControlPanel");
        controlPanel = cp.GetComponent<ControlPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != controlPanel.evacuationDirection)
        {
            gameObject.transform.Rotate(0f, 0f, 180f);
            direction = !direction;
        }
    }
}

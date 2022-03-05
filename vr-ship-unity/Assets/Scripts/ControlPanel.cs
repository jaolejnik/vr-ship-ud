using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public bool emergency = false;
    public bool evacuationDirection = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("joystick button 1"))
            emergency = !emergency;
        
        if(Input.GetKeyDown("joystick button 2"))
            evacuationDirection = !evacuationDirection;
    }
}

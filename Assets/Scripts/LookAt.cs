using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform Target;
	
    void Update()
    {
        if (Target == null)
        {
            return;
        }

        transform.LookAt(Target);
    }
}

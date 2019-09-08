using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy1Sec : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.0f);
    }

}

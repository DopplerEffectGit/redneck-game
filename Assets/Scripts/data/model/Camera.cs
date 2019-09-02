using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject palyer;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
        offset = transform.position - palyer.transform.position;

    }

    void Update()
    {
        Debug.Log("camera update");

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = palyer.transform.position + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSaw : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 5);
        transform.Translate(Vector3.up * Time.deltaTime * 3, Space.World);

    }

    private void OnBecameInvisible()
    {
      
        Destroy(gameObject);
    }

    
}

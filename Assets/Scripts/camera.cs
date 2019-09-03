using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Player;
    public float damping;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v2 = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, v2, damping * Time.deltaTime);
    }
}

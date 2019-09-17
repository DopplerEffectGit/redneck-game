using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSaw : MonoBehaviour
{
    [SerializeField] GameObject Saw;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] float SpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(SpawnTime);
            Instantiate(Saw, SpawnPoint);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool attackClicked;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(attackClicked) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        attackClicked = false;
    }

    public void attackClick() {
        attackClicked = true;

    }
}

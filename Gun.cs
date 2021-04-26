using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public float projectileForce = 300f;
    public Transform Target;

    private GameObject instProjectile;
    private Rigidbody2D instRb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            instProjectile = Instantiate(projectile, this.transform.position, this.transform.rotation);
            instRb = instProjectile.GetComponent<Rigidbody2D>();
            instRb.AddForce(transform.forward * projectileForce);
        }

        Target.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Quaternion rotation = Quaternion.LookRotation(Target.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    private float health;

    public GameObject bosshealthcheck;
    public GameObject end;
    public GameObject projectile;
    public float projectileForce = 300f;
    public Transform Target;

    private float countdown = 1;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private Vector3 mousePos;
    private GameObject projectileInst;

    private void Start()
    {
        health = 400;
    }

    // Update is called once per frame
    void Update()
    {
        if (bosshealthcheck.activeSelf == true)
        {
            health = 400;
            bosshealthcheck.SetActive(false);
        }

        if (health <= 0)
        {
            end.SetActive(true);
        }

        _direction = (mousePos - this.transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);

        mousePos = Target.transform.position;

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            projectileInst = Instantiate(projectile, this.transform.position, _lookRotation);
            projectileInst.GetComponent<Rigidbody2D>().AddForce(projectileInst.transform.forward * projectileForce);
            countdown = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile2")
        {
            health -= 5;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Projectile")
        {
            health -= 5;
            Destroy(collision.gameObject);
        }
    }
}

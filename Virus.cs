using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public GameObject Target;
    public float health = 3;
    public GameObject projectile;
    public float projectileForce = 800f;
    public bool canShoot = true;
    public GameObject heart;

    private float countdown = 1;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private Vector3 mousePos;
    private GameObject projectileInst;

    // Update is called once per frame
    void Update()
    {
        _direction = (mousePos - this.transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);

        mousePos = Target.transform.position;

        countdown -= Time.deltaTime;

        if (countdown <= 0 && canShoot == true)
        {
            projectileInst = Instantiate(projectile, this.transform.position, _lookRotation);
            projectileInst.GetComponent<Rigidbody2D>().AddForce(projectileInst.transform.forward * projectileForce);
            countdown = 1;
        }

        Quaternion rotation = Quaternion.LookRotation(Target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        if (health <= 0)
        {
            if (heart != null)
                heart.SetActive(true);

            Viruses.virusesKilled += 1;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            health -= 1;
        }

        if (collision.gameObject.tag == "Projectile2")
        {
            Destroy(collision.gameObject);
            health -= 3;
        }
    }
}

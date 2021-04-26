using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disguise : MonoBehaviour
{
    public GameObject virus;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            this.gameObject.SetActive(false);
            virus.SetActive(true);
        }

        if (collision.gameObject.tag == "Projectile2")
        {
            this.gameObject.SetActive(false);
            virus.SetActive(true);
        }
    }
}

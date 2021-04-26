using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;
    public GameObject projectile;
    public GameObject projectile2;
    public float projectileForce = 800f;
    public Transform Target;
    public Text healthText;
    public Animator anim;
    public GameObject nope;
    public GameObject dont;
    public GameObject bossDoor;
    public GameObject boss;
    public GameObject bosshealthcheck;

    private Vector3 mousePos;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private GameObject projectileInst;
    private Rigidbody2D rb;
    private float health = 50;
    private GameObject currentProjectile;
    private Vector3 checkpoint;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private void Start()
    {
        currentProjectile = projectile;
        rb = this.GetComponent<Rigidbody2D>();
        checkpoint = new Vector3(0, -1.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + health;

        _direction = (mousePos - this.transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetMouseButtonDown(0))
        {            
            projectileInst = Instantiate(currentProjectile, this.transform.position, _lookRotation);
            projectileInst.GetComponent<Rigidbody2D>().AddForce(projectileInst.transform.forward * projectileForce);
        }

        if (health <= 0)
        {
            GoBackToStart();
        }

        if (health > 50)
        {
            health = 50;
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Top")
        {
            this.transform.position = new Vector3(this.transform.position.x, 12, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (collision.gameObject.tag == "BadProjectile")
        { 
            Destroy(collision.gameObject);
            anim.Play("Hurt");
            health -= 1;
        }

        if (collision.gameObject.tag == "BossProjectile")
        {
            Destroy(collision.gameObject);
            anim.Play("Hurt");
            health -= 5;
        }

        if (collision.gameObject.tag == "Goal")
        {
            if (Viruses.virusesKilled >= 9)
            {
                this.transform.position = new Vector3(150, 0, 0);
                checkpoint = new Vector3(150, 0, 0);
            }

            else
            {
                nope.SetActive(true);
                dont.SetActive(true);
            }
        }

        if (collision.gameObject.tag == "Upgrade")
        {
            Destroy(collision.gameObject);
            currentProjectile = projectile2;
            projectileForce = 1100;
        }

        if (collision.gameObject.tag == "Health")
        {
            Destroy(collision.gameObject);
            health += 3;
        }

        if (collision.gameObject.tag == "BossTrigger")
        {
            bossDoor.SetActive(true);
            Camera.main.orthographicSize = 20;
            this.transform.position = new Vector3(305, -1.5f, 0);
            boss.SetActive(true);
            checkpoint = new Vector3(296, -1.5f, 0);
            bosshealthcheck.SetActive(true);
        }
    }

    public void GoBackToStart()
    {
        this.transform.position = checkpoint;
        health = 50;
        bossDoor.SetActive(false);
        Camera.main.orthographicSize = 5;
        boss.SetActive(false);
        bosshealthcheck.SetActive(false);
    }
}

public static class Viruses
{
    public static float virusesKilled;
}

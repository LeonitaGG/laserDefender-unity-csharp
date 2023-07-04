using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    public int GetHealth() { return health; }

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously()); //start the coroutine when the space button is pressed/held down
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine); //Stop this specific Coroutine once the space button has been lifted
        }
    }

    IEnumerator FireContinuously() //function for laser fire inside a Coroutine.
    {
        while(true)
        {
            //create an instance of the playerlaser prefab, as a GameObject
            GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;//"Quarternion.identity" : Quarternion.identity just returns the original rotation of the object
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            Destroy(laser, 1.5f);
            yield return new WaitForSeconds(projectileFiringPeriod); //creates a new instance of laser after the projectileFiringPeriod = 0.1f
        }
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; //see: Edit > Project Settings > Input > Horizontal
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp((transform.position.x + deltaX), xMin, xMax);
        var newYPos = Mathf.Clamp((transform.position.y + deltaY), yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit(); //destroy the laser after it collides and reduces health

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}

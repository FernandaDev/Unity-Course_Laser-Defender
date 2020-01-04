using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileFireRate = 1f;

    Camera cam;
    float xMin, xMax;
    float yMin, yMax;
    float projectileSpeed = 20f;
    float projectileTimer = 0;

    void Start()
    {
        SetupMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time >= projectileTimer)
            {
                projectileTimer = Time.time + 1f/ projectileFireRate;

                GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            }
        }
    }

    private void SetupMoveBoundaries()
    {
        cam = Camera.main;

        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
}

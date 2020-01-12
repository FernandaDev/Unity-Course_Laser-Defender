using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float maxHealth = 500f;
    float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
        private set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChange.Invoke();

        }
    }
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume;

    [Header("Projectile")]

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFireRate = 1f;

    Camera cam;
    float xMin, xMax;
    float yMin, yMax;
    float projectileTimer = 0;

    public static UnityEvent OnHealthChange = new UnityEvent();

    void Start()
    {
        currentHealth = maxHealth;
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
                projectileTimer = Time.time + 1f / projectileFireRate;

                GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        CurrentHealth -= damageDealer.GetDamage();
        damageDealer.OnHit();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject, 0.1f);
        FindObjectOfType<LevelManager>().LoadGameOver();
    }
}

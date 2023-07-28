using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    public float Speed = 0;
    public float GeneratedSpeed = 0;

    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _speedBoost = 0f;

    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private ParticleSystem _trailParticles;
    [SerializeField] private Light2D _light;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        AddColor();         // Color
        AddMovement();      // Movement
    }

    #region Movement
    public void SetSpeedBoost(float speed)
    {
        _speedBoost = speed;
    }

    // TODO: There might be more enemies to work with so make this method accesible to child classes
    public void Destroy()
    {
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        _rigidBody.velocity = new Vector2(0, 0);    // Stop movement
        _spriteRenderer.enabled = false;            // Disable sprite
        _light.enabled = false;
        _explosionParticles.Play();                 // Play explosion

        yield return new WaitForSeconds(_explosionParticles.main.duration);     // Wait for explosion to finish

        GameObject.Destroy(gameObject);
    }

    private void AddMovement()
    {
        GeneratedSpeed = RandomSpeed();                         // Generate speed
        float speed = GeneratedSpeed + _speedBoost;             // Generate speed
        Speed = speed;
        Vector2 movement = new Vector2(0, -speed);              // Set movement
        _rigidBody.AddForce(movement, ForceMode2D.Impulse);     // Add force with generated movement
    }

    private float RandomSpeed()
    {
        return Random.Range(_minSpeed, _maxSpeed);
    }
    #endregion

    #region Color
    public bool CompareColor(Color compareTo)
    {
        return _spriteRenderer.color == compareTo;
    }

    private void AddColor()
    {
        int R = 0;
        int G = 0;
        int B = 0;

        // Randomize color
        while (R == 0 && G == 0 && B == 0)
        {
            R = RandomIntValue(0, 1);
            G = RandomIntValue(0, 1);
            B = RandomIntValue(0, 1);
        }

        Color color = new Color(R, G, B);

        _spriteRenderer.color = color;              // Change color of the sprite
        _light.color = color;                       // Change color of the light
        var main = _explosionParticles.main;
        main.startColor = color;                    // Change color of the explosin
        main = _trailParticles.main;
        main.startColor = color;                    // Change color of the trail
    }

    private int RandomIntValue(int min, int max)
    {
        return Random.Range(min, max + 1);      // max is exclusive so I need to add +1
    }
    #endregion
}


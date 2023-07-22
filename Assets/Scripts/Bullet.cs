using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 15f;
    private Rigidbody2D _rigidbody2D;
    private PlayerMovement player;
    private float totalSpeed;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        totalSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        _rigidbody2D.velocity = new Vector2(totalSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
           Destroy(col.gameObject); 
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}

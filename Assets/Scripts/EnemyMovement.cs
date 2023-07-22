using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody2D.velocity = new Vector2(moveSpeed,0);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed *= -1;
        transform.localScale = new Vector3(transform.localScale.x*-1,1,1);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 1.0f;
    private Rigidbody2D rb;
    private Vector2 shootDirection;
    public int damage = 45;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        ReclarecalculateTrajectory();
    }

    /// <summary>
    /// Rotate in z if the projectile is falling
    /// </summary>
    public void ReclarecalculateTrajectory()
    {
        if (rb.velocity.y <= 0.0f && shootDirection.normalized.x > 0.0f)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, -1.0f));
        }
        else if (rb.velocity.y <= 0.0f && shootDirection.normalized.x < 0.0f)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f));

        }
    }


    /// <summary>
    /// Set the direction to shoot
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(Vector2 direction)
    {
        this.shootDirection = direction;
    }

    /// <summary>
    /// Activate a initial velocity 
    /// </summary>
    public void ShootYourSelf()
    {
        transform.right = shootDirection;
        rb.AddForce(shootDirection * projectileSpeed,ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == transform.parent.tag) return;

        Attackable target = collision.gameObject.GetComponent<Attackable>();

        if (target != null)
        {
            target.OnAttack(damage);
        }
        Destroy(gameObject);
    }


}

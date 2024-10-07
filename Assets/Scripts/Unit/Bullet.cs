using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    private int damage = 0;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed = 10;
    [SerializeField] private SpriteRenderer bulletImage;

    private void OnEnable()
    {
        Invoke(nameof(DestroyBullet), 2f);
    }
    /// <summary>
    /// check bullet collision control
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent(out IAttackable attackable))
        {
            attackable.TakeDamage(damage);
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        PoolManager.Instance.AddObjectInPool(PoolObjectType.Bullet, gameObject);
    }
    /// <summary>
    /// Firing of the weapon
    /// </summary>
    /// <param name="transform1"> position of the bullet</param>
    /// <param name="i">Bullet damage</param>
    public void SetDamage(Transform transform1, int i)
    {
        damage = i;
        gameObject.SetActive(true);
        bulletImage.color = Random.ColorHSV();
        _rigidbody2D.velocity = transform1.right * speed;
    }
}
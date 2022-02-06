using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_controller : MonoBehaviour
{
    private Player_data player_data;
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float move_speed, damage, attack_cooldown;

    public void SetData(Transform _target, float _move_speed, float _attack_cooldown, float shell_speed, float _damage)
    {
        rb = GetComponent<Rigidbody2D>();
        player_data = _target.gameObject.GetComponent<Player_data>();

        target = _target;
        move_speed = _move_speed;
        damage = _damage;
        attack_cooldown = _attack_cooldown;
        
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            direction = target.position - transform.position;
            direction.Normalize();
            float cooldown = attack_cooldown + Random.Range(-0.3f, 0.3f);

            rb.AddForce(direction * move_speed);
            yield return new WaitForSeconds(cooldown);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player_data.ApplyDamage(damage);
        }
    }
}

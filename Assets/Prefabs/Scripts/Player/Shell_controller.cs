using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shell_controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement, direction, speed;
    private bool shell_enable;
    private float lifetime, damage;

    public void ShellInitialization(Vector2 direction_, float damage_, float speed_, float lifetime_)
    {
        direction = direction_;
        speed = new Vector2(speed_, speed_);
        lifetime = lifetime_;
        damage = damage_;

        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(SetLifetime(lifetime));
    }

    void FixedUpdate()
    {
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    IEnumerator SetLifetime(float duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(DesroyAnimation(0.2f));
    }

    IEnumerator DesroyAnimation(float delay)
    {
        speed = new Vector2(speed.x / 3, speed.y / 3);
        animator.SetTrigger("Trigger_destroy");
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!shell_enable)
        {
            shell_enable = true;
            StartCoroutine(DesroyAnimation(0.2f));
            if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Destroying_object"))
            {
                collider.gameObject.GetComponent<IDamageable>().ApplyDamage(damage);
            }
        }
    }
}

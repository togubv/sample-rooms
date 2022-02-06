using System.Collections;
using UnityEngine;

public class Boss_controller : MonoBehaviour
{
    [SerializeField] private GameObject prefab_enemy_shell;
    private Player_data player_data;
    private Transform target;
    private Rigidbody2D rb;

    private Vector2 direction;
    private float move_speed, shell_speed,damage;

    public void SetController(Transform target_, float move_speed_, float shell_speed_, float damage_)
    {
        rb = GetComponent<Rigidbody2D>();
        player_data = target_.gameObject.GetComponent<Player_data>();

        target = target_;
        move_speed = move_speed_;
        shell_speed = shell_speed_;
        damage = damage_;

        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            Move();
            yield return new WaitForSeconds(1.5f);
        }
        StartCoroutine(ShootCooldown());
    }

    private IEnumerator ShootCooldown()
    {
        StartCoroutine(Shooting());
        yield return new WaitForSeconds(5.5f);
        StartCoroutine(Moving());
    }

    private IEnumerator Shooting()
    {
        for (int i = 0; i < 24; i++)
        {
            InstantiateShellArrow(transform.up);
            InstantiateShellArrow(-transform.up);
            InstantiateShellArrow(transform.right);
            InstantiateShellArrow(-transform.right);
            transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + 30.0f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Move()
    {
        direction = target.position - transform.position;
        direction.Normalize();
        rb.AddForce(direction * move_speed);
    }

    private void InstantiateShellArrow(Vector2 direction_)
    {
        GameObject shell = Instantiate(prefab_enemy_shell, transform.position, Quaternion.identity);
        shell.GetComponent<Enemy_shell_controller>().ShellInitialization(direction_, shell_speed, 4.0f, damage);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player_data.ApplyDamage(damage);
        }
    }
}

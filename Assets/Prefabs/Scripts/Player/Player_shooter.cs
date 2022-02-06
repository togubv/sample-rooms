using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shooter : MonoBehaviour
{
    [SerializeField] private Player_data player_data;

    [SerializeField] private GameObject prefab_shell;
    private bool player_shoot_cooldown = true;
    public List<GameObject> shell_list;

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && player_shoot_cooldown)
        {
            Shoot(new Vector2(0, 1), new Vector2(this.transform.position.x + RandomScatter(), this.transform.position.y + 0.2f), 1.5f);
        }
        if (Input.GetKey(KeyCode.DownArrow) && player_shoot_cooldown)
        {
            Shoot(new Vector2(0, -1), new Vector2(this.transform.position.x + RandomScatter(), this.transform.position.y), 1.5f);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && player_shoot_cooldown)
        {
            Shoot(new Vector2(-1, 0), new Vector2(this.transform.position.x, this.transform.position.y + RandomScatter()), 1.5f);
        }
        if (Input.GetKey(KeyCode.RightArrow) && player_shoot_cooldown)
        {
            Shoot(new Vector2(1, 0), new Vector2(this.transform.position.x, this.transform.position.y + RandomScatter()), 1.5f);
        }
    }

    private void OnEnable()
    {
        player_shoot_cooldown = true;
    }

    float RandomScatter()
    {
        float scatter = Random.Range(-0.1f, 0.1f);
        return scatter;
    }

    private IEnumerator PlayerShootCooldown()
    {
        player_shoot_cooldown = false;
        yield return new WaitForSeconds(player_data.attack_speed);
        player_shoot_cooldown = true;
    }

    private void Shoot(Vector2 direction_, Vector2 offset_, float lifetime_)
    {
        Vector2 first_vector = direction_;
        Vector2 second_vector = direction_;

        if (player_data.shoot_type == ShootType.Default)
        {
            InstantiateShellArrow(direction_, offset_, lifetime_);
        }
        else if (player_data.shoot_type == ShootType.Mirror)
        {
            InstantiateShellArrow(direction_, offset_, lifetime_);
            InstantiateShellArrow(-direction_, offset_, lifetime_);
        }
        else if (player_data.shoot_type == ShootType.Triple)
        {
            SetVectorsTriple(direction_);
            InstantiateShellArrow(direction_, offset_, lifetime_);
            InstantiateShellArrow(first_vector, offset_, lifetime_);
            InstantiateShellArrow(second_vector, offset_, lifetime_);
        }

        void SetVectorsTriple(Vector2 vector)
        {
            if (vector.x == 0 && vector.y == 1)
            {
                first_vector = new Vector2(-0.5f, 1);
                second_vector = new Vector2(0.5f, 1);
            }
            else if (vector.x == 0 && vector.y == -1)
            {
                first_vector = new Vector2(-0.5f, -1);
                second_vector = new Vector2(0.5f, -1);
            }
            else if (vector.x == -1 && vector.y == 0)
            {
                first_vector = new Vector2(-1, 0.5f);
                second_vector = new Vector2(-1, -0.5f);
            }
            else if (vector.x == 1 && vector.y == 0)
            {
                first_vector = new Vector2(1, 0.5f);
                second_vector = new Vector2(1, -0.5f);
            }
        }
    }

    private void InstantiateShellArrow(Vector2 direction_, Vector2 offset_, float lifetime_)
    {
        StartCoroutine(PlayerShootCooldown());
        GameObject shell = Instantiate(prefab_shell, offset_, Quaternion.identity);
        shell.GetComponent<Shell_controller>().ShellInitialization(direction_, player_data.damage, player_data.shell_speed, lifetime_);
        shell_list.Add(shell);
    }
}

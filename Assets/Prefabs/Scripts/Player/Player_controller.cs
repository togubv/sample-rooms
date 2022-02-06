using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_controller : MonoBehaviour
{
    [SerializeField] private Player_data player_data;

    public Vector2 move;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");    

        if (move.x < 0) this.transform.eulerAngles = new Vector2(0, 0);
        if (move.x > 0) this.transform.eulerAngles = new Vector2(0, 180);

        if (move.y != 0 || move.x != 0) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * player_data.move_speed * Time.fixedDeltaTime);
    }

    public Vector3 Offset;
    public float Velocity;
    public float MinDistance;
    [SerializeField] private Transform transform_camera;

    private void LateUpdate()
    {
        Vector3 target_pos = this.transform.position + Offset;

        if (Vector3.Distance(transform_camera.position, target_pos) < MinDistance)
        {
            return;
        }

        Vector3 newPos = Vector3.Lerp(transform_camera.position, target_pos, Velocity * Time.fixedDeltaTime);
        transform_camera.Translate(transform_camera.InverseTransformPoint(newPos));
    }

}

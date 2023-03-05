using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;

    [SerializeField]
    private float moveSpeed;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();        
        body.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
        handleAnimation(input);        
    }

    private void handleAnimation(Vector2 input)
    {        
        sprite.flipX = (input.x < 0.0f);
        animator.SetBool("walking", input.x != 0.0f || input.y != 0.0f);
    }
}

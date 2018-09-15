
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rgb2d;

    Animator animator;

    float rayDistance;

    int angles;

    int direction;

    float animationTime;

    float timeElapsed;

    [SerializeField]
    Transform groundDetection;

    [SerializeField]
    string type;

    void Start()
    {
        timeElapsed = 0;
        animationTime = 1;
        direction = 1;
        angles = 0;
        rayDistance = 0.1f;
        rgb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (IsHitAnimationPLaying())
            timeElapsed += Time.deltaTime;
        
        HandleMovement();
        if (!IsOnGround() || IsWallColliding())
        {
            ChangeDirection();
        }
        if (timeElapsed >= animationTime) {
            StopHitAnimation();
        }
    }

    void HandleMovement()
    {
        rgb2d.velocity = direction > 0 ? Vector2.right : Vector2.left;
    }

    bool IsOnGround()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance, 1 << LayerMask.NameToLayer("Default"));
        return groundInfo.collider == true;
    }

    bool IsWallColliding()
    {
        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, rayDistance, 1 << LayerMask.NameToLayer("Default"));
        return wallInfo.collider == true;
    }

    void ChangeDirection()
    {
        angles = angles < 0 ? 0 : -180;
        transform.eulerAngles = new Vector3(0, angles, 0);
        direction *= -1;
        HandleMovement();
    }

    public void PlayHitAnimation () {
        timeElapsed = 0;
        animator.SetBool("hit", true);
    }

    bool IsHitAnimationPLaying(){
        return animator.GetBool("hit");
    }

    void StopHitAnimation () {
        animator.SetBool("hit", false);
    }
}

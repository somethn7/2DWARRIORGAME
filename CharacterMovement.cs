using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;
    public bool facingRight;
    public float jumpForce;
    public bool canDoubleJump;
    public float attackCooldown = 1f;
    
    public bool isGrounded;
    private bool isAttacking= false;
    private bool isrun;
    private bool isjump;
    public bool canmove = true;

    [SerializeField] private AudioManage audioManager;
    [SerializeField] private string attackSound;
    [SerializeField] private string runAttackSound;
    [SerializeField] private string jumpSound;
    [SerializeField] private string runSound;
    [SerializeField] private string landingsound;

    [SerializeField] private Combat combat;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator _anim;

    [SerializeField] private Camera camera;
    [SerializeField] private Background background;
    [SerializeField] private CharacterHealth _characterHealth;
    
    

    private void Start()
    {
        moveSpeed = 15f;
        
    }

    private void Update()
    {
        Movement();
        CharacterAnimation();
        CharacterRunAttack();
        CharacterAttack();
        CharacterJump();
    }

    void Movement()
    {
   
        moveHorizontal = Input.GetAxis("Horizontal");
    

        rb2d.velocity = new Vector2(moveHorizontal * moveSpeed, rb2d.velocity.y);
        isrun = Mathf.Abs(moveHorizontal) > 0.01f;
        isjump = !isGrounded;
    }

    void CharacterAnimation()
    {
        
        if (Mathf.Abs(moveHorizontal) > 0 && canmove)
        {
                Debug.Log("++++");
                anim.SetTrigger("isRunning");                
                canmove = false;

                audioManager.Play(runSound);
        }
        else if (Mathf.Abs(moveHorizontal) == 0 && isGrounded)
        {
            if (!canmove)
            {
                anim.SetTrigger("stopRunning");
                audioManager.Stop(runSound);
                Debug.Log("1111");
                canmove = true;
            }
                
            
        }

        if (!facingRight && moveHorizontal > 0)
        {
            CharacterFlip();
        }
        else if (facingRight && moveHorizontal < 0)
        {
            CharacterFlip();
        }
    }

    void CharacterFlip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        var scale = camera.transform.localScale;
        scale.x *= -1;
        camera.transform.localScale = scale;
        
        
    }

    void CharacterAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking && isGrounded)
        {
            Debug.Log("attack");
            anim.SetTrigger("isAttack");
            audioManager?.Play(attackSound);
            combat.DamageEnemy();
            combat.DamageGhosts();
            combat.DamageSkeleton();
            combat.DamageAngel();
            isAttacking = true;
            StartCoroutine(ResetAttack());
        }
    }

    void CharacterRunAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {

            if (isrun || isjump)
            {
                Debug.Log("runattack");
                anim.SetTrigger("isRunAttack");
                audioManager?.Play(runAttackSound);
                combat.DamageEnemy();
                combat.DamageGhosts();
                combat.DamageSkeleton();
                combat.DamageAngel();
                isAttacking = true;
                StartCoroutine(ResetAttack());
                
            }
        }
    }

    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || canDoubleJump)
            {
                if (isGrounded) 
                {
                    anim.SetTrigger("isJumping");
                    audioManager.Stop(runSound);
                    audioManager.Play(jumpSound);
                    rb2d.velocity = Vector2.up * jumpForce;
                    canDoubleJump = true;
                }
                else if (canDoubleJump) 
                {
                    audioManager.Stop(runSound);
                    anim.SetTrigger("isJumping");
                    audioManager.Play(jumpSound);
                    rb2d.velocity = Vector2.up * (jumpForce / 1.5f);
                    canDoubleJump = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            audioManager.Play(landingsound);
            isGrounded = true;
            anim.SetTrigger("stopJumping");
        }

        if (collision.gameObject.CompareTag("Thorn"))
        {
            _characterHealth.Die();
        }

        if(collision.gameObject.CompareTag("End"))
        {
            Debug.Log("gozeliiiiiii");
            _anim.SetTrigger("LevelComplete");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = false;
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        anim.SetTrigger("stopAttack");
        isAttacking = false;
    }
}
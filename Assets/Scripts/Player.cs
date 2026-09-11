using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player obj;

    public int lives = 3;

    public bool isGrounded=false;
    public bool isMoving = false;
    public bool isImmune=false;

    public float speed=5f;
    public float jumpForce=3f;
    public float movHor;

    public float immumeTimeCnt = 0f;
    public float immuneTime=0.5f;

    public LayerMask groundLayer;
    public float radius=0.3f;
    public float groundRayDist=0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    void Awake()
    {
        obj=this;
    }

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        spr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
     /*    movHor=Input.GetAxisRaw("Horizontal");
        isMoving = (movHor !=0f);
        isGrounded=Physics2D.CircleCast(transform.position,radius,Vector3.down,groundRayDist,groundLayer);
        if(Input.GetKeyDown(KeyCode.Space))
            jump();  
            flip(movHor); */
               movHor = Input.GetAxisRaw("Horizontal");
    isMoving = (movHor != 0f);

    isGrounded = Physics2D.CircleCast(
        transform.position,
        radius,
        Vector3.down,
        groundRayDist,
        groundLayer
    );

    if (Input.GetKeyDown(KeyCode.Space))
        jump();

    flip(movHor);

    // Control de animaciones
    if (!isGrounded)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAir"))
            anim.Play("PlayerAir");
    }
    else if (isMoving)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun"))
            anim.Play("PlayerRun");
    }
    else
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
            anim.Play("PlayerIdle");
    } 
    }

    void FixedUpdate(){
        rb.linearVelocity=new Vector2(movHor*speed,rb.linearVelocity.y);
    }

    public void jump(){
        if(!isGrounded) return;
        rb.linearVelocity=Vector2.up*jumpForce;

    }

    private void flip(float _xValue){
        Vector3 theScale=transform.localScale;
        if(_xValue<0)
            theScale.x=Mathf.Abs(theScale.x) * -1;
        else
        if(_xValue >0)
            theScale.x=Mathf.Abs(theScale.x);
        transform.localScale=theScale;    
    }

    void OnDestroy(){
        obj=null;
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
        Destroy(other.gameObject);
    }

  if (other.CompareTag("Finish"))
    {
        SceneManager.LoadScene("level2");
    }


}


}
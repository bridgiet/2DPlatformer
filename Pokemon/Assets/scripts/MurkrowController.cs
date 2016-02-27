using UnityEngine;
using System.Collections;
using Assets.scripts;

public class MurkrowController : MonoBehaviour
{

    //PUBLIC INSTANCE VARIABLES
    public VelocityRange velocityRange;
    public float moveForce;
    public Transform groundCheckFront;

    //PRIVATE INSTANCE VARIABLES
    private Animator _animator;
    private bool _facingRight;
    private Transform _transform;
    private Rigidbody2D _rigidBody2d;
    private bool _isGroundedFront;

    // Use this for initialization
    void Start()
    {
        //Initialize Public Instance Variables
        this.velocityRange = new VelocityRange(200f, 500f);
        this.moveForce = 50f;

        //Initialize Private Instance Variables
        this._transform = gameObject.GetComponent<Transform>();
        this._rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        this._animator = gameObject.GetComponent<Animator>();
        this._facingRight = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this._isGroundedFront = Physics2D.Linecast(
                            this._transform.position,
                            this.groundCheckFront.position,
                            1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(this._transform.position, this.groundCheckFront.position);

        float forceX = 0f;

        //get absolute value of velocity for our gameObject
        float absVelX = Mathf.Abs(this._rigidBody2d.velocity.x);

        if (this._facingRight)
        {
            //Movement Force
            if (absVelX < this.velocityRange.maximum)
            {
                forceX = this.moveForce;
            }
        }
        else
        {
            //Movement force
            if (absVelX < this.velocityRange.maximum)
            {
                forceX = -this.moveForce;
            }
        }
        this._rigidBody2d.AddForce(new Vector2(forceX, 0));

        //call the walk clip
        this._animator.SetInteger("AnimeState", 0);
        if (this._isGroundedFront)
        {
            if (this._facingRight)
            {
                this._facingRight = false;
            }
            else
            {
                this._facingRight = true;
            }

            this._flip();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //call the hurt clip
        if (other.gameObject.CompareTag("Player"))
        {
            this._animator.SetInteger("AnimeState", 2);
        }
    }

    //PRIVATE METHOD
    private void _flip()
    {
        if (this._facingRight)
        {
            this._transform.localScale = new Vector2(1, 1);
        }
        else
        {
            this._transform.localScale = new Vector2(-1, 1);
        }
    }
}

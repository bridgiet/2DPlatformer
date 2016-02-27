using UnityEngine;
using System.Collections;
using Assets.scripts;
using System;

public class MeowthController : MonoBehaviour {
    //PUBLIC INSTANCE VARIABLES
    public VelocityRange velocityRange;
    public float moveForce;
    public float jumpForce;
    public Transform groundCheck;
    public Transform mainCamera;
    public GameController gameController;

    //PRIVATE INSTANCE VARIABLES
    private Animator _animator;
    private float _move;
    private float _jump;
    private bool _facingRight;
    private Transform _transform;
    private Rigidbody2D _rigidBody2d;
    private bool _isGrounded;
    private AudioSource[] _audioSources;
    private AudioSource _jumpSound;
    private AudioSource _coinSound;
    private AudioSource _lifeSound;
    private AudioSource _hurtSound;

    // Use this for initialization
	void Start () {
        //Initialize Public Instance Variables
        this.velocityRange = new VelocityRange(750f, 20000f);
        this.moveForce = 750f;
        this.jumpForce = 20000f;
        
        //Initialize Private Instance Variables
        this._transform = gameObject.GetComponent<Transform>();
        this._rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        this._animator = gameObject.GetComponent<Animator>();
        this._move = 0f;
        this._jump = 0f;
        this._facingRight = true;
        
        //Setup Audio sources
        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._jumpSound = this._audioSources[0];
        this._hurtSound = this._audioSources[1];
        this._lifeSound = this._audioSources[2];
        this._coinSound = this._audioSources[3];

        //Place the hero in the strting position
        this._spawn(-60, 650);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 currentPosition = new Vector3(this._transform.position.x, this._transform.position.y, this.mainCamera.position.z);
        this.mainCamera.position = currentPosition;
        this._isGrounded = Physics2D.Linecast(
                            this._transform.position, 
                            this.groundCheck.position,
                            1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(this._transform.position, this.groundCheck.position);

        float forceX = 0f;
        float forceY = 0f;

        //get absolute value of velocity for our gameObject
        float absVelX = Mathf.Abs(this._rigidBody2d.velocity.x);
        float absVelY = Mathf.Abs(this._rigidBody2d.velocity.y);

        //Ensure the player is grounded before any movement check
        if (this._isGrounded)
        {
            //gets a number between -1 to 1 for both Horizontal and Vertical axes
            this._move = Input.GetAxis("Horizontal");
            this._jump = Input.GetAxis("Vertical");

            if (this._move != 0)
            {
                if (this._move > 0)
                {
                    //Movement force
                    if (absVelX < this.velocityRange.maximum)
                    {
                        forceX = this.moveForce;
                    }
                    this._facingRight = true;
                }
                if (this._move < 0)
                {
                    //Movement Force
                    if (absVelX < this.velocityRange.maximum)
                    {
                        forceX = -this.moveForce;
                    }
                    this._facingRight = false;
                }
                this._flip();

                //call the walk clip
                this._animator.SetInteger("AnimeState", 1);
            }
            else
            {
                //call the idle clip
                this._animator.SetInteger("AnimeState", 0);
            }

            

            if (this._jump > 0)
            {
                //Jump force
                if (absVelY < this.velocityRange.maximum)
                {
                    forceY = this.jumpForce;
                    this._jumpSound.Play();
                }
            }
        }    
        else
        {
            //call the jump clip
            this._animator.SetInteger("AnimeState", 2);
        }

        //Apply the forces to the player
        this._rigidBody2d.AddForce(new Vector2(forceX, forceY));
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Coin")){
            this._coinSound.Play();
            Destroy(other.gameObject);
            this.gameController.ScoreValue ++;
        }
        if (other.gameObject.CompareTag("Amulet"))
        {
            this._lifeSound.Play();
            Destroy(other.gameObject);
            this.gameController.ScoreValue = this.gameController.ScoreValue * 2;
            this.gameController.LivesValue++;
        }
        if (other.gameObject.CompareTag("Death"))
        {
            this.gameController.LivesValue--;     
            this._spawn(-60, 650);
            this._hurtSound.Play();
        }
        if (other.gameObject.CompareTag("Murkrow"))
        {
            this.gameController.LivesValue--;
            this._hurtSound.Play();
            this._spawn(640, 650);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            this.gameController.ScoreValue = this.gameController.LivesValue * this.gameController.ScoreValue;
            this.gameController.LevelCompleted = true;
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

    private void _spawn(int x, int y)
    {
        this._transform.position = new Vector3(x, y, 0);
    }
}

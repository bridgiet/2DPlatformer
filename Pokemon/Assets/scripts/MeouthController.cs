using UnityEngine;
using System.Collections;

public class MeouthController : MonoBehaviour {
    //PRIVATE INSTANCE VARIABLES
    private Animator _animator;
    private float _move;
    private float _jump;
    private bool _facingRight;
    private Transform _transform;
	// Use this for initialization
	void Start () {
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._move = 0f;
        this._jump = 0f;
        this._facingRight = true; 
	}
	
	// Update is called once per frame
	void Update () {
        this._move = Input.GetAxis("Horizontal");
        this._jump = Input.GetAxis("Vertical");
        Debug.Log("WALK" + this._move);
        Debug.Log("JUMP" + this._jump);
        if(this._move != 0)
        {

            if (this._move > 0)
            {
                this._facingRight = true;
            }
            if (this._move < 0)
            {
                this._facingRight = false;
            }
            this._flip();
            //call the walk clip
            this._animator.SetInteger("AnimeState", 0);
        }    
        if(this._jump > 0)
        {
            //call the jump clip
            this._animator.SetInteger("AnimeState", 1);
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

using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    //PUBLIC INSTANCE VARIABLES
    public float speed = 5f;

    //PRIVATE INSTANCE VARIABLES
    private Transform _transform;
    private Vector2 _currentPosition;

    // Use this for initialization
    void Start()
    {
        //Make a reference with the transform component
        this._transform = gameObject.GetComponent<Transform>();

        //Reset the space sprite to the left
        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        this._currentPosition = this._transform.position;
        this._currentPosition -= new Vector2(this.speed, 0);
        this._transform.position = this._currentPosition;

        if (this._currentPosition.x <= -640)
        {
            this.Reset();
        }
    }

    void Reset()
    {
        this._transform.position = new Vector2(640f, 0);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {
    //PRIVATE INSTANCE VARIABLES
    private int _scoreValue;
    private int _livesValue;
    private bool _levelCompleted;
    private AudioSource[] _audioSources;
    private AudioSource _winSound;
    private AudioSource _outSound;

    //PUBLIC INSTANCE VARIABLES
    public Text livesLabel;
    public Text scoreLabel;
    public Text gameOverLabel;
    public Text highSchoolLabel;
    public Text winLabel;
    public Button restartButton;

    //PUBLIC ACCESS METHODS
    public int ScoreValue
    {
        get
        {
            return this._scoreValue;
        }

        set
        {
            this._scoreValue = value;
            this.scoreLabel.text = "Coins: " + this._scoreValue;
        }
    }

    public int LivesValue
    {
        get
        {
            return this._livesValue;
        }

        set
        {
            this._livesValue = value;
            if (this._livesValue <= 0)
            {
                _endGame();
            }
            else
            {
                this.livesLabel.text = "Lives: " + this._livesValue;
               
            }           
        }
    }
    public bool LevelCompleted
    {
        get
        {
            return this._levelCompleted;
        }

        set
        {
            this._levelCompleted = value;
            if (this._levelCompleted == true)
            {
                _endGame();
            }
        }
    }

    // Use this for initialization
    void Start () {
        this._initialize();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //PRIVATE METHODS
    private void _initialize()
    {
        this.ScoreValue = 0;
        this.LivesValue = 5;
        this.LevelCompleted = false;
        this.gameOverLabel.gameObject.SetActive(false);
        this.winLabel.gameObject.SetActive(false);
        this.highSchoolLabel.gameObject.SetActive(false);
        this.restartButton.gameObject.SetActive(false);

        //Setup Audio sources
        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._winSound = this._audioSources[0];
        this._outSound = this._audioSources[1];
    }

    private void _endGame()
    {
        this.highSchoolLabel.text = "High Score: " + _scoreValue;
        if (this.LevelCompleted)
        {
            this.winLabel.gameObject.SetActive(true);
            this._winSound.Play();
        }
        else
        {
            this.gameOverLabel.gameObject.SetActive(true);
            this._outSound.Play();
        }
        this.highSchoolLabel.gameObject.SetActive(true);
        this.scoreLabel.gameObject.SetActive(false);
        this.livesLabel.gameObject.SetActive(false);
        this.restartButton.gameObject.SetActive(true);
    }

    //PUBLIC METHODS
    public void RestartButtonClick()
    {
        Application.LoadLevel("Pokemon");
    }
}

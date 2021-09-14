using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float playerSpeed = 10.0f;
    [SerializeField]
    private float playerJumpForce = 20.0f;
    [SerializeField]
    private Transform playerGroundHitPosition;
    [SerializeField]
    private float playerGroundHitRadius = 0.1f;
    [SerializeField]
    private LayerMask playerGroundHitLayer;
    [SerializeField]
    private float slidingSpeed = 30.0f;
    [SerializeField]
    private float playerMaxYDown = -10.0f;


    private float _playerScaleX;
    private Rigidbody2D _playerRb;
    private Animator _playerAnim;
    private bool _isGrounded = true;
    private bool _isJumped = false;
    private int flipDirection = 1;
    private bool canSlide = true;
    private bool _isPlayerDied = false;

    private bool _isPlayerWon = false;


    public void setIsPlayerWon(bool state)
    {
        _isPlayerWon = state;
    }

    public bool IsPlayerDied { get { return _isPlayerDied; }}

    private protected readonly string runningState = "Running";
    private protected readonly string jumpingState = "Jump";
    private protected readonly string fireTriggerButton = "Melee";
    private protected readonly string shootTriggerButton = "Shoot";
    private protected readonly string slideState = "Slide";
    private protected readonly string dieState = "Die";
    private protected readonly KeyCode crouchKey = KeyCode.C;
    private protected readonly int groundLayer = 0;
    private protected readonly int airLayer = 1;
    private protected readonly string soundManagerTag = "SoundManager";

    void Start()
    {
        _playerScaleX = transform.localScale.x;
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!_isPlayerWon)
        {

            float horizontal = Input.GetAxis("Horizontal");
            bool isFired = Input.GetButtonDown("Fire1");
            bool isShooted = Input.GetButtonDown("Fire2");
            bool isCrouch = Input.GetKeyDown(crouchKey);
            _isGrounded = checkIsGrounded();
            if (!_isPlayerDied)
            {
                flipPlayer(horizontal);
                makeJump();
            }
            setUpAnimation(horizontal, isFired, isShooted, isCrouch);
            checkPlayerDieType1();

            moveThePlayer(horizontal);




        }
        else
        {
            _playerRb.gravityScale = 0;
        }
        
    }

    


    private void flipPlayer(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(_playerScaleX, transform.localScale.y, transform.localScale.z);
            flipDirection = 1;
        }
        else if (horizontal < 0) {
            transform.localScale = new Vector3(-_playerScaleX, transform.localScale.y, transform.localScale.z);
            flipDirection = -1;
        }
    }

    private void moveThePlayer(float horizontal)
    {
        if (!_isPlayerDied)
        {
            transform.Translate(Vector3.right * playerSpeed * horizontal * Time.deltaTime);
        }
        else
        {
            _playerRb.velocity = Vector3.zero;
        }
    }

    private void setUpAnimation(float horizontal, bool fire, bool shoot, bool isCrouch)
    {
        _playerAnim.SetFloat(runningState, Mathf.Abs(horizontal));
        _playerAnim.SetBool(jumpingState, _isJumped);
        if (fire)
        {
            _playerAnim.SetTrigger(fireTriggerButton);
        } else if (shoot)
        {
            _playerAnim.SetTrigger(shootTriggerButton);
        } else if (isCrouch && _isGrounded && Mathf.Abs(horizontal) > 0.1 && canSlide)
        {
            _playerAnim.SetTrigger(slideState);
            //This is a movement only for slide 
            slideMovement();
            canSlide = false;
        }



        if (_isGrounded)
        {
            _playerAnim.SetLayerWeight(groundLayer, 1.0f);
            _playerAnim.SetLayerWeight(airLayer, 0.0f);
        } else if (!_isGrounded)
        {
            _playerAnim.SetLayerWeight(airLayer, 1.0f);
            _playerAnim.SetLayerWeight(groundLayer, 0.0f);

        }

        if (_isPlayerDied)
        {
            _playerAnim.SetBool(dieState, true);
        }
    }



    private void makeJump()
    {

        if (_isGrounded && _isJumped)
        {
            _isJumped = false;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _playerRb.AddRelativeForce(Vector3.up * playerJumpForce,ForceMode2D.Impulse);
            _isGrounded = false;
            _isJumped = true;
        }
    }


    private bool checkIsGrounded()
    {
        return Physics2D.OverlapCircle(playerGroundHitPosition.position, playerGroundHitRadius, playerGroundHitLayer) != null;
    }

    private void slideMovement()
    {
        _playerRb.AddForce((Vector2.right * flipDirection) * slidingSpeed);
    }


    public void SetCanSlide(bool state)
    {
        canSlide = state;
    }



    private void checkPlayerDieType1()
    {
        if(transform.position.y < playerMaxYDown)
        {
            _isPlayerDied = true;
        }
    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class playercon : MonoBehaviourPunCallbacks
{
    public GameObject camera;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float flySpeed = 7f;
    public int score = 0;
    
    public int requiredScore = 5;


    public Animator anim;
   public Rigidbody rb;
   // public GameObject target;

    public static playercon instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine("hello");
        rb = GetComponent<Rigidbody>();
        ScoreManager.instance.gameover.SetActive(false);

        if (photonView.IsMine)
        {
            camera.SetActive(true);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            HandleMovementInput();
            scorings();
            ScoreManager.instance.scoreText.text = "Score: " + score.ToString();

        }
    }

    private void HandleMovementInput()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");
        bool fly = Input.GetKey(KeyCode.LeftShift);

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        if (IsGrounded())
        {
            // Only move when the player is grounded not in the air.
            rb.MovePosition(transform.position + movement);

            if (jump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
            }

            if (fly)
            {
                rb.AddForce(Vector3.up * flySpeed * Time.deltaTime);
                anim.SetBool("Fly", true);
            }
            else
            {
                anim.SetBool("Fly", false);
            }

            anim.SetBool("walk", movement.magnitude > 0);
        }
     
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float rayDistance = 1.1f;
        return Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            score++;
        }
    }

    void scorings()
    {
        if (score >= requiredScore)
        {
            ScoreManager.instance.gameover.SetActive(true);
            photonView.RPC("DisableScoreText", RpcTarget.All);
        }
    }

    [PunRPC]
    private void DisableScoreText()
    {
        ScoreManager.instance.scoreText.gameObject.SetActive(false);
    }

    IEnumerator hello()
    {
        yield return new WaitForSeconds(3f);
        Destroy(ScoreManager.instance.taptext);
    }

}

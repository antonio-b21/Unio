using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining;

    public GameOverScreen GameOverScreen;
    public AudioSource backgroundAudio;
    public AudioClip audioClip;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        /*else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right / 250, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left / 250, ForceMode.VelocityChange);
        }*/
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -1)
        {
            backgroundAudio.PlayOneShot(audioClip, 0.5f);
            
        }

        if (transform.position.y < 0)
        {
            GameOverScreen.Setup(10);
        }

        rigidbodyComponent.velocity = new Vector3(horizontalInput*3 , rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed )
        {
            float jumpPower = 5;
            if (superJumpsRemaining > 0)
            {
                jumpPower = 8;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }

    }

    private void OnTriggerExit(Collider other)
    {
      
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Camera Camera;
    public AudioSource AudioSource;
    public Weapon Weapon;
    public Animator Dead;

    private float moveVertically, moveHorizontally, moveSpeed;

    public bool ableToMove = true;

    private float jumpVelocity;
    private const float gravity = 14.0f;
    private const float jumpForce = 7.0f;

    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private float mouseSensitivity = 3f;
    private float cameraRotationLimit = 85f;

    public float PlayersMaxHealth = 100f;
    public float PlayersCurrentHealth;

    void Start()
    {
        PlayersCurrentHealth = PlayersMaxHealth;
        moveSpeed = 5f;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (ableToMove)
        {
            Jump();
            Move();
            Rotate();
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (PlayersCurrentHealth <= 0)
            {
                PlayersCurrentHealth = 0;
                return;
            }
            PlayersCurrentHealth -= 10;
        }
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            jumpVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = jumpForce;
            }
        }
        else
        {
            jumpVelocity -= gravity * Time.deltaTime;
        }
    }

    void Move()
    {
        if (ableToMove)
        {
            moveVertically = Input.GetAxis("Vertical") * moveSpeed;
            moveHorizontally = Input.GetAxis("Horizontal") * moveSpeed;
            Vector3 movement = new Vector3(moveHorizontally, jumpVelocity, moveVertically);

            movement = controller.transform.TransformDirection(movement);

            controller.Move(movement * Time.deltaTime);
        }
    }

    void Rotate()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        rotation = new Vector3(0f, yRotation, 0f) * mouseSensitivity;

        float xRotation = Input.GetAxisRaw("Mouse Y");
        cameraRotationX = xRotation * mouseSensitivity;


        controller.transform.Rotate((controller.transform.rotation * Quaternion.Euler(rotation).eulerAngles));

        if (Camera != null)
        {
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            Camera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

    public float GetHealthScaled()
    {
        return (float) PlayersCurrentHealth / PlayersMaxHealth;
    }

    public void TakeDamage(float amount)
    {
        PlayersCurrentHealth -= amount;
        if (PlayersCurrentHealth <= 0f)
        {
            PlayersCurrentHealth = 0f;
            Dead.SetTrigger("Dead");
            ableToMove = false;
            AudioListener.volume = 0f;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void Pause()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (Cursor.lockState == CursorLockMode.Locked)
        //    {
        //        Time.timeScale = 1;
        //        Cursor.lockState = CursorLockMode.None;
        //    }
        //    else if (Cursor.lockState == CursorLockMode.None)
        //    {
        //        Time.timeScale = 0;
        //        Cursor.lockState = CursorLockMode.Locked;
        //    }
        //}
    }
}

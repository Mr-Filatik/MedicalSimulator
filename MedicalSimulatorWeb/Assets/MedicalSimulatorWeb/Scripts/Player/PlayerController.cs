using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private HeadController headController = null;
    [SerializeField] 
    private float speed = 2f;

    #region Private Field

    private CharacterController _characterController = null;

    #endregion

    void Awake()
    {
        if (headController == null) throw new Exception($"Не найден компонент дочернего объекта Head");

        _characterController = GetComponent<CharacterController>();
        if (_characterController == null) throw new Exception($"Не найден компонент CharacterController");
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        Rotate();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Settings.MouseVisibility = !Settings.MouseVisibility;
        }
    }

    private void Rotate()
    {
        float rotatePlayer = 1920 * Input.GetAxis("Mouse X") / Screen.width; //сделал чтобы на всех экранах было одинаково
        float rotateHead = -1080 * Input.GetAxis("Mouse Y") / Screen.height; //сделал чтобы на всех экранах было одинаково

        transform.Rotate(Vector3.up * rotatePlayer * Settings.MouseSensivity * Time.deltaTime);
        headController.HeadTilt(rotateHead);
    }

    Vector3 moveDirection;
    private void Move()
    {
        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");

        if (_characterController.isGrounded)
        {
            moveDirection = new Vector3(movex, 0f, movez);
            moveDirection = transform.TransformDirection(moveDirection) * speed;
        }
        else
        {
            moveDirection.y -= 9.81f;
        }

        _characterController.Move(moveDirection * Time.deltaTime);
    }

    /*
    //shift
    //sit (ctrl)
    public float Speed = 2f;
    public float Gravity = 10f;
    public float JumpForce = 5f;
    public float Sensivity = 100f;
    public float Border = 30f;

    CharacterController chc;
    Vector3 moveDirection;
    public GameObject Head;

    // Start is called before the first frame update
    void Start()
    {
        chc = GetComponent<CharacterController>();
    }

    bool isVisibleCursore = true;
    void Update()
    {
        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");
        float rotatePlayer = Input.GetAxis("Mouse X");
        float rotateHead = -Input.GetAxis("Mouse Y");
        bool isJump = Input.GetKeyDown(KeyCode.Space);

        transform.Rotate(Vector3.up * rotatePlayer * Sensivity * Time.deltaTime);
        Head.transform.Rotate(Vector3.right * rotateHead * Sensivity * Time.deltaTime);

        //Head.transform.localRotation = Quaternion.Euler(Mathf.Clamp(Head.transform.localEulerAngles.x, -15f, 15f), Head.transform.localEulerAngles.y, Head.transform.eulerAngles.z);
        Head.transform.localRotation = Quaternion.Euler(GetX(), Head.transform.localEulerAngles.y, Head.transform.eulerAngles.z);

        float GetX()
        {
            if (Head.transform.localEulerAngles.x < 180f && Head.transform.localEulerAngles.x > Border)
            {
                return Border;
            }
            if (Head.transform.localEulerAngles.x > 180f && Head.transform.localEulerAngles.x < 360f - Border)
            {
                return 360f - Border;
            }
            return Head.transform.localEulerAngles.x;
        }

        if (chc.isGrounded)
        {
            moveDirection = new Vector3(movex, 0f, movez);
            moveDirection = transform.TransformDirection(moveDirection) * Speed;
            if (isJump)
            {
                moveDirection.y += JumpForce;
            }
        }
        else 
        {
            moveDirection.y -= Gravity * Time.deltaTime; 
        }

        //Debug.Log(chc.velocity);

        chc.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isVisibleCursore = !isVisibleCursore;
            Cursor.visible = isVisibleCursore;
            if (isVisibleCursore)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    */
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [Header("-----Movement-----")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float gravityScale;

    [Header("-----Jump-----")]
    [SerializeField]
    private float jumpHeight;

    [Header("-----Crouch-----")]
    [SerializeField]
    private float originalScale;
    [SerializeField]
    private float scaleWhenCrouched;
    [SerializeField]
    private float crouchedSpeed;

    [Header("-----Head Bob-----")]
    [SerializeField]
    private Transform headBobAnchor;
    [SerializeField]
    private float bobFrequency;
    [SerializeField]
    private float bobAmplitude;

    [Header("-----Ground Detection-----")]
    [SerializeField]
    private Transform feet;
    [SerializeField]
    private float detectionRadius;
    [SerializeField]
    private LayerMask whatIsGround;

    [Header("-----Message App-----")]
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private MessageDataSO messageData;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageListText;

    private CharacterController controller;
    private Camera cam;

    private Vector3 verticalMovement;

    private float bobTimer;
    private Vector3 originalHeadPosition;

    private PlayerInput playerInput;
    private Vector2 input;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;

        originalHeadPosition = headBobAnchor.localPosition;
    }
    private void OnEnable()
    {
        playerInput.actions["Jump"].started += Jump;
        playerInput.actions["Move"].performed += Move;
        playerInput.actions["Move"].canceled += MoveCancelled;
        playerInput.actions["Message"].started += ToggleMessagePanel;
        /*deshabilitar los controles del gameplay y activar los controles de la UI.
        playerInput.SwitchCurrentActionMap("UI");*/

        //si se desconecta el control que estes usando
        playerInput.deviceLostEvent.AddListener((x) => Time.timeScale = 0f);

        eventManager.OnMessageUpdated += UpdateMessageList;
    }
    private void OnDisable()
    {
        eventManager.OnMessageUpdated -= UpdateMessageList;
    }

    void Update()
    {
        MoveAndRotate();
        ApplyGravity();
        ApplyHeadBob();
        //Crouch();
    }


    private void Move(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>();
    }
    private void MoveCancelled(InputAction.CallbackContext ctx)
    {
        input = Vector2.zero;
    }
    private void Jump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            verticalMovement.y = 0;
            verticalMovement.y = Mathf.Sqrt(-2 * gravityScale * jumpHeight);
        }
    }
    private void ApplyHeadBob()
    {
        if (input.sqrMagnitude > 0 && IsGrounded()) // Solo si nos movemos y tocamos el suelo
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float horizontalBob = Mathf.Cos(bobTimer) * bobAmplitude;
            float verticalBob = Mathf.Sin(bobTimer * 2) * bobAmplitude; // Más rápido en Y

            headBobAnchor.localPosition = originalHeadPosition + new Vector3(horizontalBob, verticalBob, 0);
        }
        else
        {
            bobTimer = 0;
            headBobAnchor.localPosition = Vector3.Lerp(headBobAnchor.localPosition, originalHeadPosition, Time.deltaTime * 5f);
        }
    }

    //private void Crouch()
    //{
    //    if(Input.GetKeyDown(KeyCode.LeftControl))
    //    {
    //        transform.localScale /= 2; //A la mitad TODO para que no deforme objetos.
    //    }
    //    else if(Input.GetKeyUp(KeyCode.LeftControl))
    //    {
    //        transform.localScale *= 2;
    //    }
    //}
    private void ToggleMessagePanel(InputAction.CallbackContext ctx)
    {
        messagePanel.SetActive(!messagePanel.activeSelf);
        if (messagePanel.activeSelf)
        {
            UpdateMessageList();
        }
    }
    private void UpdateMessageList()
    {
        messageListText.text = "";
        foreach(var msg in messageData.messages)
        {
            if (msg.isTriggered)
            {
                messageListText.text += $"<b>{msg.title}</b>\n{msg.content}\n\n";
            }
        }
    }

    private void MoveAndRotate()
    {
        //Se aplica al cuerpo la rotación que tenga la cámara.
        //transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);

        ////Si hay input...
        if (input.sqrMagnitude > 0)
        {
            //Se calcula el ángulo en base a los inputs
            float angleToRotate = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            //Se rota el vector (0, 0, 1) a dicho ángulo
            Vector3 movementInput = Quaternion.Euler(0, angleToRotate, 0) * Vector3.forward;

            //Se aplica movimiento en dicha dirección.
            controller.Move(movementInput * movementSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        verticalMovement.y += gravityScale * Time.deltaTime;
        controller.Move(verticalMovement * Time.deltaTime);
    }
    private bool IsGrounded()
    {
        return Physics.CheckSphere(feet.position, detectionRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(feet.position, detectionRadius);
    }
}

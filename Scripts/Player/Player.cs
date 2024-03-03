using UnityEngine;

public enum PlayerState { Idle, Interacting};

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GameObject[] playerVisuals;

    private CharacterController controller;
    private Vector3 moveInput;

    private PlayerState playerState = PlayerState.Idle;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;

    private bool bCanMove = true;

    [Header("Player Falling")]
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float gravity = -9.8f;
    private bool bIsGrounded;
    private Vector2 playerVelocity;

    [Header("Player Look")]
    [SerializeField] private Camera playerCam;
    [SerializeField] private float xLookSensitivity;
    [SerializeField] private float yLookSensitivity;
    private Vector2 lookPosition;
    private float xRotation = 0f;

    [Header("Player Interaction")]
    [SerializeField] private Transform interactionTransform;
    [SerializeField] private float interactionDist;

    private KeyCode interact = KeyCode.E;
    private BaseItem interactableObject;

    private KeyCode pause = KeyCode.Escape;
    private bool bIsPaused = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        else
        {
            Instance = this;
        }

        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        SetCanMoveCursor(false);
    }

    private void Update()
    {
        bIsGrounded = Physics.CheckSphere(groundChecker.position, checkRadius, whatIsGround);

        if (bCanMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput = moveInput.normalized;

            lookPosition.x = Input.GetAxisRaw("Mouse X");
            lookPosition.y = Input.GetAxisRaw("Mouse Y");

            HandleMovement();
            HandleLook();
            HandleInteraction();
        }

        if (Input.GetKeyDown(interact) && interactableObject != null)
        {
            if(bIsPaused)
            {
                return;
            }

            interactableObject.Interact();
            switch (interactableObject.GetItemDataSO().ItemType)
            {
                case ItemType.Task:
                    CustomEvents.OnInteractableObjectSelected?.Invoke(interactableObject.GetItemDataSO());
                    break;
                case ItemType.Both:
                case ItemType.Story:
                    CustomEvents.OnInteractableObjectSelected?.Invoke(null);
                    break;
                default:
                    Debug.LogWarning("No definition for state passed in");
                    break;
            }
        }

        if(Input.GetKeyDown(pause) && playerState == PlayerState.Idle)
        {
            PauseGame();
        }
    }

    private void HandleMovement()
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = moveInput.x;
        moveDir.z = moveInput.y;
        controller.Move(transform.TransformDirection(moveDir * moveSpeed * Time.deltaTime));

        playerVelocity.y += gravity * Time.deltaTime;
        if (bIsGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleLook()
    {
        xRotation -= (lookPosition.y * Time.deltaTime) * yLookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * (lookPosition.x * Time.deltaTime) * xLookSensitivity);
    }

    private void HandleInteraction()
    {
        if (Physics.Raycast(interactionTransform.position, interactionTransform.forward, out RaycastHit hit, interactionDist))
        {
            if (hit.collider.TryGetComponent(out BaseItem obj))
            {
                SetInteractable(obj);
            }
            else
            {
                SetInteractable(null);
            }
        }
        else
        {
            SetInteractable(null);
        }
    }

    private void PauseGame()
    {
        bIsPaused = !bIsPaused;
        CustomEvents.OnSetIsGamePaused?.Invoke(bIsPaused);
    }

    public void SetPlayerState(PlayerState _state)
    {
        playerState = _state;
    }

    //Sets the game rendering the visuals of the player body for when they are not needed
    public void RenderPlayerBody(bool _state)
    {
        if(_state)
        {
            foreach(GameObject obj in playerVisuals)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in playerVisuals)
            {
                obj.SetActive(false);
            }
        }
    }

    private void SetInteractable(BaseItem _item)
    {
        if (_item != null)
        {
            interactableObject = _item;
            CustomEvents.OnInteractableObjectSelected?.Invoke(interactableObject.GetItemDataSO());
        }
        else
        {
            interactableObject = null;
            CustomEvents.OnInteractableObjectSelected?.Invoke(null);
        }
    }

    public void SetCanPlayerMove(bool _canMove)
    {
        bCanMove = _canMove;
    }

    public void SetCanMoveCursor(bool _canUse)
    {
        if (_canUse)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public bool IsMoving()
    {
        return moveInput.x != 0 || moveInput.y != 0;
    }
}
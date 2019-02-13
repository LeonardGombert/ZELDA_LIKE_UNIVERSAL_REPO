using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileMovement : MonoBehaviour
{
    #region Variable Decarations
    private Animator anim;

    //Basic Move Vectors
    private Vector3 originTile;
    private Vector3 destinationTile;
    private Vector3 newPosition;

    //Ability1 Vector Values
    private Vector3 mousePosition;
    private Vector3 objectPosition;
    private Vector3 targetObjectPosition;
    private Vector3 facingDirection;

    private GameObject targetedObject;

    private bool isMoving = false;
    private bool movementIsCoolingDown = false;
    private bool isDashing = false;
    private bool blinkIsCoolingdown = false;

    [SerializeField]
    private float blinkMovementMultiplier;
    [SerializeField]
    private float blinkCooldown;

    private float movementCooldown = 0.3f;
    private float moveTime = 0.3f;
    #endregion

    #region Monobehavior Callbacks
    private void Awake()
    {
        //EnemyTileChase.playerHasMoved;
        anim = GetComponent<Animator>();
        //Snapping player to grid position with offset
        this.transform.position = ExtensionMethods.FloorVect3Values(this.transform.position) + new Vector2(0.5f, 0.75f);
    }
    // Update is called once per frame
    void Update()
    {
        //Movement Overrides all other functions
        if (isMoving || movementIsCoolingDown || isDashing) return;
        
        int horizontal = 0;
        int vertical = 0;
        
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        
        if (horizontal != 0)vertical = 0;
        
        if (horizontal != 0 || vertical != 0)
        {
            StartCoroutine(microMovementCooldown(movementCooldown));
            Move(horizontal, vertical, 0);
        }
        FetchObjectPosition();
    }
    #endregion

    #region Player Functions
    #region Basic Movement
    private void Move(int xDirection, int yDirection, int zDirection)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            BlinkCooldown(blinkCooldown);
            RunBlinkCalculations((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"), 0);
        }

        originTile = transform.position;
        destinationTile = originTile + new Vector3(xDirection, yDirection, zDirection);
        
        StartCoroutine(TileMovement(destinationTile));
    }

    private IEnumerator TileMovement(Vector3 destinationPosition)
    {
        isMoving = true;
        EnemyTileChase.playerHasMoved = true;

        float sqrRemainingDistanceToDestination = (transform.position - destinationPosition).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistanceToDestination > float.Epsilon)
        {
            newPosition = Vector3.MoveTowards(transform.position, destinationPosition, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistanceToDestination = (transform.position - destinationPosition).sqrMagnitude;

            newPosition = ExtensionMethods.FloorVect3Values(newPosition);
            Debug.Log("My Destination is " + newPosition);
            
            facingDirection = ExtensionMethods.ReturnSign(newPosition);
            Debug.Log("My facing direction is " + facingDirection);

            yield return null;
        }

        isMoving = false;
        EnemyTileChase.playerHasMoved = false;
    }

    private IEnumerator microMovementCooldown(float cooldown)
    {
        movementIsCoolingDown = true;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        movementIsCoolingDown = false;
    }
    #endregion

    #region Blink Ability
    private void RunBlinkCalculations(int xDirection, int yDirection, int zDirection)
    {
        originTile = transform.position;
        destinationTile = originTile + new Vector3(xDirection * blinkMovementMultiplier, yDirection * blinkMovementMultiplier, zDirection);

        transform.position = destinationTile;
    }

    private IEnumerator BlinkCooldown(float cooldown)
    {
        blinkIsCoolingdown = true;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        blinkIsCoolingdown = false;
    }
    #endregion

    #region Move Tile Ability
    private void FetchObjectPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = ExtensionMethods.getFlooredWorldPosition(mousePosition);

            Debug.Log("My mouse click position is " + mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition);

            if (hit.collider.tag == "test")
            {
                targetedObject = hit.collider.gameObject;
                Debug.Log("I have hit an object called " + targetedObject.name);

                targetedObject.AddComponent<MoveObject>();
            }
        }
    }
    #endregion
    #endregion
}

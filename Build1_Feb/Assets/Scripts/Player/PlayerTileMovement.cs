using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileMovement : MonoBehaviour
{
    #region Variable Decarations
    private Animator anim;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //Snapping player to grid position with offset
        this.transform.position = ExtensionMethods.FloorVect3Values(this.transform.position) + new Vector2(0.5f, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Overrides all other functions
        if (isMoving || movementIsCoolingDown || isDashing) return;
        
        //Temp storage for movement directions
        int horizontal = 0;
        int vertical = 0;

        //Updated Movement Directions
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        //Set Animator Blend Tree Parameters
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);

        //horizontal direction ovverides vertical (no diagonal movement)
        if (horizontal != 0)vertical = 0;

        //if movement is inputed, run the move functions
        if (horizontal != 0 || vertical != 0)
        {
            StartCoroutine(microMovementCooldown(movementCooldown));
            Move(horizontal, vertical, 0);
        }
    }

    private void Move(int xDirection, int yDirection, int zDirection)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            BlinkCooldown(blinkCooldown);
            RunBlinkCalculations((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"), 0);
        }

        Vector3 originTile = transform.position;
        Vector3 destinationTile = originTile + new Vector3(xDirection, yDirection, zDirection);
        
        StartCoroutine(TileMovement(destinationTile));
    }

    private IEnumerator TileMovement(Vector3 destinationPosition)
    {
        isMoving = true;
        
        float sqrRemainingDistanceToDestination = (transform.position - destinationPosition).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistanceToDestination > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, destinationPosition, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistanceToDestination = (transform.position - destinationPosition).sqrMagnitude;

            newPosition = ExtensionMethods.FloorVect3Values(newPosition);
            Debug.Log("My Destination is " + newPosition);

            yield return null;
        }
        
        isMoving = false;
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

    private void RunBlinkCalculations(int xDirection, int yDirection, int zDirection)
    {
        //Calculate blink movement values
        Vector3 originTile = transform.position;
        Vector3 destinationTile = originTile + new Vector3(xDirection * blinkMovementMultiplier, yDirection * blinkMovementMultiplier, zDirection);

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
}

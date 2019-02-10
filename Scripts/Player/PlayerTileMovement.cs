using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileMovement : MonoBehaviour
{
    public Tilemap groundTiles;
    public Tilemap obstacleTiles;

    public Animator anim;

    public bool isMoving = false;

    public bool movementIsCoolingDown = false;
    private float moveTime = 0.3f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //Snapping player to grid position with offset
        this.transform.position = ExtensionMethods.FloorVect3Values(this.transform.position) + new Vector3(0.5f, 0.5f);
        Debug.Log("My position is " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Overrides all other functions
        if (isMoving || movementIsCoolingDown) return;
        
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
            StartCoroutine(microMovementCooldown(0.3f));
            Move(horizontal, vertical, 0);
        }
    }

    private void Move(int xDirection, int yDirection, int zDirection)
    {
        Vector3 originTile = transform.position;
        Vector3 destinationTile = originTile + new Vector3(xDirection, yDirection, zDirection);

        Debug.Log("My Starting point is " + originTile);
        Debug.Log("My Destination is " + destinationTile);
        
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
}

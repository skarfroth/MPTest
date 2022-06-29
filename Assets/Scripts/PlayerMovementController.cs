using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementController : NetworkBehaviour
{
    public float movementSpeed = 5.0f;
    public GameObject playerModel;

    private Rigidbody2D rb2d;
    private Vector2 movementVector;

    public SpriteRenderer PlayerSprite;
    public Material[] PlayerColors;

    private void Start()
    {
        playerModel.SetActive(false);
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (playerModel.activeSelf == false)
            {
                SetPosition();
                PlayerCosmeticsSetup();
                playerModel.SetActive(true);
            }
            if (hasAuthority)
            {
                movementVector.x = Input.GetAxis("Horizontal");
                movementVector.y = Input.GetAxis("Vertical");
                Rotation();
            }
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Game" && hasAuthority)
        {
            Movement();
        }
    }

    public void Movement()
    {
        rb2d.velocity = Vector2.ClampMagnitude(movementVector, 1) * movementSpeed;
    }

    public void SetPosition()
    {
        transform.position = Vector2.zero;
    }

    public void Rotation()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void PlayerCosmeticsSetup()
    {
        PlayerSprite.material = PlayerColors[CharacterCosmetics.instance.currentColorIndex];
    }
}

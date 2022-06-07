using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementController : NetworkBehaviour
{
    public float Speed = 0.1f;
    public GameObject PlayerModel;

    private void Start()
    {
        PlayerModel.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (PlayerModel.activeSelf == false)
            {
                SetPosition();
                PlayerModel.SetActive(true);
            }
            if (hasAuthority)
            {
                // Movement();
            }
        }
    }

    public void SetPosition()
    {
        transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
    }
}

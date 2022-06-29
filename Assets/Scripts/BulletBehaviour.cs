using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(transform.up * Time.deltaTime, Space.World);
    }
}

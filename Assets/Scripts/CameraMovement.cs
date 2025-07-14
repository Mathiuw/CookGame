using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 cameraOffset = Vector2.zero;

    private void Update()
    {
        transform.position = new Vector3(target.position.x + cameraOffset.x, target.position.y + cameraOffset.y, transform.position.z);
    }
}
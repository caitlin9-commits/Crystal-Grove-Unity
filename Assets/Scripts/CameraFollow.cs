using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //this allows me to set the tagret of the camera eg. the nomad
    public Vector3 offset;
    public float smoothSpeed = 5f;

    public Vector3 rotation;
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(rotation);
    }
}

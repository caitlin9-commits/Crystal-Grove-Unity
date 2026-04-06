using UnityEngine;
//this class is linked to the main camera game object
//this makes the camera follow The Nomad asset
public class CameraFollow : MonoBehaviour 
{
    public Transform target; //this allows me to set the target of the camera eg. the nomad
    public Vector3 offset; //inputted co-ords in relation to the target (nomad)
    public float smoothSpeed = 5f; //camera movement smoothness

    public Vector3 rotation; //inputted value for camera rotation (camera faces the nomad asset)
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset; //sets the position of the camera
        transform.position = Vector3.Lerp( //moves camera as nomad moves
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(rotation); //sets rotation of camera
    }
}

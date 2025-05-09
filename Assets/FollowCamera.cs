using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // The target to follow (your character)
    public float distance = 5.0f; // Distance from the target
    public float height = 2.0f; // Height above the target
    public float rotationDamping = 3.0f; // Damping for rotation
    public float heightDamping = 2.0f; // Damping for height

    void LateUpdate()
    {
        if (!target)
            return;

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Set the rotation of the camera
        transform.LookAt(target);
    }
}

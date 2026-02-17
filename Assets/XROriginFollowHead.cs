using UnityEngine;

[DefaultExecutionOrder(1000)] // run late
public class XROriginFollowHead : MonoBehaviour
{
    [Header("Assign Main Camera (under Camera Offset)")]
    [SerializeField] private Transform head;

    [Header("Optional: if you have a CharacterController on the XR Origin, assign it")]
    [SerializeField] private CharacterController characterController;

    void Reset()
    {
        characterController = GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        if (head == null) return;

        // Camera local offset inside the rig
        Vector3 local = head.localPosition;

        // We only want to move the rig on the ground plane
        Vector3 localXZ = new Vector3(local.x, 0f, local.z);

        if (localXZ.sqrMagnitude < 0.000001f) return;

        // Convert local XZ to world motion
        Vector3 worldMove = transform.TransformVector(localXZ);

        // Move the rig root (prefer CharacterController if present)
        if (characterController != null)
            characterController.Move(worldMove);
        else
            transform.position += worldMove;

        // Cancel out the head's local XZ so it doesn't "drift" away from the capsule
        head.localPosition -= localXZ;
    }
}



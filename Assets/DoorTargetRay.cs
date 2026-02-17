using UnityEngine;

public class DoorTargetRay : MonoBehaviour
{
    [SerializeField] private DoorSwing door;   
    [SerializeField] private bool toggle = false;

    public void Trigger()
    {
        if (door == null) return;
        if (toggle) door.Toggle();
        else door.Open();
    }
}

using UnityEngine;
using System.Collections;

public class DoorSwing : MonoBehaviour
{
    public float openAngle = 90f;
    public float openTime = 0.5f;

    private bool isOpen = false;
    private bool isMoving = false;
    private Quaternion closedRot;

    void Awake()
    {
        closedRot = transform.localRotation;
    }

    public void Open()
    {
        if (isMoving || isOpen) return;

        Quaternion openRot = closedRot * Quaternion.Euler(0f, openAngle, 0f);
        StartCoroutine(RotateTo(openRot));
        isOpen = true;
    }

    public void Toggle()
    {
        if (isMoving) return;

        Quaternion target = isOpen ? closedRot : closedRot * Quaternion.Euler(0f, openAngle, 0f);
        StartCoroutine(RotateTo(target));
        isOpen = !isOpen;
    }

    private IEnumerator RotateTo(Quaternion targetRot)
    {
        isMoving = true;

        Quaternion start = transform.localRotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / openTime;
            transform.localRotation = Quaternion.Slerp(start, targetRot, t);
            yield return null;
        }

        transform.localRotation = targetRot;
        isMoving = false;
    }
}

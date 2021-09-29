using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRot : MonoBehaviour
{
    [SerializeField] private bool Interactable;
    [SerializeField] private bool isDoneRot = true;
    [SerializeField] private Vector3 DesiredRot;

    [SerializeField] private float RotTime;

    // Update is called once per frame
    void Update()
    {
        if (Interactable)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(DesiredRot), Time.deltaTime * RotTime);
            isDoneRot = false;
        }

        if (Mathf.Abs(transform.localRotation.eulerAngles.y - DesiredRot.y) < 1)
        {
            isDoneRot = true;
        }

        if (DesiredRot.y > 359)
        {
            DesiredRot.y = 0;
        }
    }

    private void OnMouseDown()
    {
        if (isDoneRot)
        {
            DesiredRot.y = transform.localRotation.eulerAngles.y + 90;

            Interactable = true;
        }
    }
}

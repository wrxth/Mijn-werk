using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drijf : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float DepthBeforeSubMerged = 1f;
    [SerializeField] private float DisplacementAmount = 3f;
    [SerializeField] private float FloaterCount = 4f;
    [SerializeField] private float WaterDrag = 1f;
    [SerializeField] private float WaterAnglularDrag = 0.5f;
    

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / FloaterCount, transform.position, ForceMode.Acceleration);
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
        if (gameObject.transform.position.y < waveHeight)
        {
            float displacementMulti = Mathf.Clamp01((waveHeight - gameObject.transform.position.y) / DepthBeforeSubMerged) * DisplacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti),transform.position ,0f);
            rb.AddForce(displacementMulti * -rb.velocity * WaterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMulti * -rb.angularVelocity * WaterAnglularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);

        }
    }

}

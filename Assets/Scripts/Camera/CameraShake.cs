using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform cameraAxis;
    Vector3 originalPos = new Vector3();

    [SerializeField] private float shakeDuration = 1.0f;
    private float shakeDurationOrignal;

    [SerializeField] private float maxForce = 30.0f;

    [SerializeField] private float maxShakeAmount = 2f;
    [SerializeField] private float decreaseFactor = 1.0f;

    private Coroutine shakeEvent;
    private void Start()
    {
        shakeDurationOrignal = shakeDuration;
        List<GunFire> guns = FindObjectsOfType<GunFire>().ToList();
        foreach (GunFire gun in guns)
        {
            gun.onGunFiredEvent += Shake;
        }
    }

    public void Shake (float fireForce, Vector3 Pos)
    {
        if (shakeEvent == null)
        {
            shakeEvent = StartCoroutine(DoShake(fireForce));
        }
    }

    IEnumerator DoShake(float fireForce)
    {
        if (fireForce > maxForce)
            fireForce = maxForce;

        do
        {
            cameraAxis.localPosition = originalPos + Random.insideUnitSphere * maxShakeAmount * (fireForce/maxForce);
            shakeDuration -= Time.deltaTime * decreaseFactor;
            yield return null;
        }
        while (shakeDuration > 0);

        if (shakeDuration <= 0)
        {
            shakeDuration = 0f;
            cameraAxis.localPosition = originalPos;
        }

        yield return new WaitForSeconds(0.01f);
        shakeEvent = null;
        shakeDuration = shakeDurationOrignal;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RippleEffectPostProccessor : MonoBehaviour {

    [SerializeField] private Material RippleMaterial;
    [SerializeField] private float MaxAmount = 50f;

    [Range(0, 1)]
    [SerializeField] private float Friction = .9f;

    private float Amount = 0f;

    private void Start()
    {
        List<GunFire> guns = FindObjectsOfType<GunFire>().ToList();
        foreach (GunFire gun in guns)
        {
            gun.onGunFiredEvent += Ripple;
        }
    }

    void Update()
    {
        RippleMaterial.SetFloat("_Amount", Amount);
        Amount *= Friction;
    }

    public void Ripple(float fireForce, Vector3 position)
    {
        if (fireForce > MaxAmount)
            fireForce = MaxAmount;

        Amount = fireForce;
        Vector3 pos = position;
        RippleMaterial.SetFloat("_CenterX", pos.x);
        RippleMaterial.SetFloat("_CenterY", pos.y);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, RippleMaterial);
    }
}

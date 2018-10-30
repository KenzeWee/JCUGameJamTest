﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions
{
    /*------------------------RIGIDBODY2D--------------------------*/
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff, ForceMode2D.Impulse);
    }

    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff;
        body.AddForce(baseForce);

        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }

    /*--------------------- Find Game Objects By Layer ------------------------------*/
    public static List<GameObject> FindGameObjectsByLayer(int layerToSearch)
    {
        GameObject[] go = Object.FindObjectsOfType<GameObject>();
        List<GameObject> listToReturn = new List<GameObject>();
        foreach (GameObject gameObj in go)
        {
            if (gameObj.layer == layerToSearch)
                listToReturn.Add(gameObj);
        }
        return listToReturn;
    }

    /*------------------- Vector3 ------------------*/
    public static Vector3 With (this Vector3 originalVec, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? originalVec.x, y ?? originalVec.y, z ?? originalVec.z);
    }
}
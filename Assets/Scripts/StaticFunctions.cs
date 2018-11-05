using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions
{
    public enum CONTROLLERTYPE { NULL, CONTROLLER01 = 1, CONTROLLER02 = 2, CONTROLLER03 = 3, CONTROLLER04 = 4, KEYBOARD = 5 };

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

    public static void Explode(this GameObject source, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(source.transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, source.transform.position, explosionRadius);
        }
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
    public static Vector3 With(this Vector3 originalVec, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? originalVec.x, y ?? originalVec.y, z ?? originalVec.z);
    }

    /*---------------------List------------------------*/
    public static T RandomObject<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Shuffles the list randomly
    /// </summary>
    public static List<T> RandomizeList<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }

    public static List<PlayerVariable> QuickSortList(this List<PlayerVariable> list)
    {
        System.Random r = new System.Random();
        List<PlayerVariable> less = new List<PlayerVariable>();
        List<PlayerVariable> greater = new List<PlayerVariable>();
        if (list.Count <= 1)
            return list;
        int pos = r.Next(list.Count);

        PlayerVariable pivot = list[pos];
        list.RemoveAt(pos);
        foreach (PlayerVariable player in list)
        {
            if (player.CurrentScore <= pivot.CurrentScore)
            {
                less.Add(player);
            }
            else
            {
                greater.Add(player);
            }
        }
        return concat(QuickSortList(less), pivot, QuickSortList(greater));
    }

    public static List<PlayerVariable> concat(List<PlayerVariable> less, PlayerVariable pivot, List<PlayerVariable> greater)
    {
        List<PlayerVariable> sorted = new List<PlayerVariable>(less);
        sorted.Add(pivot);
        foreach (PlayerVariable i in greater)
        {

            sorted.Add(i);
        }

        return sorted;
    }
}
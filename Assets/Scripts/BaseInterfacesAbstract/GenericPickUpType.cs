using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPickUpType<T> : MonoBehaviour, IPickUp
{
    //can be type of projectile to give
    //could be amount of health to return 
    //etc etc

    [SerializeField] private T pickUpItem;
    public T PickUpItem { get { return pickUpItem; } }

    //What this pickup does when its picked up
    //e.g. replenish health
    public abstract void PickUpBehaviour<TPlayerType>(GenericPlayer<TPlayerType> player) where TPlayerType : IInput;
}

public interface IPickUp
{
    void PickUpBehaviour<TPlayerType>(GenericPlayer<TPlayerType> player) where TPlayerType : IInput;
}

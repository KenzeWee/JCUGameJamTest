using UnityEngine;

[RequireComponent(typeof(AI_Behaviour))]
public class AI : GenericPlayer<AI_Behaviour>
{
    protected override void SuscribeToEvents()
    {
        base.SuscribeToEvents();
        GameManager.Instance.onPlayerKnockedOutEvent += inputManager.PlayerKnockOutCheck;
    }

    protected override void UnsuscribeToEvents()
    {
        base.UnsuscribeToEvents();
        GameManager.Instance.onPlayerKnockedOutEvent -= inputManager.PlayerKnockOutCheck;
    }
}

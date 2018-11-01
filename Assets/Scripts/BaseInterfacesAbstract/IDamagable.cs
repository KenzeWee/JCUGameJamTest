public delegate void OnDie();

public interface IDamagable { 
    int HP { get; }
    void ChangeHealth(int amount);
    event OnDie onDieEvent;
}

public delegate void OnDie();

public interface IDamagable { 
    int HP { get; }
    void TakeDamage(int amount);
    event OnDie onDieEvent;
}

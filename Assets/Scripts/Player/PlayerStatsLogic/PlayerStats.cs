using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable, IStrengthDamageble
{
    public static PlayerStats Instance { get; private set; }
    [SerializeField] private PlayerDescriptor _descriptor;
    [SerializeField] private bool _isPlayelLowHp = false;
    private bool _isTakeDamage = false;
    private bool _isPlayerDead = false;
    private float _killsCounter = 0;
    public float GetKillsCounter() 
    {
        return _killsCounter;
    }
    public bool GetIsPlayerLowHp()
    {
        return _isPlayelLowHp;
    }
    public bool TakeDamage(float damageAmount)
    {
        _isTakeDamage = true;
        _descriptor.PlayerHealth -= damageAmount;
        return true;
    }
    public void TakeStrengthDamage(float strengthDamage)
    {
        _isTakeDamage = true;
        _descriptor.Strength -= strengthDamage;
    }
    public void SetIsTakeDamageFalse() { _isTakeDamage = false; }   
    public bool GetIsTakeDamage() { return _isTakeDamage; }   
    public bool GetIsUltimateReady() 
    {
        if(_descriptor.Strength == _descriptor.MaxStrength) { return true; }
        else { return false; }
    }
    public float GetHPValue() 
    {
        return _descriptor.PlayerHealth;
    }
    public float GetStrengthValue() 
    {
        return _descriptor.Strength;
    }
    public bool GetIsPlayerDead() { return _isPlayerDead; }
    private void Death()
    {
        
        _isPlayerDead = true;
    }
    private void Start()
    {
        StatsEventManager.AddEventListener(EnemyBoutyKill);
        StatsRicochetAddedEventManager.AddEventListener(RicochetKill);
        WeaponController weaponController = gameObject.GetComponent<WeaponController>();
        weaponController.ultimateStrengthReset.AddListener(UltimateStrengthReset);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        HealthChecker();
        StrengthChecker();
    }
    private void HealthChecker() 
    {
        if(_descriptor.PlayerHealth < 0) { Death(); }
        else if (_descriptor.PlayerHealth < _descriptor.PlayerLowHpBorder)
        {
            _isPlayelLowHp = true;
        }
        else 
        {
            _isPlayelLowHp = false;
        }
        if (_descriptor.PlayerHealth > _descriptor.MaxPlayerHealth)
        {
            _descriptor.PlayerHealth = _descriptor.MaxPlayerHealth;
        }
    }
    private void StrengthChecker() 
    {
        if(_descriptor.Strength < 0f) 
        {
            _descriptor.Strength = 0f;
        }
        else if(_descriptor.Strength > _descriptor.MaxStrength) 
        {
            _descriptor.Strength = _descriptor.MaxStrength;
        }
    }
    
    private void EnemyBoutyKill(float enemyBounty) 
    {
        _killsCounter++;
        _descriptor.Strength += enemyBounty;
    }
    private void UltimateStrengthReset() 
    {
        _descriptor.Strength = 0f;
    }

    private void RicochetKill()
    {
        float chance = UnityEngine.Random.value;
        if( chance <= 0.3f) 
        {
            _descriptor.PlayerHealth += _descriptor.MaxPlayerHealth / 2f;
        }
        else 
        {
            _descriptor.Strength += 10f;
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data", order = 51)]
public class WeaponData : ScriptableObject
{
    public enum ATTACK_MODE { MELEE, FIREARMS }

    [Header("Setup")] 
    public string WeaponName = "Unknown Weapon";
    [SerializeField] private ATTACK_MODE _attackMode = ATTACK_MODE.FIREARMS;
    [SerializeField] private float _attackRange = 3.5f;
    [SerializeField] private float _attackDelay = 0.5f;
    [SerializeField] private Vector2 _accuracySpread = new Vector2(-2, 2); // разброс
    [field: SerializeField] public GameObject OnAttackFX { get; set; }

    #region getters

    public ATTACK_MODE AttackMode => _attackMode;
    
    public float AttackRange => _attackRange;
    public float AttackDelay => _attackDelay;
    public Vector2 AccuracySpread => _accuracySpread;

    #endregion
}

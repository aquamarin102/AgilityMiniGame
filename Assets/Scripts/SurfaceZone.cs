using UnityEngine;

public enum SurfaceEffectType
{
    WineSlow,
    GreaseSlip,
    FoamSlip,
    HotTeaDamage
}

[RequireComponent(typeof(Collider))]
public class SurfaceZone : MonoBehaviour
{
    public SurfaceEffectType effectType;
    public float lifetime = 5f;

    [Header("Slow/Slip")]
    public float speedMultiplier = 0.7f;
    public float controlMultiplier = 0.75f;

    [Header("HotTea")]
    public float damageTickSeconds = 0.5f;
    public int damagePerTick = 1;

    private float _nextTick;

    private void Reset()
    {
        var c = GetComponent<Collider>();
        if (c) c.isTrigger = true;
    }

    private void Start()
    {
        if (lifetime > 0f) Destroy(gameObject, lifetime);
    }

    private void OnTriggerStay(Collider other)
    {
        var mods = other.GetComponentInParent<MovementModifiers>();
        var hp = other.GetComponentInParent<PlayerHealth>();

        switch (effectType)
        {
            case SurfaceEffectType.WineSlow:
                if (mods) mods.ApplySpeedMultiplier(speedMultiplier, 0.2f);
                break;

            case SurfaceEffectType.GreaseSlip:
            case SurfaceEffectType.FoamSlip:
                if (mods) mods.ApplyControlMultiplier(controlMultiplier, 0.2f);
                break;

            case SurfaceEffectType.HotTeaDamage:
                if (hp == null) break;
                if (Time.time >= _nextTick)
                {
                    _nextTick = Time.time + damageTickSeconds;
                    hp.TakeDamage(damagePerTick);
                }
                break;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [Header("Stats")]
    public WeaponStats stats;

    [SerializeField] new Collider collider;
    int damage;
    string enemyTag;

    public void Start()
    {
        if(transform.root == gameObject)
            GameManager.instance.EndRound += Destroy;
    }

    public void Init(float strength, string enemyTag)
    {
        GameManager.instance.EndRound -= Destroy;
        damage = Mathf.RoundToInt(strength * stats.damageMultiplier);
        this.enemyTag = enemyTag;
    }

    public void Attack()
    {
        if(!collider.enabled)
            StartCoroutine(EnableCollider());
    }

    public void SetDamage(float damage)
    {
        damage = Mathf.RoundToInt(damage * stats.damageMultiplier);
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(stats.attackPeriod.x);
        Debug.Log("Enabled");
        collider.enabled = true;
        yield return new WaitForSeconds(stats.attackPeriod.y - stats.attackPeriod.x);
        collider.enabled = false;
        Debug.Log("Disabled");
    }

    private void Destroy()
    {
        GameManager.instance.EndRound -= Destroy;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.collider.CompareTag(enemyTag))
        {
            collision.collider.GetComponent<Character>().Damage(damage, 0);
            collider.enabled = false;
            StopAllCoroutines();
            Debug.Log("Check");
        }
    }

}

[System.Serializable]
public struct WeaponStats
{
    [Header("Stats")]
    public int damageMultiplier;
    public int range;

    [Header("Animation")]
    public string animationName;
    public float animationLength;
    public Vector2 attackPeriod;

}

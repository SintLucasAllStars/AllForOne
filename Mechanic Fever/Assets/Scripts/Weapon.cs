using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public WeaponStats stats;

    [SerializeField] new Collider collider;
    [SerializeField] Collider triger;
    int damage;
    string enemyTag;

    [SerializeField] Vector3 position;
    [SerializeField] Vector3 rotation;

    public void Start()
    {
        if(transform.root == transform)
            GameManager.instance.EndRound += Destroy;
    }

    public void Init(float strength, string enemyTag, Transform parent)
    {
        GameManager.instance.EndRound -= Destroy;
        damage = Mathf.RoundToInt(strength * stats.damageMultiplier);
        this.enemyTag = enemyTag;

        transform.parent = parent;
        transform.localPosition = position;
        transform.localRotation = Quaternion.Euler(rotation);


        if(triger != null)
            Destroy(triger);
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
        Debug.Log("Bye");
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
    public int damageMultiplier;

    [Header("Animation")]
    public string animationName;
    public Vector2 attackPeriod;

}

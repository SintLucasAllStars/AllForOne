using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableSpawning : MonoBehaviour
{
    public GameObject[] weaponPrefab;
    public GameObject[] powerUpPrefab;

    public Vector3 spawnOffSet;

    private NavMeshHit hit;

    private void Start()
    {
        Init();
        GameManager.gameManager.OnReset.AddListener(Init);
    }

    private void Init()
    {
        GameManager.gameManager.setItemSpawner(this);
    }

    public Vector3 getSpawnPosition()
    {
        float x = GameManager.gameManager.fieldSize.x / 2;
        float y = GameManager.gameManager.fieldSize.y / 2;
        Vector3 spawnPosition = new Vector3(Random.Range(-x, x + 1), 0, Random.Range(-y, y + 1));
        //Debug.DrawRay(spawnPosition, Vector3.up * 10, Color.green, 10000000);
        if (!NavMesh.SamplePosition(spawnPosition, out hit, 1f, NavMesh.AllAreas))
        {
            NavMesh.SamplePosition(spawnPosition, out hit, 10f, NavMesh.AllAreas);
        }
        return hit.position;
    }

    public void spawnWeapon()
    {
        Instantiate(weaponPrefab[Random.Range(0, weaponPrefab.Length)], getSpawnPosition() + spawnOffSet, Quaternion.identity);
    }

    public void spawnPowerUp()
    {
        Instantiate(powerUpPrefab[Random.Range(0, powerUpPrefab.Length)], getSpawnPosition() + spawnOffSet, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (GameManager.gameManager != null)
        {
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(GameManager.gameManager.fieldSize.x * 2, 1, GameManager.gameManager.fieldSize.y * 2));
        }
    }
}

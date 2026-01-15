using UnityEngine;

// Moștenim BossHealth pentru a fi recunoscuți de PlayerBullet fără modificări
public class SpawnerBoss : BossHealth 
{
    [Header("Setări Spawn & Rage")]
    public GameObject minionPrefab;
    public float spawnInterval = 4f; 
    private float nextSpawnTime;

    [Header("Limite Hartă")]
    public float minX; public float maxX;
    public float minY; public float maxY;

    protected override void Start()
    {
        base.Start(); // Execută setările de bază (HP, Slider) din BossHealth
    }

    void Update()
    {
        // RAGE MODE: Dacă viața e sub 50%, spawnăm de 2 ori mai repede
        float actualInterval = (currentHealth <= maxHealth * 0.5f) ? spawnInterval / 2f : spawnInterval;

        if (Time.time >= nextSpawnTime)
        {
            SpawnAtRandom();
            nextSpawnTime = Time.time + actualInterval;
        }
    }

    void SpawnAtRandom()
    {
        Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        Instantiate(minionPrefab, randomPos, Quaternion.identity);
    }

    // Suprascriem funcția de damage pentru a adăuga scutul de minioni
    public override void TakeDamage(int damage)
    {
        // Căutăm dacă mai există minioni pe mapă
        RushMinion[] activeMinions = Object.FindObjectsByType<RushMinion>(FindObjectsSortMode.None);

        if (activeMinions.Length > 0)
        {
            Debug.Log("Scut activ! Mai sunt " + activeMinions.Length + " minioni.");
            return; // Glonțul lovește, dar nu scade viața
        }

        // Dacă nu sunt minioni, apelăm logica normală de damage din scriptul părinte
        base.TakeDamage(damage); 
    }
}
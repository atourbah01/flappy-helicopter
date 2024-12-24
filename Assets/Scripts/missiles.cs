using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public Sprite missileSprite;
    public float respawnXPositionLeft = -10.0f;
    public float respawnXPositionRight = 10.0f;
    public float respawnYMin = -5.0f;
    public float respawnYMax = 5.0f;
    public float destroyXPosition = -10.0f;
    public float missileSpeed = 5.0f;
    public float launchIntervalMin = 1.0f;
    public float launchIntervalMax = 5.0f;

    private float nextLaunchTime = 3.0f;

    void Update()
    {
        if (Time.time >= nextLaunchTime)
        {
            LaunchMissile();
            nextLaunchTime = Time.time + Random.Range(launchIntervalMin, launchIntervalMax);
        }
    }

    void LaunchMissile()
    {
        // Check if there's already a missile on the left side
        if (!IsMissileActiveOnLeft())
        {
            SpawnMissile(respawnXPositionLeft, Random.Range(respawnYMin, respawnYMax));
        }
        else if (!IsMissileActiveOnRight())
        {
            SpawnMissile(respawnXPositionRight, Random.Range(respawnYMin, respawnYMax));
        }
    }

    bool IsMissileActiveOnLeft()
    {
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");
        foreach (GameObject missile in missiles)
        {
            if (missile.transform.position.x <= respawnXPositionLeft)
            {
                return true;
            }
        }
        return false;
    }

    bool IsMissileActiveOnRight()
    {
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");
        foreach (GameObject missile in missiles)
        {
            if (missile.transform.position.x >= respawnXPositionRight)
            {
                return true;
            }
        }
        return false;
    }

    void SpawnMissile(float respawnXPosition, float respawnYPosition)
    {
        // Instantiate a new missile
        GameObject missile = new GameObject("Missile");
        SpriteRenderer renderer = missile.AddComponent<SpriteRenderer>();
        renderer.sprite = missileSprite;
        missile.transform.position = new Vector2(respawnXPosition, respawnYPosition);
        Rigidbody2D rb = missile.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = new Vector2((respawnXPosition == respawnXPositionLeft) ? missileSpeed : -missileSpeed, 0);
    }
}
// Attach this script to your enemy object
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    public AudioClip enemySound; // Assign your audio clip here in Inspector
    public float minDistance = 1f; // Minimum distance to hear sound
    public float maxDistance = 5f; // Maximum distance to hear sound
    Transform player;

    private AudioSource audioSource;
    private AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerMovement>().transform;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = enemySound;
        audioSource.spatialBlend = 1; // To make the sound 3D
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
    }

    void Update()
    {
        // Play sound when player is within the hearing radius
        if (Vector2.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.loop = true;
                EventManager.Instance.OnPlayerEnterReaperRadius();

                audioSource.Play();
            }

            backgroundMusic.mute = true;
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                EventManager.Instance.OnPlayerLeaveReaperRadius();
            }
            backgroundMusic.mute = false;
        }
    }
}

using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemReferences item;

    public bool IsCollected;

    [SerializeField]
    private ParticleSystem pickParticleEffect;

    [SerializeField]
    private AudioSource audioSource;

    public event Action<Item> onItemPickup;


    private void OnCollisionEnter(Collision collision)
    {
        IsCollected = true;

        if (pickParticleEffect != null)
        {
            pickParticleEffect.Play();
        }    
        
        if (audioSource != null)
        {
            audioSource.Play();
        }

        onItemPickup?.Invoke(this);
        gameObject.SetActive(false);
    }
}

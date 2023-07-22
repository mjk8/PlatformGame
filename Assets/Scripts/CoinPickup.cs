using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSFX;
    private bool collected = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")&&!collected)
        {
            collected = true;
            FindObjectOfType<GameSession>().CoinGettoDaze();
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
            //GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}

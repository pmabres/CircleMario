using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hongo : MonoBehaviour
{
    private String playerTag = "Player";
    private bool destroying;
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(playerTag) && GameManager.ActiveGameState == GameManager.GameState.Running && !destroying)
        {
            var originalBulletSpeed = GameManager.DifficultyHelper.GetBulletSpeed();
            destroying = true;
            GameManager.DifficultyHelper.BulletSpeed = 0.5f;
            Destroy(gameObject);
            StartCoroutine(RestoreSpeed(3, originalBulletSpeed));
        }
    }
    IEnumerator RestoreSpeed(float seconds, float originalSpeed)
    {
        yield return new WaitForSeconds(seconds);
        
        GameManager.DifficultyHelper.BulletSpeed = originalSpeed;
    }
}

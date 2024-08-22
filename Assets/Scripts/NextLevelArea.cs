using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Singleton.LoadNextLevel();
        }
    }
}

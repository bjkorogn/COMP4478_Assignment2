using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void Setup() {
        // Set game over screen to active
        gameObject.SetActive(true);
    }

    public void RestartButton() {
        // Reload main scene
        SceneManager.LoadScene("MainScene");
    }
}

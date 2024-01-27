using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution(1920, 1080, true);
    }
    public void BeginGame()
    {
        SceneManager.LoadScene("Scenes/GameplayScene");
    }
}

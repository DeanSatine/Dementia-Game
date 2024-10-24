using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void SetScene(string NextScene) {//Load the next scene
        SceneManager.LoadScene(NextScene);
    }

    public void EndGame() {
        Application.Quit();
    }

}

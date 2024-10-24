using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int WrongObjects = 0;//Number of incorrectly positioned objects (set automaticaly)
    [SerializeField] int MaxGuesses = 3;//Maximum guesses
    [SerializeField] string NextScene;//Next level scene
    [SerializeField] Image FadeImage;//Reference to fade image
    [SerializeField] float FadeSpeed = 0.4f;//Fade % per second
    private bool Fade = false;//Start fading
    private bool Lose = false;//Game lost
    private int WrongGuesses = 0;//Number of incorrect guesses

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Fade) FadeImage.color = new Color(0, 0, 0, FadeImage.color.a + FadeSpeed * Time.deltaTime);//If fading, increase fade image alpha
        else FadeImage.color = new Color(0, 0, 0, FadeImage.color.a - FadeSpeed * Time.deltaTime);//Decrease fade image alpha

        if (FadeImage.color.a >= 1) {//When fading is over

            //Activate win or lose functions
            if (Lose) Debug.Log("Game Over");//Reset game
            else SetNextScene();//SetNextScene();

        }
    }

    public void Guess(bool correct) {//Object was guessed
        
        if (correct) {//If object is displaced
            WrongObjects -= 1;//Decrease displaced object counter
        }
        else {
            WrongGuesses += 1;//Increase wrong guess counter

            if (WrongGuesses >= MaxGuesses) LoseGame();//If wrong guesses exceeds max allowed, lose game
    
        }


    }

    public void End() {//End level
        Debug.Log("End");
        if (WrongObjects > 0) Lose = false;//Determine if all displaced objects are found
        Fade = true;//Start fading
        FadeImage.color = new Color(0, 0, 0, 0.05f);
    }

    private void LoseGame() {//Force game to end
        Fade = true;
        Lose = true;
    }

    private void SetNextScene() {//Load the next scene
        SceneManager.LoadScene(NextScene);
    }

}

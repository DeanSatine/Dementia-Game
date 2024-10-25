using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private AudioSource audio;
    [HideInInspector] public int WrongObjects = 0;//Number of incorrectly positioned objects (set automaticaly)
    [SerializeField] int MaxGuesses = 3;//Maximum guesses
    [SerializeField] string NextScene;//Next level scene
    [SerializeField] Image FadeImage;//Reference to fade image
    [SerializeField] float FadeSpeed = 0.4f;//Fade % per second
    private bool Fade = false;//Start fading
    private bool Lose = false;//Game lost
    private int WrongGuesses = 0;//Number of incorrect guesses
    [SerializeField] AudioClip GoodSound;
    [SerializeField] AudioClip BadSound;

    [SerializeField] bool displayCount = true;
    [SerializeField] Text CountText;
    [SerializeField] Text GuessText;
    public Text ObjectText;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        FadeImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float FadeAmount = Mathf.Clamp(FadeSpeed * Time.deltaTime, 0.001f, 1);
        if (Fade) FadeImage.color = new Color(0, 0, 0, Mathf.Clamp(FadeImage.color.a + FadeAmount, 0, 1));//If fading, increase fade image alpha
        else FadeImage.color = new Color(0, 0, 0, Mathf.Clamp(FadeImage.color.a - FadeAmount, 0, 1));//Decrease fade image alpha

        
        if (FadeImage.color.a >= 1) {//When fading is over

            //Activate win or lose functions
            if (Lose) SceneManager.LoadScene("FailureUI");//Reset game
            else SetNextScene();//SetNextScene();

        }

        if (displayCount) CountText.text = WrongObjects.ToString();
        else CountText.text = "???";

    }

    public void Guess(bool correct) {//Object was guessed
        
        if (correct) {//If object is displaced
            WrongObjects -= 1;//Decrease displaced object counter

            audio.clip = GoodSound;

        }
        else {
            WrongGuesses += 1;//Increase wrong guess counter
            GuessText.text += "X";

            if (WrongGuesses >= MaxGuesses) LoseGame();//If wrong guesses exceeds max allowed, lose game
            
            audio.clip = BadSound;

        }

        audio.Play();

    }

    public void End() {//End level
        Debug.Log("End");
        if (WrongObjects > 0) Lose = false;//Determine if all displaced objects are found
        Fade = true;//Start fading
    }

    private void LoseGame() {//Force game to end
        Fade = true;
        Lose = true;
    }

    private void SetNextScene() {//Load the next scene
        Debug.Log("Success");
        SceneManager.LoadScene(NextScene);
    }

}

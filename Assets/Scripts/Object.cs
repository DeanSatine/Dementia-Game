using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Object : MonoBehaviour
{
    [Header("Displacement")]
    [SerializeField] private bool Wrong = false;//Is this a displaced object
    [SerializeField] private GameManager Manager;//Game manager reference
    [SerializeField] private Vector3 CorrectPos = Vector3.zero;//Correct Position
    [SerializeField] private Vector3 CorrectRot = Vector3.zero;//Correct Rotation
    [SerializeField] private Vector3 CorrectSca = Vector3.one;//Correct Scale
    [SerializeField] private string CorrectText = "name";

    [Header("Extra")]
    [SerializeField] private bool isExit = false;//Is this the room exit (does it end the scene)
    [SerializeField] private bool DisableSelf = false;//Disable self when selected
    [SerializeField] private GameObject[] Activate = new GameObject[0];//Objects to activate
    [SerializeField] private GameObject[] Deactivate = new GameObject[0];//Objects to deactivate
    [SerializeField] private AudioClip Sound = null;//Sound to play
    [SerializeField] public string DisplayText = "name";//Text to display

    private bool selected = false;//Has this object been selected

    void Start() {
        if (Wrong) Manager.WrongObjects += 1;//If this object is displaced, increase wrong objects in game
        if (DisplayText == "name") DisplayText = gameObject.name;//If no display text is set, set display text to name
        if (CorrectText == "name") CorrectText = gameObject.name;//Setup Correct Text
        if (CorrectPos == Vector3.zero) CorrectPos = transform.position;//Setup corect transform
        if (CorrectRot == Vector3.zero) CorrectRot = transform.rotation.eulerAngles;
    }

    public void Select()//Advance the game
    {

        if (Sound != null) {//Play sound
            GetComponent<AudioSource>().clip = Sound;
            GetComponent<AudioSource>().Play();
        }

        if (isExit) {//If marked as the level exit, end the level
            Manager.End();
            return;
        }

        if (selected) return;//If already selected, return
        else selected = true;//Otherwise set as selected

        if (Activate.Length > 0)//Activate listed objects
        {
            for (int i = 0; i < Activate.Length; i++)
            {
                Activate[i].SetActive(true);
            }
        }

        if (Deactivate.Length > 0)//Deactivate listed objects
        {
            for (int i = 0;i < Deactivate.Length; i++)
            {
                Deactivate[i].SetActive(false);
            }
        }

        Manager.Guess(Wrong);//Trigger game manager guess based on Wrong

        if (Wrong) {//If object is worng, fix its position
            transform.position = CorrectPos;
            
            transform.localScale = CorrectSca;

            transform.rotation = Quaternion.identity;
            transform.Rotate(CorrectRot);

            DisplayText = CorrectText;

        }

        gameObject.SetActive(!DisableSelf);

    }

}

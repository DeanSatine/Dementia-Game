using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private AudioSource Audio;
    [SerializeField] private bool DisableSelf = true;
    [SerializeField] private GameObject[] Activate = new GameObject[0];//Objects to activate
    [SerializeField] private GameObject[] Deactivate = new GameObject[0];//Objects to deactivate
    [SerializeField] private AudioClip Sound;//Sound to play
    [SerializeField] string Text = "";//Text to display

    public void Collect()//Advance the game
    {
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

        //Display Text

        Audio.clip = Sound;
        Audio.Play();

        gameObject.SetActive(!DisableSelf);

    }

}

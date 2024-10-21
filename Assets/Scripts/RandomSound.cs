using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [SerializeField] AudioSource Source;
    [SerializeField] AudioClip[] Sounds;
    [SerializeField, Range(0, 60)] float MaxInterval = 60;
    [SerializeField, Range(0, 60)] float MinInterval = 0;
    float timer = 0.0f;

    void Start()
    {
        timer = Random.Range(MinInterval, MaxInterval);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && Sounds.Length > 0) Play();

    }

    void Play() {

        Source.clip = Sounds[Random.Range(0, Sounds.Length - 1)];
        Source.Play();

        timer = Random.Range(MinInterval, MaxInterval);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // subscribe
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //unsubscribe
    }
    // This is the new OnLevelWasLoaded method. You may name it as you want
    // Make sure to subscribe/unsubscribe the correct method name (see above)
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log(scene.name);
        music.Stop();

        if (scene.name == "Start")
        {
            music.clip = startClip;
            music.volume = .75f;

            music.Play();
        }

        else if (scene.name == "Game")
        {
            music.clip = gameClip;
            music.volume = .25f;

            music.Play();
        }

        else if (scene.name == "GameOver")
        {
            music.clip = startClip;
            music.volume = .75f;

            music.Play();
        }
    }
}

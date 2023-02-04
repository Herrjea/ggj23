using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
	[SerializeField] AudioSource EffectsSource;
	[SerializeField] AudioSource MusicSource;

	[HideInInspector] public static SoundManager Instance = null;

	AudioClip wrongPress;
	AudioClip rightPress;
	AudioClip wordCompleted;
	AudioClip win;

	string sfxFileExtension = ".m4a";


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		wrongPress = Resources.Load<AudioClip>("WrongPress" + sfxFileExtension);
		rightPress = Resources.Load<AudioClip>("RightPress" + sfxFileExtension);
		wordCompleted = Resources.Load<AudioClip>("WordCompleted" + sfxFileExtension);
		win = Resources.Load<AudioClip>("Win" + sfxFileExtension);

		GameEvents.P1WrongPress.AddListener(WrongPress);
		GameEvents.P1RightPress.AddListener(RightPress);
		GameEvents.P1WordCompleted.AddListener(WordCompleted);
		GameEvents.P1Wins.AddListener(Win);
	}


	public void PlaySfx(AudioClip clip)
	{
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}


	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();

		print("playing " + clip.name);
	}


	void WrongPress(int position)
	{
		PlaySfx(wrongPress);
	}

	void RightPress(int position)
	{
		PlaySfx(rightPress);
	}

	void WordCompleted()
    {
		PlaySfx(wordCompleted);
    }

	void Win()
	{
		PlaySfx(win);
	}
}

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

		wrongPress = Resources.Load<AudioClip>("SFX/WrongPress");
		if (wrongPress == null)
			print("Audio clip not found: wrongPress");

		rightPress = Resources.Load<AudioClip>("SFX/RightPress");
		if (rightPress == null)
			print("Audio clip not found: rightPress");

		wordCompleted = Resources.Load<AudioClip>("SFX/WordCompleted");
		if (wordCompleted == null)
			print("Audio clip not found: wordCompleted");

		win = Resources.Load<AudioClip>("SFX/Win");
		if (win == null)
			print("Audio clip not found: win");

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

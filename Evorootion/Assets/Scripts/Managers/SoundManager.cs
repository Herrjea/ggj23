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

	AudioClip mainTheme;


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
			print("Audio clip not found: WrongPress");

		rightPress = Resources.Load<AudioClip>("SFX/RightPress");
		if (rightPress == null)
			print("Audio clip not found: RightPress");

		wordCompleted = Resources.Load<AudioClip>("SFX/WordCompleted");
		if (wordCompleted == null)
			print("Audio clip not found: WordCompleted");

		win = Resources.Load<AudioClip>("SFX/Win");
		if (win == null)
			print("Audio clip not found: Win");

		mainTheme = Resources.Load<AudioClip>("SFX/MainTheme");
		if (mainTheme == null)
			print("Audio clip not found: MainTheme");

		GameEvents.P1WrongPress.AddListener(WrongPress);
		GameEvents.P1RightPress.AddListener(RightPress);
		GameEvents.P1NewOwnBasicWord.AddListener(WordCompleted);
		GameEvents.P1NewEnemyBasicWord.AddListener(WordCompleted);
		GameEvents.P2NewOwnBasicWord.AddListener(WordCompleted);
		GameEvents.P2NewEnemyBasicWord.AddListener(WordCompleted);
		GameEvents.P1Wins.AddListener(Win);

		PlayMusic(mainTheme);
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

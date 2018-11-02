using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu (menuName = "Audio/Audio Clip")]
public class AudioSO : ScriptableObject {
	[SerializeField] protected AudioClip audioClip;
	[Range (0, 1)][SerializeField] protected float volume = 1.0f;

	[SerializeField] protected bool Loop = false;

	[SerializeField] protected bool UseOriginal = true;
	[SerializeField] protected float startTime;
	[SerializeField] protected float endTime;

	[SerializeField] protected bool Spartial = true;

	[SerializeField] protected AudioMixerGroup audioMixer;

	[SerializeField] protected bool fadeOut;
	[SerializeField] protected float fadeTime;

	protected AudioSource audioSource;
	protected bool isFading = false;
	protected ScriptableObjectMono soMono;
	protected Coroutine fadeout;

	public AudioSO Initialize (GameObject target) {
		AudioSO clone = Object.Instantiate (this);
		clone.cloneInitialize (target);
		return clone;
	}

	protected void cloneInitialize (GameObject target) {
		if (audioSource == null) {
			audioSource = target.AddComponent<AudioSource> ();

			audioSource.clip = audioClip;
			audioSource.volume = volume;
			audioSource.playOnAwake = false;
			audioSource.time = startTime;
			audioSource.loop = Loop;
			audioSource.outputAudioMixerGroup = audioMixer;

			if (Spartial)
				audioSource.spatialBlend = 1.0f;

			if (UseOriginal)
				endTime = audioClip.length;
		}

		soMono = FindObjectOfType<ScriptableObjectMono> ();
	}

	public virtual void Update () {
		//add update code here and place 
		//this in monobehaviour update
		//use for checking

		checkAudioTime ();
	}

	public virtual void Play () {
		if (audioSource.time < startTime)
			audioSource.time = startTime;

		if (!audioSource.isPlaying && !isFading) {
			audioSource.volume = volume;
			audioSource.Play ();
		}
	}

	public virtual void Stop () {
		if (fadeOut)
			fadeout = soMono.RunCoroutine (FadeOut ());
		else {
			audioSource.Stop ();
			audioSource.time = startTime;
		}

	}

	public bool audioIsPlaying () {
		return audioSource.isPlaying;
	}

	protected void checkAudioTime () {
		if (audioSource.time >= endTime) {
			if (!Loop) {
				if (fadeOut) {
					fadeout = soMono.RunCoroutine (FadeOut ());
				} else {
					audioSource.Stop ();
					audioSource.time = startTime;
				}
			} else {
				audioSource.time = startTime;
			}
		}
	}

	protected void bypassEffects (bool bypass) {
		if (bypass)
			audioSource.bypassEffects = true;
		else
			audioSource.bypassEffects = false;
	}

	void OnDisable () {
		audioSource = null;
	}

	private IEnumerator FadeOut () {
		isFading = true;
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * fadeTime;

			yield return null;
		}

		isFading = false;

		if (audioSource.volume <= 0) {
			audioSource.Stop ();
			audioSource.time = startTime;
			if (fadeout != null)
				soMono.EndCoroutine (fadeout);
		}
	}
}
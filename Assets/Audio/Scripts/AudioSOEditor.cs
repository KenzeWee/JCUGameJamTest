using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (AudioSO))]
[CanEditMultipleObjects]
public class AudioSOEditor : Editor {

	private AudioSO audioSO = null;
	private SerializedObject serializedObj;

	SerializedProperty audioClip;
	SerializedProperty volume;
	private float sliderVolume = 1.0f;

	SerializedProperty loop;
	private bool loopToggle;

	SerializedProperty useOriginal;
	private bool useOriginalToggle;
	SerializedProperty startTime;
	SerializedProperty endTime;

	SerializedProperty spartial;
	private bool spartialToggle;

	SerializedProperty audioMixer;

	SerializedProperty useFadeOut;
	private bool fadeOutToggle;
	SerializedProperty fadeoutTime;

	private void OnEnable () {
		audioSO = (AudioSO) target;
		serializedObj = new SerializedObject (audioSO);

		useOriginal = serializedObj.FindProperty ("UseOriginal");
		useOriginalToggle = useOriginal.boolValue;

		audioClip = serializedObj.FindProperty ("audioClip");
		volume = serializedObj.FindProperty ("volume");
		sliderVolume = volume.floatValue;

		loop = serializedObj.FindProperty ("Loop");
		loopToggle = loop.boolValue;

		startTime = serializedObj.FindProperty ("startTime");
		endTime = serializedObj.FindProperty ("endTime");

		spartial = serializedObj.FindProperty ("Spartial");
		spartialToggle = spartial.boolValue;

		audioMixer = serializedObj.FindProperty ("audioMixer");

		useFadeOut = serializedObj.FindProperty ("fadeOut");
		fadeOutToggle = useFadeOut.boolValue;
		fadeoutTime = serializedObj.FindProperty ("fadeTime");
	}

	public override void OnInspectorGUI () {
		serializedObj.Update ();

		GUILayout.BeginVertical ();

		EditorGUILayout.LabelField ("AUDIO SO: Instructions on how to use", EditorStyles.boldLabel);
		EditorGUILayout.HelpBox ("In the script holding the reference, reassign the variable holding the reference to the reference initialized (e.g. audioSO = audioSO.Initialize(gameObject); " +
			"Call audioSo.Play to play audio and place audioSO.Update() in update" +
			"Call audioIsPlaying to get a bool if the audio is playing.", MessageType.Info);

		GUILayout.Space (10);

		EditorGUILayout.LabelField ("Audio Clip and Volume", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField (audioClip, new GUIContent ("Audio Clip"));
		sliderVolume = EditorGUILayout.Slider ("Volume", sliderVolume, 0.0f, 1.0f);
		volume.floatValue = sliderVolume;

		GUILayout.Space (10);

		EditorGUILayout.LabelField ("Loop", EditorStyles.boldLabel);

		GUILayout.BeginHorizontal ();

		GUILayout.Label ("Loop", GUILayout.MinWidth (112));
		loopToggle = EditorGUILayout.Toggle (loopToggle);
		loop.boolValue = loopToggle;

		GUILayout.EndHorizontal ();

		GUILayout.Space (10);

		EditorGUILayout.LabelField ("Use audio clip length or use custom start and end time", EditorStyles.boldLabel);

		GUILayout.EndVertical ();

		GUILayout.BeginHorizontal ();

		GUILayout.Label ("Use Original Time", GUILayout.MinWidth (112));
		useOriginalToggle = EditorGUILayout.Toggle (useOriginalToggle);
		useOriginal.boolValue = useOriginalToggle;

		GUILayout.EndHorizontal ();

		if (!useOriginalToggle) {
			GUILayout.BeginVertical ();

			EditorGUILayout.PropertyField (startTime, new GUIContent ("Start Time"));
			EditorGUILayout.PropertyField (endTime, new GUIContent ("End Time"));

			GUILayout.EndVertical ();
		}

		GUILayout.BeginVertical ();

		GUILayout.Space (10);

		EditorGUILayout.LabelField ("Use 2D or 3D audio", EditorStyles.boldLabel);

		GUILayout.EndVertical ();

		GUILayout.BeginHorizontal ();

		GUILayout.Label ("Spartial Audio", GUILayout.MinWidth (112));
		spartialToggle = EditorGUILayout.Toggle (spartialToggle);
		spartial.boolValue = spartialToggle;

		GUILayout.EndHorizontal ();

		GUILayout.BeginVertical ();

		GUILayout.Space (10);
		EditorGUILayout.LabelField ("Audio Mixer", EditorStyles.boldLabel);

		EditorGUILayout.PropertyField (audioMixer, new GUIContent ("Audio Mixer"));

		GUILayout.Space (10);
		EditorGUILayout.LabelField ("Use Fade Out", EditorStyles.boldLabel);

		GUILayout.BeginHorizontal ();

		GUILayout.Label ("Fade Out", GUILayout.MinWidth (112));
		fadeOutToggle = EditorGUILayout.Toggle (fadeOutToggle);
		useFadeOut.boolValue = fadeOutToggle;

		GUILayout.EndHorizontal ();

		if (fadeOutToggle) {
			GUILayout.BeginVertical ();

			EditorGUILayout.PropertyField (fadeoutTime, new GUIContent ("Fade Out Time"));

			GUILayout.EndVertical ();
		}

		GUILayout.EndVertical ();

		serializedObj.ApplyModifiedProperties ();
	}
}
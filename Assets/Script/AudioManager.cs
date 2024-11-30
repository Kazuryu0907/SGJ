using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // private AudioSource[] audioSources = new AudioSource[20];

    [SerializeField] private AudioClip audioRepel;
    [SerializeField] private AudioClip audioAttract;
    private string attractedObjectName = "";
    private string repeledObjectName = "";
    [SerializeField] private AudioClip audioBiribiri;
    [SerializeField] private AudioClip audioGoal;
    private AudioSource audioSource = default;
    private void Awake(){
        audioSource = gameObject.AddComponent<AudioSource>();
        // for(int i = 0; i < audioSources.Length; i++){
        //     audioSources[i] = gameObject.AddComponent<AudioSource>();
        // }
    }
    // private AudioSource getUnusedAudioSource(){
    //     for(int i = 0; i < audioSources.Length; i++){
    //         if(!audioSources[i].isPlaying){
    //             return audioSources[i];
    //         }
    //     }
    //     return null;
    // }
    public void play(AudioClip clip){
        // if(audioSource == null)return;
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
    }
    public void playRepel(string objectName){
        if(repeledObjectName == objectName)return;
        play(audioRepel);
        repeledObjectName = objectName;
    }
    public void playAttract(string objectName){
        if(attractedObjectName == objectName)return;
        play(audioAttract);
    }
    public void playBiribiri(){
        // Debug.Log("Biribiri");
        play(audioBiribiri);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioOnClick : MonoBehaviour
{
    //public AudioSource AudioSource;
    //public AudioClip buttonPresed;
    //public AudioClip buttonPointer;
    // Llamamos a esta función para cambiar de escena
    public void PlayClip()
    {
        AudioManager.Instance.PlaySFX("ClipButton");
        //AudioSource.PlayOneShot(buttonPresed);
    }

    public void ClipPointerPlay()
    {
        //AudioSource.PlayOneShot(buttonPointer);
    }
}


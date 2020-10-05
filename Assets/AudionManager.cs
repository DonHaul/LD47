using UnityEngine;

[System.Serializable]public class Sound
{
    public string name;
    public AudioClip file;

    public float volume = 0.7f;
    public float pitch = 1f;
    public AudioSource source;

    public void SetSource(AudioSource _source)
    {

        source = _source;
        source.clip = file;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}


public class AudionManager : MonoBehaviour
{

    public static AudionManager instance;
    public Sound[] sounds;
    public void Awake()
    {
        if (instance != null)
            Debug.LogError("More than 1 audion manager in the scene");
        else
        instance = this;    
    
        //DontDestroyOnLoad(gameObject);


        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i.ToString() + "_" + sounds[i].name);
           // DontDestroyOnLoad(_go);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string soundname)
    {
        for (int i = 0; i < sounds.Length; i++)
        {

            if(sounds[i].name==soundname)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.Log("Sound not found");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

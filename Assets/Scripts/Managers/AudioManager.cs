using UnityEngine;

public enum AudioClipType{grabClip,shopClip}
public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;
   [SerializeField] private AudioSource _audioSource;
   public AudioClip GrabClip, ShopClip;

   private void Awake()
   {
      if (Instance!=null&&Instance!=this)
      {
         Destroy(gameObject);
         return;
      }

      Instance = this;
   }


   public void PlayAudio(AudioClipType clipType)
   {
      if (_audioSource!=null)
      {
         AudioClip audioClip = null;
         if (clipType==AudioClipType.grabClip)
         {
            audioClip = GrabClip;
         }
         else if (clipType==AudioClipType.shopClip)
         {
            audioClip = ShopClip;
         }
         _audioSource.PlayOneShot(audioClip,0.5f);
      }
      
      
   }
}

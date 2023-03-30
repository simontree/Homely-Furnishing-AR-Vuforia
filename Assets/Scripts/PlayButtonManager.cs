using UnityEngine;
using UnityEngine.Video;

public class PlayButtonManager : MonoBehaviour
{
    public void PlayVideo()
    {
        transform.parent.gameObject.GetComponent<VideoPlayer>().Play();
        gameObject.SetActive(false);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class IntroController : MonoBehaviour
{

    private VideoPlayer introVideo;
    private AsyncOperation async;

    private void Start()
    {
        introVideo = GetComponent<VideoPlayer>();
        StartCoroutine(Wait(introVideo.clip.length));

        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }

    private IEnumerator Wait(double duration)
    {
        yield return new WaitForSeconds((float)duration);
        async.allowSceneActivation = true;
    }

    private void Update()
    {
        if (Input.anyKey)
            async.allowSceneActivation = true;
    }

}
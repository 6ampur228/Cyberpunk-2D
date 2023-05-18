using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Door : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private AudioSource _doorOpeningSound;
    [SerializeField] private SceneAsset _nextScene;

    private float _timeLoadScene = 0.7f;

    private void Update()
    {
        if(_text.gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            _text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _text.gameObject.SetActive(false);
        }
    }

    private void OpenDoor()
    {
        _doorOpeningSound.Play();
        StartCoroutine(LoadSceneAfterTime(_timeLoadScene));
    }

    private IEnumerator LoadSceneAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(_nextScene.name);
    }
}

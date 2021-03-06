﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GachaController.Auth;
using Dialog;
using Story;
using UnityEngine.AddressableAssets;

namespace Title
{
    public class TitleController : MonoBehaviour
    {
        [SerializeField] private LoginErrorDialog dialog;

        [SerializeField] private Canvas parent;

        // Start is called before the first frame update
        void Start()
        {
            // new StoryRepository().Insert();
            // new StoryRepository().UpdateFromMasterData();
            // var key    = "http://localhost:8000/TestBackgroundImage.prefab";
            // var handle = Addressables.LoadAssetAsync<GameObject>("Assets/TestBackgroundImage.prefab");
        }
        
        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount != 0 || Input.GetMouseButtonUp(0))
            {
                Debug.Log("touched");
                var handle = Addressables.DownloadDependenciesAsync( "Assets/TestBackgroundImage.prefab", true );
                handle.Completed += _ => StartCoroutine(Login());
            }
        }

        IEnumerator Login()
        {
            var loginService = new LoginService();
            var coroutine = loginService.LogihWithCredential();
            yield return StartCoroutine(coroutine);
            var isSuccess = (bool) coroutine.Current;
            if (isSuccess)
            {
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                ShowLoginFailedDialog();
            }
        }

        void ShowLoginFailedDialog()
        {
            Instantiate(dialog).transform.SetParent(parent.transform, false);
        }
    }
}
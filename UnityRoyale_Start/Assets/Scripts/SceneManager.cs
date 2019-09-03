using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneManager : MonoBehaviour
{
    public string m_SceneAddressToLoad;

    public void LoadGameplayScene()
    {
        // 场景异步加载
        // m_SceneAddressToLoad：场景名称
        // 场景内所有的 Assets 都会被异步加载
        Addressables.LoadSceneAsync(m_SceneAddressToLoad, UnityEngine.SceneManagement.LoadSceneMode.Single).Completed += OnSceneLoaded;
    }

    void OnSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        //Addressables.UnloadSceneAsync(new SceneInstance());
        // LOGIC THAT KICKSTARTS THE GAMEPLAY
    }
}

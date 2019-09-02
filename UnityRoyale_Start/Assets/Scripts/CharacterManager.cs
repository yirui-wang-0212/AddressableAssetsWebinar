using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; //TODO: Mention the use of this namespace
using UnityEngine.ResourceManagement.AsyncOperations; // TODO: Mention that this is needed to do the async operations over the lists?

public class CharacterManager : MonoBehaviour
{
    //public GameObject m_archerObject;

    //public AssetReference m_ArcherObject;

    public List<AssetReference> m_Characters;
    // Assets 是否加载完成，完成后可以准备实例化
    bool m_AssetsReady = false;
    int m_ToLoadCount;
    int m_CharacterIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_ToLoadCount = m_Characters.Count;

        foreach (var character in m_Characters)
        {
            // 异步加载
            // 资源可以在本地也可以在远端服务器
            // 把 character 包资源加载进来，每加载完一个 character，直接执行 OnCharacterAssetLoaded 函数
           character.LoadAssetAsync<GameObject>().Completed += OnCharacterAssetLoaded;
        }
    }

    // 点击卡片事件
    public void SpawnCharacter(int characterType)
    {
        //Instantiate(m_archerObject);

        //m_ArcherObject.InstantiateAsync();

        // 如果全部 load 进来了
        if (m_AssetsReady)
        {
           Vector3 position = Random.insideUnitSphere * 5;
           position.Set(position.x, 0, position.z);
           // 异步实例化
           // 系统不会等待
           // 调用完成时会回来接着运行
           // 大量实例化不会卡住系统
           m_Characters[characterType].InstantiateAsync(position, Quaternion.identity);
        }
    }

    // 每加载完一个 character 执行一次减法
    // 计数角色是否全都 load 进来了
    // 如果全部 load 进来了， m_AssetsReady 设为 true
    void OnCharacterAssetLoaded(AsyncOperationHandle<GameObject> obj)
    {
       m_ToLoadCount--;

       if (m_ToLoadCount <= 0)
           m_AssetsReady = true;
    }

    private void OnDestroy() //TODO: Should we teach instantiate with game objects and then manually release?
    {
       foreach (var character in m_Characters)
       {
           character.ReleaseAsset();
       }
    }
}

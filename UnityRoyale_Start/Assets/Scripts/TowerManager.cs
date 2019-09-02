using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; //TODO: Mention the use of this namespace
using UnityEngine.ResourceManagement.AsyncOperations; // TODO: Mention that this is needed to do the async operations over the lists?
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    public IList<GameObject> m_Towers;

    public AssetLabelReference m_TowerLabel;

    public Button[] m_TowerCards;

    // Start is called before the first frame update
    void Start()
    {
        // 异步加载所有 label 属性是 m_TowerLabel 的所有资源
        // 加载完成后调用 OnResourcesRetrieved 函数
        Addressables.LoadAssetsAsync<GameObject>(m_TowerLabel, null).Completed += OnResourcesRetrieved;
    }

    // 确认加载完成后，将灰色卡片变成彩色卡片
    private void OnResourcesRetrieved(AsyncOperationHandle<IList<GameObject>> obj)
    {
        m_Towers = obj.Result;

        //Activate the tower cards since their assets are now loaded
        foreach(var towerCard in m_TowerCards)
        {
            towerCard.interactable = true;
        }
    }

    public void InstantiateTower(int index)
    {
        if(m_Towers != null)
        {
            Vector3 position = Random.insideUnitSphere * 5;
            position.Set(position.x, 0, position.z);
            Instantiate(m_Towers[index], position, Quaternion.identity, null);
        }
    }
}

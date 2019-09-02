using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class TextureController : MonoBehaviour
{

    public Renderer m_ReferencedMaterial;

    // On CLick 事件
    public void SwitchToHighDef()
    {
        // 加载纹理
        // ArcherSkin：名字（Address），名字可以一样
        // HD：标签
        LoadTexture("ArcherSkin", "Skin2");
    }

    void LoadTexture(string key, string label)
    {
        // 异步加载
        // 加载完成后调用 TextureLoaded
        Addressables.LoadAssetsAsync<Texture2D>(new List<object> { key, label }, null, Addressables.MergeMode.Intersection).Completed
            += TextureLoaded;
    }

    // 更换纹理
    void TextureLoaded(AsyncOperationHandle<IList<Texture2D>> obj)
    {
        m_ReferencedMaterial.material.mainTexture = obj.Result[0];
    }
}

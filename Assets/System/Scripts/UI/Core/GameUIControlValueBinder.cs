using System.Collections.Generic;
using UnityEngine;

/*
* Copyright(c) 2021  mengyu
*
* 模块名：     
* GameUIControlValueBinder.cs
* 
* 用途：
* UI控件数据绑定器。该绑定器用在需要绑定的UI控件上。
*
* 作者：
* mengyu
*/

namespace Ballance2.UI.Core
{
  /// <summary>
  /// UI控件数据绑定器。该绑定器用在需要绑定的UI控件上。
  /// </summary>
  [AddComponentMenu("Ballance/UI/ValueBinder/Custom")]
  [RequireComponent(typeof(RectTransform))]
  public class GameUIControlValueBinder : MonoBehaviour
  {
    /// <summary>
    /// 指定对应UI消息中心名字
    /// </summary>
    public string MessageCenterName = null;

    /// <summary>
    /// 指定绑定器的名称，可在UI消息中心使用该名称查找
    /// </summary>
    public string Name = "";

    public List<GameUIControlValueBinderUserUpdateCallback> UserUpdateCallbacks = new List<GameUIControlValueBinderUserUpdateCallback>();
    public GameUIControlValueBinderSupplierCallback BinderSupplierCallback => OnBinderSupplierHandle;
    public GameUIMessageCenter MessageCenter = null;

    protected virtual bool OnBinderSupplierHandle(object value) {
      return false;
    }

    private void Start()
    {
      if (!string.IsNullOrEmpty(MessageCenterName)) {
        MessageCenter = GameUIMessageCenter.FindGameUIMessageCenter(MessageCenterName);
        if (MessageCenter != null)
          MessageCenter.RegisterValueBinder(this);
        else
          Log.W("GameUIControlValueBinder:" + Name, "Failed to find GameUIMessageCenter {0}", MessageCenterName);
      }
      BinderBegin();
    }
    private void Awake()
    {
      if (!string.IsNullOrEmpty(MessageCenterName))
        MessageCenter = GameUIMessageCenter.FindGameUIMessageCenter(MessageCenterName);
    }
    private void OnDestroy()
    {
      if (MessageCenter != null)
        MessageCenter.UnRegisterValueBinder(this);
    }

    protected virtual void BinderBegin() { }

    /// <summary>
    /// 通知UI更新事件
    /// </summary>
    /// <param name="newval">新的数值</param>
    public void NotifyUserUpdate(object newval)
    {
      UserUpdateCallbacks.ForEach((a) => a.Invoke(newval));
    }
  }

  public delegate bool GameUIControlValueBinderSupplierCallback(object value);
  public delegate void GameUIControlValueBinderUserUpdateCallback(object value);
}

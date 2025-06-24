using System.Collections.Generic;
using UnityEngine;
using static Ballance2.Services.GameManager;

/*
* Copyright(c) 2021  mengyu
*
* 模块名：     
* GameUIMessageUtils.cs
* 
* 用途：
* UI 消息中心，方便处理UI事件。
*
* 此类提供了简单事件的绑定、数值同步绑定两个功能。
*
* 作者：
* mengyu
*/

namespace Ballance2.UI.Core
{
  /// <summary>
  /// UI 消息中心，方便处理UI事件
  /// </summary>
  [RequireComponent(typeof(RectTransform))]
  [AddComponentMenu("Ballance/UI/MessageCenter")]
  public class GameUIMessageCenter : MonoBehaviour
  {

    private static Dictionary<string, GameUIMessageCenter> messageCenters = new Dictionary<string, GameUIMessageCenter>();

    /// <summary>
    /// 查找系统中的 UI 消息中心
    /// </summary>
    /// <param name="name">名字</param>
    /// <returns>找到则返回 UI 消息中心实例，否则返回null</returns>
    public static GameUIMessageCenter FindGameUIMessageCenter(string name)
    {
      if (messageCenters.ContainsKey(name))
        return messageCenters[name];
      return null;
    }

    private Dictionary<string, GameUIControlValueBinder> valueBinders = new Dictionary<string, GameUIControlValueBinder>();
    private Dictionary<string, List<VoidDelegate>> events = new Dictionary<string, List<VoidDelegate>>();

    /// <summary>
    /// 注册数据更新器（该方法无需手动调用）
    /// </summary>
    /// <param name="binder"></param>
    /// <returns></returns>
    public bool RegisterValueBinder(GameUIControlValueBinder binder)
    {
      if (!valueBinders.ContainsKey(binder.Name))
      {
        valueBinders[binder.Name] = binder;
        return true;
      }
      return false;
    }

    /// <summary>
    /// 取消注册数据更新器（该方法无需手动调用）
    /// </summary>
    /// <param name="binder"></param>
    /// <returns></returns>
    public bool UnRegisterValueBinder(GameUIControlValueBinder binder)
    {
      if (valueBinders.ContainsKey(binder.Name))
      {
        valueBinders.Remove(binder.Name);
        return true;
      }
      return false;
    }

    /// <summary>
    /// 订阅数据更新器
    /// </summary>
    /// <param name="binderName">数据更新器名称</param>
    /// <param name="callbackFun">数据更新回调</param>
    /// <returns>返回一个可供更新数据的回调，调用此回调更新控件上的数据</returns>
    public GameUIControlValueBinderSupplierCallback SubscribeValueBinder(string binderName, GameUIControlValueBinderUserUpdateCallback callbackFun)
    {
      if (valueBinders.TryGetValue(binderName, out GameUIControlValueBinder binder))
      {
        binder.UserUpdateCallbacks.Add(callbackFun);
        return binder.BinderSupplierCallback;
      }
      return null;
    }
    public GameUIControlValueBinderSupplierCallback SubscribeValueBinder(GameUIControlValueBinder binder, GameUIControlValueBinderUserUpdateCallback callbackFun)
    {
      if (!valueBinders.ContainsKey(binder.Name))
        valueBinders[binder.Name] = binder;
      binder.UserUpdateCallbacks.Add(callbackFun);
      return binder.BinderSupplierCallback;
    }

    /// <summary>
    /// 使用数据更新器获取控件实例
    /// </summary>
    /// <param name="binderName">数据更新器名称</param>
    /// <returns></returns>
    public GameObject GetComponentInstance(string binderName)
    {
      if (valueBinders.TryGetValue(binderName, out GameUIControlValueBinder binder))
        return binder.gameObject;
      return null;
    }

    /// <summary>
    /// 取消订阅数据更新器
    /// </summary>
    /// <param name="binderName">数据更新器名称</param>
    /// <param name="callbackFun">数据更新回调</param>
    /// <returns>返回是否成功</returns>
    public bool UnSubscribeValueBinder(string binderName, GameUIControlValueBinderUserUpdateCallback callbackFun)
    {
      if (valueBinders.TryGetValue(binderName, out GameUIControlValueBinder binder))
      {
        binder.UserUpdateCallbacks.Remove(callbackFun);
        return true;
      }
      return false;
    }

    /// <summary>
    /// 订阅单一消息
    /// </summary>
    /// <param name="evtName">消息名称</param>
    /// <param name="callBack">消息回调</param>
    public void SubscribeEvent(string evtName, VoidDelegate callBack)
    {
      if (!events.TryGetValue(evtName, out var handlers))
      {
        handlers = new List<VoidDelegate>();
        events.Add(evtName, handlers);
      }
      handlers.Add(callBack);
    }

    /// <summary>
    /// 取消订阅单一消息
    /// </summary>
    /// <param name="evtName">消息名称</param>
    /// <param name="callBack">消息回调</param>
    public bool UnSubscribeEvent(string evtName, VoidDelegate callBack)
    {
      if (events.TryGetValue(evtName, out var handlers))
      {
        handlers.Remove(callBack);
        return true;
      }
      return false;
    }

    /// <summary>
    /// 发送单一消息
    /// </summary>
    /// <param name="evtName">消息名称</param>
    public void NotifyEvent(string evtName)
    {
      if (events.TryGetValue(evtName, out var handlers))
        handlers.ForEach((h) => h.Invoke());
    }

    private void Start()
    {
      if (!messageCenters.ContainsKey(Name))
        messageCenters.Add(Name, this);
    }
    private void OnDestroy()
    {
      messageCenters.Remove(Name);
    }

    [Tooltip("消息中心名字")]
    public string Name = "";
  }
}

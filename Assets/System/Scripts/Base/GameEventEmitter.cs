﻿using System;
using System.Collections.Generic;

/*
 * Copyright (c) 2022  mengyu
 * 
 * 模块名：     
 * GameEventEmitter.cs
 *
 * 用途：
 * 游戏事件发射器。
 * 
 * 作者：
 * imengyu
 */

namespace Ballance2.Base
{
  /// <summary>
  /// 游戏事件发射器
  /// 游戏事件发射器可以让某个类发送一组事件，让许多接受方订阅事件。
  /// 此类非常像 Nodejs 的 EventEmitter，此类功能也是启发自它。
  /// </summary>
  [Serializable]
  public class GameEventEmitter
  {
    public GameEventEmitter(string name) { Name = name; }

    /// <summary>
    /// 当前事件发射器的名称
    /// </summary>
    /// <value></value>
    public string Name { get; }

    public Dictionary<string, GameEventEmitterStorage> _Events = new Dictionary<string, GameEventEmitterStorage>();
    
    /// <summary>
    /// 获取指定的事件
    /// </summary>
    /// <param name="name">事件名称</param>
    /// <returns>返回事件存储实例</returns>
    public GameEventEmitterStorage GetEvent(string name) {
      GameEventEmitterStorage result = null;
      _Events.TryGetValue(name, out result);
      return result;
    }
    /// <summary>
    /// 注册一个事件存储实例，如果已经注册，则返回已有实例
    /// </summary>
    /// <param name="name">事件名称</param>
    /// <returns>返回事件存储实例</returns>
    public GameEventEmitterStorage RegisterEvent(string name) {
      GameEventEmitterStorage result = null;
      if(_Events.TryGetValue(name, out result))
        return result;
      result = new GameEventEmitterStorage(name);
      return result; 
    }
    /// <summary>
    /// 发射指定名称的事件
    /// </summary>
    /// <param name="name">事件名称</param>
    /// <param name="param">事件名称参数</param>
    public void EmitEvent(string name, object param) {
      if(_Events.TryGetValue(name, out var result))
        result.Emit(param);
    }
    /// <summary>
    /// 删除指定事件，此操作会删除所有订阅此事件的接收回调
    /// </summary>
    /// <param name="name">事件名称</param>
    public void DeleteEvent(string name) {
      if(_Events.ContainsKey(name))
        _Events.Remove(name);
    }
  }

  /// <summary>
  /// 事件发射器的事件存储类
  /// </summary>
  [Serializable]
  public class GameEventEmitterStorage
  {
    public GameEventEmitterStorage(string name) { Name = name; }

    /// <summary>
    /// 当前事件的名称
    /// </summary>
    /// <value></value>
    public string Name { get; }

    /// <summary>
    /// 发射当前事件
    /// </summary>
    /// <param name="obj">事件的参数</param>
    public void Emit(params object[] obj) {
      GameEventEmitterHandler n = _Listeners;
      GameEventEmitterHandler next = null;
      while(n != null) {
        next = n.Next;
        n.Delegate.Invoke(obj);
        //只执行一次就删除
        if(n.Once) 
          n.Off();
        //下一个
        n = next;
      }
    }

    internal GameEventEmitterHandler _Listeners;

    /// <summary>
    /// 增加事件侦听
    /// </summary>
    /// <param name="fn">事件侦听回调</param>
    public GameEventEmitterHandler On(GameEventEmitterDelegate fn) { return AddListener(fn, null, false); }
    /// <summary>
    /// 增加事件侦听并且设置标签，可以使用标签取消事件侦听
    /// </summary>
    /// <param name="fn">事件侦听回调</param>
    /// <param name="tag">指定事件标签</param>
    public GameEventEmitterHandler OnWithTag(GameEventEmitterDelegate fn, string tag) { return AddListener(fn, tag, false); }
    /// <summary>
    /// 增加单次事件侦听
    /// </summary>
    /// <param name="fn">事件侦听回调</param>
    public GameEventEmitterHandler Once(GameEventEmitterDelegate fn) { return AddListener(fn, null, true); }

    private GameEventEmitterHandler AddListener(GameEventEmitterDelegate fn, string tag, bool once) {
      GameEventEmitterHandler result = new GameEventEmitterHandler(this);
      if(_Listeners != null) {
        result.Next = _Listeners;
        _Listeners.Prev = result;
      }

      _Listeners = result;
      result.Delegate = fn;
      result.Tag = tag;
      result.Once = once;
      return result;
    }

    /// <summary>
    /// 移除事件侦听
    /// </summary>
    /// <param name="fn">事件侦听回调</param>
    public void Off(GameEventEmitterDelegate fn) {
      GameEventEmitterHandler n = _Listeners;
      GameEventEmitterHandler next = null;
      while(n != null) {
        next = n.Next;
        if(n.Delegate == fn) 
          n.Off();
        n = next;
      }
    }
    /// <summary>
    /// 移除事件指定标签的侦听
    /// </summary>
    /// <param name="tag">指定事件标签</param>
    public void OffAllTag(string tag) {
      GameEventEmitterHandler n = _Listeners;
      GameEventEmitterHandler next = null;
      while(n != null) {
        next = n.Next;
        if(n.Tag == tag) 
          n.Off();
        n = next;
      }
    }
    /// <summary>
    /// 清空当前事件的所有事件侦听
    /// </summary>
    public void Clear() {
      GameEventEmitterHandler n = _Listeners;
      GameEventEmitterHandler next = null;
      while(n != null) {
        next = n.Next;
        n.Off();
        n = next;
      }
      _Listeners = null;
    }
  }

  [Serializable]
  public class GameEventEmitterHandler
  {
    /// <summary>
    /// 获取当前回调所属事件
    /// </summary>
    public GameEventEmitterStorage Storage { get; }

    public GameEventEmitterHandler(GameEventEmitterStorage storage) {
      Storage = storage;
    }

    public GameEventEmitterHandler Next = null;
    public GameEventEmitterHandler Prev = null;
    public GameEventEmitterDelegate Delegate;
    public bool Once = false;
    public string Tag = null;

    /// <summary>
    /// 获取当前监听回调是否已经移除
    /// </summary>
    public bool Deleted { get; private set; } = false;

    /// <summary>
    /// 移除当前监听回调，与调用 GameEventEmitterStorage.Off 是一致的。
    /// </summary>
    public void Off() {
      if(Deleted)
        return;
      if(this == Storage._Listeners)//第一个
        Storage._Listeners = Next;
      if(Prev != null)
        Prev.Next = Next;
      if(Next != null)
        Next.Prev = Prev;
      Deleted = true;
    }
  }

  /// <summary>
  /// 事件接收器内核回调
  /// </summary>
  /// <param name="evtName">事件名称</param>
  /// <param name="pararms">参数</param>
  /// <returns>返回是否中断其他事件的分发</returns>
  public delegate void GameEventEmitterDelegate(params object[] obj);
}

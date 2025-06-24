﻿using Ballance2.Res;
using SubjectNerd.Utilities;
using System.Collections.Generic;
using UnityEngine;

/*
* Copyright(c) 2021  mengyu
*
* 模块名：     
* GameStaticResEntry.cs
* 
* 用途：
* 静态 Prefab 资源引入脚本.该脚本绑定在 GameEntry 上。
*
* 作者：
* mengyu
*/

namespace Ballance2.Entry
{
  public class GameStaticResEntry : MonoBehaviour
  {
    /// <summary>
    /// 静态 Prefab 资源引入
    /// </summary>
    [Reorderable("GamePrefab", true, "Name")]
    public List<GameObjectInfo> GamePrefab = null;
    /// <summary>
    /// 静态资源引入
    /// </summary>
    [Reorderable("GameAssets", true, "Name")]
    public List<GameAssetsInfo> GameAssets = null;

    private void Start()
    {
      GameSystem.FillResEntry(this);
    }
  }
}

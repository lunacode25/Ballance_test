using UnityEngine;

/*
 * Copyright (c) 2021  mengyu
 * 
 * 模块名：     
 * LookAt.cs
 * 
 * 用途：
 * 物体看物体工具脚本。
 * 
 * 作者：
 * mengyu
 */

namespace Ballance2.Game
{
  /// <summary>
  /// 物体看物体工具脚本。 此脚本可以让物体看者另一个物体，可以控制某个旋转轴。
  /// </summary>
  public class LookAt : MonoBehaviour {
    
    /// <summary>
    /// 指定看着的目标物体
    /// </summary>
    [Tooltip("指定看着的目标物体")]
    public Transform Target;
    /// <summary>
    /// 是否更新旋转轴X
    /// </summary>
    [Tooltip("是否更新旋转轴X")]
    public bool EnableX = true;
    /// <summary>
    /// 是否更新旋转轴Y
    /// </summary>
    [Tooltip("是否更新旋转轴Y")]
    public bool EnableY = true;
    /// <summary>
    /// 是否更新旋转轴Z
    /// </summary>
    [Tooltip("是否更新旋转轴Z")]
    public bool EnableZ = true;

    private void Update() {
      if(Target != null) {
        if(EnableX && EnableY && EnableZ)
          transform.LookAt(Target);
        else {
          Vector3 old = transform.eulerAngles;
          transform.LookAt(Target);

          if(EnableX) old.x = transform.eulerAngles.x;
          if(EnableY) old.y = transform.eulerAngles.y;
          if(EnableZ) old.z = transform.eulerAngles.z;

          transform.eulerAngles = old;
        }
      }
    }
  }
}
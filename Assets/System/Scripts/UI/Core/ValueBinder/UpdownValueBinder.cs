using Ballance2.UI.Core.Controls;
using UnityEngine;

/*
* Copyright(c) 2021  mengyu
*
* 模块名：     
* SliderValueBinder.cs
* 
* 用途：
* Slider控件数据绑定器
*
* 作者：
* mengyu
*/

namespace Ballance2.UI.Core.ValueBinder
{
  [AddComponentMenu("Ballance/UI/ValueBinder/UpdownValueBinder")]
  [RequireComponent(typeof(Updown))]
  public class UpdownValueBinder : GameUIControlValueBinder
  {
    private Updown slider = null;

    protected override bool OnBinderSupplierHandle(object value)
    {
      if (value.GetType() == typeof(int))
        slider.value = (int)value;
      else if (value.GetType() == typeof(float))
        slider.value = (float)value;
      else if (value.GetType() == typeof(double))
        slider.value = (float)(double)value;
      else
        UnityEngine.Debug.Log("Unknow type:  " + value.GetType().Name + " " + value);
      return true;
    }

    protected override void BinderBegin()
    {
      slider = GetComponent<Updown>();
      slider.onValueChanged.AddListener((v) =>
      {
        NotifyUserUpdate(v);
      });
    }
  }
}
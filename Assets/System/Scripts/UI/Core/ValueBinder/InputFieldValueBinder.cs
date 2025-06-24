using UnityEngine;
using UnityEngine.UI;

/*
* Copyright(c) 2021  mengyu
*
* 模块名：     
* InputFieldValueBinder.cs
* 
* 用途：
* InputField 控件数据绑定器
*
* 作者：
* mengyu
*/

namespace Ballance2.UI.Core.ValueBinder
{
  [AddComponentMenu("Ballance/UI/ValueBinder/InputFieldValueBinder")]
  [RequireComponent(typeof(InputField))]
  public class InputFieldValueBinder : GameUIControlValueBinder
  {
    private InputField input = null;

    protected override bool OnBinderSupplierHandle(object value)
    {
      input.text = (string)value;
      return true;
    }
    protected override void BinderBegin()
    {
      input = GetComponent<InputField>();
      input.onValueChanged.AddListener((v) =>
      {
        NotifyUserUpdate(v);
      });
    }
  }
}
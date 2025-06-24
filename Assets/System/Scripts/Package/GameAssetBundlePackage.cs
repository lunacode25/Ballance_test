﻿using Ballance2.Services.Debug;
using Ballance2.Utils;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

/*
* Copyright(c) 2021  mengyu
*
* 模块名：     
* GameAssetBundlePackage.cs
* 
* 用途：
* 游戏模块（AssetBundle）声明
*
* 作者：
* mengyu
*/

namespace Ballance2.Package
{
  /// <summary>
  /// 模块包 AssetBundle
  /// </summary>
  public class GameAssetBundlePackage : GamePackage
  {
    public override void Destroy()
    {
      base.Destroy();
    }

    public override async Task<bool> LoadInfo(string filePath)
    {
      PackageFilePath = PathUtils.FixFilePathScheme(filePath);

      UnityWebRequest request = UnityWebRequest.Get(filePath);
      await request.SendWebRequest();

      if (request.result == UnityWebRequest.Result.Success)
      {
        AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(request.downloadHandler.data);
        await assetBundleCreateRequest;
        AssetBundle = assetBundleCreateRequest.assetBundle;

        if (AssetBundle == null)
        {
          LoadError = "错误的包，加载 AssetBundle 失败";
          GameErrorChecker.SetLastErrorAndLog(GameError.AssetBundleNotFound, TAG, "Not found AssetBundle in Package");
          return false;
        }
        else
        {
          PreLoadI18NResource(null);

          TextAsset modDefTextAsset = AssetBundle.LoadAsset<TextAsset>("PackageDef.xml");
          if (modDefTextAsset == null || string.IsNullOrEmpty(modDefTextAsset.text))
          {
            GameErrorChecker.SetLastErrorAndLog(GameError.PackageDefNotFound, TAG, "PackageDef.xml not found");
            LoadError = "模块并不包含 PackageDef.xml";
            return false;
          }
          else
          {
            PackageDef = new XmlDocument();

            try
            {
              PackageDef.LoadXml(modDefTextAsset.text);
            }
            catch (System.Exception e)
            {
              GameErrorChecker.SetLastErrorAndLog(GameError.PackageIncompatible, TAG, "Format error in PackageDef.xml : " + e);
              return false;
            }

            if (StringUtils.IsUrl(filePath)) UpdateTime = System.DateTime.Now;
            else UpdateTime = System.IO.File.GetLastWriteTime(PathUtils.FixFilePathScheme(filePath));

            if(ReadInfo(PackageDef)) 
            {
              if (!string.IsNullOrEmpty(BaseInfo.Logo))
                LoadLogo();
              return true;
            }
            return false;
          }
        }
      }
      else
      {
        if (request.responseCode == 404)
          LoadError = "未找到资源包";
        else if (request.responseCode == 403)
          LoadError = "无权限读取资源包";
        else
          LoadError = "HTTP 请求错误 " + request.responseCode;

        GameErrorChecker.SetLastErrorAndLog(GameError.NetworkError, TAG, "Load AssetBundle failed : " + LoadError + "(" + request.responseCode + ")");
        return false;
      }
    }    
    private void LoadLogo()
    {
      try
      {
        Texture2D texture2D = AssetBundle.LoadAsset<Texture2D>(BaseInfo.Logo);
        BaseInfo.LogoTexture = Sprite.Create(texture2D,
            new Rect(Vector2.zero, new Vector2(texture2D.width, texture2D.height)),
            new Vector2(0.5f, 0.5f));
      }
      catch (System.Exception e)
      {
        BaseInfo.LogoTexture = null;
        Log.E(TAG, "在加载模块的 Logo {0} 失败\n错误信息：{1}", BaseInfo.Logo, e.ToString());
      }
    }
  }
}

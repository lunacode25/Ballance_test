﻿using System.Collections.Generic;
using Ballance2.Utils;

/*
* Copyright(c) 2021  mengyu
*
* 模块名：     
* Log.cs
* 
* 用途：
* 基础日志静态类。此类提供一些静态方可输出日志至控制台或文件，或
* 可注册日志观察者以获取系统输出的日志，供自己输出或处理。
* 
* 日志观察者使用方法：
* Log.RegisterLogObserver((level, tag, message, stackTrace) => {
*   //捕获Warning和Error等级的日志信息
* }, LogLevel.Warning | LogLevel.Error);
*
* 日志使用方法：
* Log.V(tag, message, ...) 打印一些最为繁琐、意义不大的日志信息 
* Log.D(tag, message, ...) 打印一些调试信息
* Log.I(tag, message, ...) 打印一些比较重要的数据，可帮助你分析用户行为数据
* Log.W(tag, message, ...) 打印一些警告信息
* Log.E(tag, message, ...) 打印错误信息
*
* 作者：
* mengyu
*
*/

namespace Ballance2
{
  /// <summary>
  /// 基础日志类
  /// </summary>
  public static class Log
  {
    private static string TAG = "Log";

    /// <summary>
    /// 打印一些最为繁琐、意义不大的日志信息 
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="message">要打印的日志信息</param>
    public static void V(string tag, string message)
    {
      LogInternal(LogLevel.Verbose, tag, message);
    }
    /// <summary>
    /// 打印可格式化字符串的日志信息 
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="format">格式化字符串，此字符串格式与 string.Format 格式相同</param>
    /// <param name="param">格式化参数</param>
    public static void V(string tag, string format, params object[] param)
    {
      V(tag, string.Format(format, param));
    }
    /// <summary>
    /// 打印一些调试信息
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="message">要打印的日志信息</param>
    public static void D(string tag, string message)
    {
      LogInternal(LogLevel.Debug, tag, message);
    }
    /// <summary>
    /// 打印可格式化字符串的调试信息
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="format">格式化字符串，此字符串格式与 string.Format 格式相同</param>
    /// <param name="param">格式化参数</param>
    public static void D(string tag, string format, params object[] param)
    {
      D(tag, string.Format(format, param));
    }
    /// <summary>
    /// 打印一些信息字符串
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="message">要打印的日志信息</param>
    public static void I(string tag, string message)
    {
      LogInternal(LogLevel.Info, tag, message);
    }
    /// <summary>
    /// 打印可格式化字符串的信息
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="format">格式化字符串，此字符串格式与 string.Format 格式相同</param>
    /// <param name="param">格式化参数</param>
    public static void I(string tag, string format, params object[] param)
    {
      I(tag, string.Format(format, param));
    }
    /// <summary>
    /// 打印一些警告信息
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="message">要打印的日志信息</param>
    public static void W(string tag, string message)
    {
      LogInternal(LogLevel.Warning, tag, message);
    }
    /// <summary>
    /// 打印可格式化字符串的警告信息
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="format">格式化字符串，此字符串格式与 string.Format 格式相同</param>
    /// <param name="param">格式化参数</param>
    public static void W(string tag, string format, params object[] param)
    {
      W(tag, string.Format(format, param));
    }
    /// <summary>
    /// 打印错误信息
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="message">要打印的日志信息</param>
    public static void E(string tag, string message)
    {
      LogInternal(LogLevel.Error, tag, message);
    }
    /// <summary>
    /// 打印可格式化字符串的错误信息
    /// </summary>
    /// <param name="tag">标签</param>
    /// <param name="format">格式化字符串，此字符串格式与 string.Format 格式相同</param>
    /// <param name="param">格式化参数</param>
    public static void E(string tag, string format, params object[] param)
    {
      E(tag, string.Format(format, param));
    }

    private static void LogInternal(LogLevel level, string tag, string message)
    {
      LogWrite(level, tag, message, DebugUtils.GetStackTrace(2));
    }

    /// <summary>
    /// 手动写入日志
    /// </summary>
    /// <param name="level">日志等级</param>
    /// <param name="tag">标签</param>
    /// <param name="message">信息</param>
    /// <param name="stackTrace">堆栈信息</param>
    public static void LogWrite(LogLevel level, string tag, string message, string stackTrace)
    {
      if (logWriteLock)
      {
        logWriteLock = false;
        return;
      }

      //如果在 Editor 中就把日志输出到控制台显示
      //#if UNITY_EDITOR
      logWriteLock = true;

      var str = string.Format("[{0}] {1}", tag, message);
      switch (level)
      {
        case LogLevel.Debug:
        case LogLevel.Verbose:
        case LogLevel.Info: UnityEngine.Debug.Log(str); break;
        case LogLevel.Warning: UnityEngine.Debug.LogWarning(str); break;
        case LogLevel.Error: UnityEngine.Debug.LogError(str); break;
      }

      logWriteLock = false;
      //#endif

      if (!logTemporaryForeachLock)
      {
        LogTemporaryData data = new LogTemporaryData();
        data.level = level;
        data.message = message;
        data.tag = tag;
        data.stackTrace = stackTrace;
        logTemporary.Add(data);
      }

      if (logTemporary.Count > 256)
        logTemporary.RemoveAt(0);

      observers.ForEach((observer) =>
      {
        if ((observer.AcceptLevel & level) != LogLevel.None)
          observer.Observer(level, tag, message, stackTrace);
      });
    }

    /// <summary>
    /// 重新发送暂存区中的日志条目
    /// </summary>
    public static void SendLogsInTemporary()
    {
      logTemporaryForeachLock = true;
      logTemporary.ForEach((data) =>
      {
        observers.ForEach((observer) =>
        {
          if ((observer.AcceptLevel & data.level) != LogLevel.None)
            observer.Observer(data.level, data.tag, data.message, data.stackTrace);
        });
      });
      logTemporary.Clear();
      logTemporaryForeachLock = false;
    }

    /// <summary>
    /// 注册日志观察者
    /// </summary>
    /// <param name="observer">观察者回调</param>
    /// <param name="acceptLevel">指定观察者要捕获的日志等级</param>
    /// <returns>返回大于0的数字表示观察者ID，返回-1表示错误</returns>
    /// <example>
    /// Log.RegisterLogObserver((level, tag, message, stackTrace) => {
    ///   //捕获Warning和Error等级的日志信息
    /// }, LogLevel.Warning | LogLevel.Error);
    /// </example>
    public static int RegisterLogObserver(LogObserver observer, LogLevel acceptLevel)
    {
      if (acceptLevel == LogLevel.None)
      {
        E(TAG, "At least one LogLevel is required for LogObserver ! ");
        return -1;
      }

      LogObserverInternal logObserverInternal = observers.Find((o) => o.Observer == observer);
      if (logObserverInternal != null)
      {
        E(TAG, "Can not register LogObserver {0} because it already registered! ", observer.GetHashCode());
        return -1;
      }

      logObserverInternal = new LogObserverInternal();
      logObserverInternal.Id = CommonUtils.GenAutoIncrementID();
      logObserverInternal.AcceptLevel = acceptLevel;
      logObserverInternal.Observer = observer;

      observers.Add(logObserverInternal);
      return logObserverInternal.Id;
    }
    /// <summary>
    /// 取消注册日志观察者
    /// </summary>
    /// <param name="id">观察者ID（由 RegisterLogObserver 返回）</param>
    public static void UnRegisterLogObserver(int id)
    {
      LogObserverInternal logObserverInternal = observers.Find((o) => o.Id == id);
      if (logObserverInternal == null)
        return;
      observers.Remove(logObserverInternal);
    }
    /// <summary>
    /// 获取日志观察者
    /// </summary>
    /// <param name="id">观察者ID（由 RegisterLogObserver 返回）</param>
    /// <returns>如果找到则返回观察者，如果找不到则返回null</returns>
    public static LogObserver GetLogObserver(int id)
    {
      LogObserverInternal logObserverInternal = observers.Find((o) => o.Id == id);
      if (logObserverInternal != null)
        return logObserverInternal.Observer;
      return null;
    }

    /// <summary>
    /// 日志等级转为对应字符串
    /// </summary>
    /// <param name="logLevel">日志等级</param>
    /// <returns></returns>
    public static string LogLevelToString(LogLevel logLevel)
    {
      switch (logLevel)
      {
        case LogLevel.None: return "N";
        case LogLevel.Verbose: return "V";
        case LogLevel.Debug: return "D";
        case LogLevel.Info: return "I";
        case LogLevel.Warning: return "W";
        case LogLevel.Error: return "E";
        case LogLevel.All: return "A";
      }
      return logLevel.ToString();
    }

    private struct LogTemporaryData
    {
      public LogLevel level;
      public string tag;
      public string message;
      public string stackTrace;
    }

    /// <summary>
    /// 获取指定等级的文字颜色
    /// </summary>
    /// <param name="level">日志等级</param>
    /// <returns>返回十六进制颜色字符串，例如 ffffff</returns>
    public static string GetLogColor(LogLevel level)
    {
      switch (level)
      {
        case LogLevel.Info: return "67CCFF";
        case LogLevel.Verbose: return "FFFFFF";
        case LogLevel.Warning: return "FFCE00";
        case LogLevel.Error: return "FF1B00";
        default: return "FFFFFF";
      }
    }

    private static List<LogObserverInternal> observers = new List<LogObserverInternal>();
    private static List<LogTemporaryData> logTemporary = new List<LogTemporaryData>();
    private static bool logTemporaryForeachLock = false;
    private static bool logWriteLock = false;

    /// <summary>
    /// 内部观察者保存类
    /// </summary>
    private class LogObserverInternal
    {
      public int Id;
      public LogObserver Observer;
      public LogLevel AcceptLevel;
    }

    internal static void StartLogFile() {
      new LogFileObserver();
    }
  }

  /// <summary>
  /// 日志等级
  /// </summary>
  public enum LogLevel
  {
    /// <summary>
    /// 无
    /// </summary>
    None = 0,
    /// <summary>
    /// 无关紧要的调试信息
    /// </summary>
    Verbose = 0x1,
    /// <summary>
    /// 调试信息
    /// </summary>
    Debug = 0x2,
    /// <summary>
    /// 信息
    /// </summary>
    Info = 0x4,
    /// <summary>
    /// 警告
    /// </summary>
    Warning = 0x8,
    /// <summary>
    /// 错误
    /// </summary>
    Error = 0x10,
    /// <summary>
    /// 表示全部日志等级
    /// </summary>
    All = Verbose | Debug | Info | Warning | Error,
  }

  /// <summary>
  /// 日志观察者接口
  /// </summary>
  public delegate void LogObserver(LogLevel level, string tag, string message, string stackTrace);
}

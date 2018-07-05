// Copyright (c) 2013-2018 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System;

namespace SharpConfig
{
  /// <summary>
  /// Represents an error that occurs when a string value could not be converted to a specific instance.
  /// </summary>
  [Serializable]
  public sealed class SettingValueCastException : Exception
  {
    private SettingValueCastException(string message, Exception innerException)
        : base(message, innerException)
    { }

    internal static SettingValueCastException Create(string stringValue, Type dstType, Exception innerException)
    {
      string msg = $"Failed to convert value '{stringValue}' to type {dstType.FullName}.";
      return new SettingValueCastException(msg, innerException);
    }
  }
}
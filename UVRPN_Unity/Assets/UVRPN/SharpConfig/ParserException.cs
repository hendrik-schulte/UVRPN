// Copyright (c) 2013-2018 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System;

namespace SharpConfig
{
  /// <summary>
  /// Represents an error that occurred during
  /// the configuration parsing stage.
  /// </summary>
  [Serializable]
  public sealed class ParserException : Exception
  {
    internal ParserException(string message, int line)
        : base($"Line {line}: {message}")
    {
      Line = line;
    }

    /// <summary>
    /// Gets the line in the configuration that caused the exception.
    /// </summary>
    public int Line { get; private set; }
  }
}
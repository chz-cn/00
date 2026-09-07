
#:property TargetFramework=net10.0
#:property PublishAot=false
#:property Nullable=enable
#:property ImplicitUsings=disable
#:property DebugType=None
#:property DebugSymbols=false

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Threading.Tasks;

await TGen.Gen().ConfigureAwait(false);

public static class TGen
{
  public static ReadOnlySpan<byte> Indexer => """

- name: index
  href: ../index.md

"""u8;

  public static ReadOnlySpan<byte> TP => """
- name: "xx"
  href: ../xx/

"""u8;

  public static ReadOnlySpan<byte> TPF => """
- name: xxxx-xxxx-4xxx-xxxx-xxxxxxxxxxxx
  href: xxxx-xxxx-4xxx-xxxx-xxxxxxxxxxxx.md

"""u8;

  public static readonly EnumerationOptions Opt = new()
  { MatchType = MatchType.Simple, AttributesToSkip = 0, IgnoreInaccessible = false };

  public static Task Gen()
  {
    int count = 0;
    Span<string> sp = new string[256];

    foreach (var l1 in Directory.EnumerateDirectories("src", "*", Opt))
    {
      if (l1.Length is not 6) Throw(l1);
      sp[count++] = l1;
    }

    sp = sp[..count];
    Span<Task> t = new Task[count];

    count = 0;
    foreach (var l1 in sp)
      t[count++] = Task.Factory.StartNew(ProcessL1, l1);

    var root = new List<byte>(7203);
    Span<byte> tp = stackalloc byte[TP.Length];
    TP.CopyTo(tp);

    root.AddRange(Indexer);
    foreach (var l1 in sp)
    {
      tp[9] = (byte)l1[4];
      tp[10] = (byte)l1[5];
      tp[24] = (byte)l1[4];
      tp[25] = (byte)l1[5];
      root.AddRange(tp);
    }

    Directory.CreateDirectory("src/_data");
    File.WriteAllBytes("src/_data/toc.yml", CollectionsMarshal.AsSpan(root));

    return Task.WhenAll(t);

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    static void Throw(string l1) => throw new ArgumentOutOfRangeException
      (nameof(l1), "Unsupported Directory&length: " + l1);

    static void ProcessL1(object? obj)
    {
      var path = (string)obj!;

      var str = new List<byte>(512);
      Span<byte> tp = stackalloc byte[TPF.Length];
      TPF.CopyTo(tp);
      ref byte tp0 = ref tp[0];
      str.Clear();
      str.Add(10);

      foreach (string file in Directory.EnumerateFiles(path, "*", Opt))
      {
        if (file.Length is not 42) Throw(file);

        ref readonly ushort start = ref Unsafe.As<char, ushort>(
          ref Unsafe.AsRef(in file.AsSpan()[7]));

        if (Vector256.IsHardwareAccelerated && Vector256<ushort>.IsSupported)
        {
          var acc0 = Vector256.LoadUnsafe(in start);
          var acc1 = Vector256.LoadUnsafe(in start, 16);
          var vec = Vector256.Narrow(acc0, acc1);
          vec.StoreUnsafe(ref tp0, 8);
          vec.StoreUnsafe(ref tp0, 49);
        }
        else
        {
          var acc0 = Vector128.LoadUnsafe(in start);
          var acc1 = Vector128.LoadUnsafe(in start, 8);
          var acc2 = Vector128.LoadUnsafe(in start, 16);
          var acc3 = Vector128.LoadUnsafe(in start, 24);
          var vec0 = Vector128.Narrow(acc0, acc1);
          var vec1 = Vector128.Narrow(acc2, acc3);
          vec0.StoreUnsafe(ref tp0, 8);
          vec1.StoreUnsafe(ref tp0, 24);
          vec0.StoreUnsafe(ref tp0, 49);
          vec1.StoreUnsafe(ref tp0, 65);
        }
        str.AddRange(tp);
      }

      File.WriteAllBytes(path + "/toc.yml", CollectionsMarshal.AsSpan(str));

      [DoesNotReturn]
      [MethodImpl(MethodImplOptions.NoInlining)]
      static void Throw(string file) => throw new ArgumentOutOfRangeException
        (nameof(file), "Unsupported File&length: " + file);
    }
  }
}

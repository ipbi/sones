#region License
/* **********************************************************************************
 * Copyright (c) Roman Ivantsov
 * This source code is subject to terms and conditions of the MIT License
 * for CLIrony. A copy of the license can be found in the License.txt file
 * at the root of this distribution. 
 * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
 * MIT License.
 * You must not remove this notice from this software.
 * **********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace sones.Lib.Frameworks.CLIrony {
  //Some common classes

  public class StringDictionary : Dictionary<string, string> { }
  public class CharList : List<char> { }

  public class StringSet : HashSet<string> {
    public void AddRange(IEnumerable<string> values) {
      foreach (string value in values) Add(value);
    }
    public override string ToString() {
      return ToString(" ");
    }
    public string ToString(string separator) {
      return TextUtils.JoinStrings(separator, this);
    }
  }

  public class StringList : List<string> {
    public StringList() { }
    public StringList(params string[] args) {
      AddRange(args);
    }
    public override string ToString() {
      return ToString(" ");
    }
    public string ToString(string separator) {
      return TextUtils.JoinStrings(separator, this);
    }
    //Used in sorting suffixes and prefixes; longer strings must come first in sort order
    public static int LongerFirst(string x, string y) {
      try {//in case any of them is null
        if (x.Length > y.Length) return -1;
      } catch { }
      if (x == y) return 0;
      return 1; 
    }

  }//KeyList class

}

﻿#region License
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
using System.Linq;
using System.Text;

namespace sones.Lib.Frameworks.CLIrony.Compiler {

  public enum CodeAnalysisPhase {
    Init,
    AssignScopes,
    Allocate,    //Allocating local variables
    Binding,     //Binding variable references to variable locations
    Optimization,
    MarkTailCalls,
  }

  public class CodeAnalysisArgs {
    public readonly CompilerContext Context;
    public CodeAnalysisPhase Phase;
    public bool SkipChildren;
    public CodeAnalysisArgs(CompilerContext context) {
      Context = context;
      Phase = CodeAnalysisPhase.Init;
    }
  }



}

﻿using System.Collections.Generic;
using sones.GraphDB.Indices;
using sones.GraphDB.QueryLanguage.NonTerminalCLasses.Aggregates;
using sones.GraphDB.QueryLanguage.NonTerminalCLasses.Functions;
using sones.GraphDB.QueryLanguage.Operators;
using sones.GraphDB.Settings;
using sones.GraphDB.Structures.EdgeTypes;
using sones.GraphFS.DataStructures;
using sones.GraphFS.Objects;
using sones.GraphDB.ImportExport;

namespace sones.GraphDB.QueryLanguage
{

    /// <summary>
    /// marks a grammar as extendable
    /// </summary>
    public interface IExtendableGrammar
    {

        void SetAggregates      (IEnumerable<ABaseAggregate>    aggregates);
        void SetFunctions       (IEnumerable<ABaseFunction>     functions);
        void SetOperators       (IEnumerable<ABinaryOperator>   operators);
        void SetSettings        (IEnumerable<ADBSettingsBase>   settings);
        void SetEdges           (IEnumerable<AEdgeType>         edges);
        void SetIndices         (IEnumerable<IVersionedIndexObject<IndexKey, ObjectUUID>> indices);
        void SetGraphDBImporter (IEnumerable<AGraphDBImport>    graphDBImporter);

    }
}

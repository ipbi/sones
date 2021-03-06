﻿/*
 * CreateIndexAttributeNode
 * (c) Achim Friedland, 2009 - 2010
 */

#region usings

using System;

using sones.GraphDB.Exceptions;
using sones.GraphDB.Managers.Structures;

using sones.Lib.Frameworks.Irony.Parsing;

#endregion

namespace sones.GraphDB.GraphQL.StructureNodes
{

    /// <summary>
    /// This node is requested in case of an CreateIndexAttributeNode node.
    /// </summary>

    public class IndexAttributeNode : AStructureNode, IAstNodeInit
    {

        #region Properties

        public IndexAttributeDefinition IndexAttributeDefinition { get; private set; }

        #endregion

        #region Data

        private IDChainDefinition _IndexAttribute = null;
        private String _OrderDirection = null;
        private String _IndexType = null;

        #endregion

        #region Constructor

        public IndexAttributeNode()
        { }

        #endregion

        #region GetContent(myCompilerContext, myParseTreeNode)

        public void GetContent(CompilerContext myCompilerContext, ParseTreeNode myParseTreeNode)
        {

            if (myParseTreeNode.ChildNodes[0].HasChildNodes())
            {

                if (myParseTreeNode.ChildNodes[0].ChildNodes[0].ChildNodes.Count > 1)
                {

                    _IndexType = ((ATypeNode)myParseTreeNode.ChildNodes[0].ChildNodes[0].ChildNodes[0].AstNode).ReferenceAndType.TypeName;
                    _IndexAttribute = ((IDNode)myParseTreeNode.ChildNodes[0].ChildNodes[0].ChildNodes[2].AstNode).IDChainDefinition;

                }

                else
                {
                    //_IndexAttribute = myParseTreeNode.ChildNodes[0].ChildNodes[0].Token.ValueString;
                    _IndexAttribute = new IDChainDefinition();
                    _IndexAttribute.AddPart(new ChainPartTypeOrAttributeDefinition(myParseTreeNode.ChildNodes[0].ChildNodes[0].Token.ValueString));
                }

            }

            if (myParseTreeNode.ChildNodes.Count > 1 && myParseTreeNode.ChildNodes[1].HasChildNodes())
            {
                _OrderDirection = myParseTreeNode.ChildNodes[1].FirstChild.Token.ValueString;
            }
            else
            {
                _OrderDirection = String.Empty;
            }

            IndexAttributeDefinition = new IndexAttributeDefinition(_IndexAttribute, _IndexType, _OrderDirection);

        }

        #endregion

        #region IAstNodeInit Members

        public void Init(CompilerContext context, ParseTreeNode parseNode)
        {
            GetContent(context, parseNode);
        }

        #endregion

        #region ToString()

        public override String ToString()
        {

            if (_OrderDirection.Equals(String.Empty))
                return String.Concat(_IndexAttribute);

            else
                return String.Concat(_IndexAttribute, " ", _OrderDirection);

        }

        #endregion

    }

}

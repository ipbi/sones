﻿/*
 * AttributeAssignOrUpdateUndefined
 * (c) Stefan Licht, 2010
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sones.GraphDB.Structures.Enums;
using sones.GraphDB.ObjectManagement;
using sones.GraphDB.TypeManagement;
using sones.Lib.ErrorHandling;

using sones.GraphDB.Errors;
using sones.GraphDB.TypeManagement;

namespace sones.GraphDB.Managers.Structures
{

    #region AttributeAssignOrUpdateUndefined  - Refactor and add undefined logic into defined attribute AssignsOrUpdate

    public class AttributeAssignOrUpdateUndefined : AAttributeAssignOrUpdate
    {

        #region Properties

        public UndefinedAttributeDefinition UndefinedAttribute { get; private set; }

        #endregion

        #region Ctor

        public AttributeAssignOrUpdateUndefined(IDChainDefinition myIDChainDefinition, UndefinedAttributeDefinition myUndefinedAttribute)
            : base(myIDChainDefinition)
        {
            UndefinedAttribute = myUndefinedAttribute;
        }

        #endregion

        #region override AAttributeAssignOrUpdate.GetValueForAttribute

        public override Exceptional<IObject> GetValueForAttribute(DBObjectStream aDBObject, DBContext dbContext, GraphDBType _Type)
        {
            return new Exceptional<IObject>(new Error_NotImplemented(new System.Diagnostics.StackTrace(true)));
        }
        
        #endregion

        #region override AAttributeAssignOrUpdateOrRemove.Update

        public override Exceptional<Dictionary<String, Tuple<TypeAttribute, IObject>>> Update(DBContext myDBContext, DBObjectStream myDBObjectStream, GraphDBType myGraphDBType)
        {
            Dictionary<String, Tuple<TypeAttribute, IObject>> attrsForResult = new Dictionary<String, Tuple<TypeAttribute, IObject>>();

            #region undefined attributes

            //TODO: change this to a more handling thing than KeyValuePair
            var addExcept = myDBContext.DBObjectManager.AddUndefinedAttribute(UndefinedAttribute.AttributeName, UndefinedAttribute.AttributeValue, myDBObjectStream);

            if (addExcept.Failed())
            {
                return new Exceptional<Dictionary<string, Tuple<TypeAttribute, IObject>>>(addExcept);
            }

            //sthChanged = true;

            attrsForResult.Add(UndefinedAttribute.AttributeName, new Tuple<TypeAttribute, IObject>(new UndefinedTypeAttribute(UndefinedAttribute.AttributeName), UndefinedAttribute.AttributeValue));

            #endregion

            return new Exceptional<Dictionary<string, Tuple<TypeAttribute, IObject>>>(attrsForResult);

        }

        #endregion

    }

    #endregion

}

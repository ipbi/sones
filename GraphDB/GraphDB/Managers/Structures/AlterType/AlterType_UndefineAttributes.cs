﻿/*
 * AlterType_UndefineAttributes
 * (c) Stefan Licht, 2010
 */

#region Usings

using System;
using System.Collections.Generic;

using sones.GraphDB.Errors;
using sones.GraphDB.NewAPI;
using sones.GraphDB.TypeManagement;
using sones.GraphDB.Structures.Enums;
using sones.GraphDB.Structures.ExpressionGraph;

using sones.Lib.ErrorHandling;
using sones.GraphDB.Indices;

#endregion

namespace sones.GraphDB.Managers.AlterType
{

    public class AlterType_UndefineAttributes : AAlterTypeCommand
    {

        #region data

        private List<String> _ListOfAttributes;

        #endregion

        public AlterType_UndefineAttributes(List<String> listOfAttributes)
        {
            _ListOfAttributes = listOfAttributes;
        }

        public override TypesOfAlterCmd AlterType
        {
            get { throw new NotImplementedException(); }
        }

        public override Exceptional Execute(DBContext dbContext, GraphDBType graphDBType)
        {

            var listOfTypeAttrs = new List<TypeAttribute>();
            var retExcept = new Exceptional();

            foreach (var attr in _ListOfAttributes)
            {

                var typeAttr = graphDBType.GetTypeAttributeByName(attr);

                if (typeAttr == null)
                {
                    return new Exceptional(new Error_AttributeIsNotDefined(attr));
                }

                var attrType = dbContext.DBTypeManager.GetTypeByUUID(typeAttr.DBTypeUUID);

                if (attrType == null)
                    return new Exceptional(new Error_TypeDoesNotExist(""));

                if (attrType.IsUserDefined)
                    return new Exceptional(new Error_InvalidReferenceAssignmentOfUndefAttr());

                listOfTypeAttrs.Add(typeAttr);

            }

            var dbobjects = dbContext.DBObjectCache.SelectDBObjectsForLevelKey(new LevelKey(graphDBType, dbContext.DBTypeManager), dbContext);

            foreach (var item in dbobjects)
            {

                if (!item.Success())
                {
                    retExcept.PushIExceptional(item);
                }

                else
                {
                    foreach (var attr in listOfTypeAttrs)
                    {
                        if (item.Value.HasAttribute(attr.UUID, graphDBType))
                        {
                            var attrVal = item.Value.GetAttribute(attr.UUID);

                            var addExcept = item.Value.AddUndefinedAttribute(attr.Name, attrVal, dbContext.DBObjectManager);

                            if (!addExcept.Success())
                                retExcept.PushIExceptional(addExcept);

                            item.Value.RemoveAttribute(attr.UUID);

                            var saveExcept = dbContext.DBObjectManager.FlushDBObject(item.Value);

                            if (!saveExcept.Success())
                                retExcept.PushIExceptional(saveExcept);
                        }
                    }
                }
            }


            var indices = graphDBType.GetAllAttributeIndices();
            List<AAttributeIndex> idxToDelete = new List<AAttributeIndex>();

            //remove attributes from type
            foreach (var attr in listOfTypeAttrs)
            {

                foreach (var item in indices)
                {
                    var index = item.IndexKeyDefinition.IndexKeyAttributeUUIDs.Find(idx => idx == attr.UUID);

                    if (index != null && item.IndexKeyDefinition.IndexKeyAttributeUUIDs.Count == 1)
                    {
                        idxToDelete.Add(item);
                    }
                }

                //remove indices
                foreach (var idx in idxToDelete)
                {
                    var remExcept = graphDBType.RemoveIndex(idx.IndexName, idx.IndexEdition, dbContext.DBTypeManager);

                    if (!remExcept.Success())
                    {
                        retExcept.PushIExceptional(remExcept);
                    }
                }

                idxToDelete.Clear();
                graphDBType.RemoveAttribute(attr.UUID);

                var flushExcept = dbContext.DBTypeManager.FlushType(graphDBType);

                if (!flushExcept.Success())
                {
                    retExcept.PushIExceptional(flushExcept);
                }
            }

            return retExcept;

        }

        public override IEnumerable<Vertex> CreateVertex(DBContext dbContext, GraphDBType graphDBType)
        {
            return base.CreateVertex(dbContext, graphDBType);
        }

    }

}

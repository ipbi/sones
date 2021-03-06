﻿
#region Usings

using System;
using System.Collections.Generic;
using sones.GraphDB.Indices;
using sones.GraphDB.ObjectManagement;
using sones.GraphDB.TypeManagement;
using sones.GraphDB.TypeManagement.BasicTypes;
using sones.GraphDB.TypeManagement;
using sones.Lib;
using sones.Lib.ErrorHandling;

#endregion

namespace sones.GraphDB.Aggregates
{

    /// <summary>
    /// The aggregate COUNT
    /// </summary>
    public class CountAggregate : ABaseAggregate
    {

        #region Properties

        public override string FunctionName
        {
            get { return "COUNT"; }
        }

        #endregion

        #region Attribute aggregation

        public override Exceptional<IObject> Aggregate(IEnumerable<DBObjectStream> myDBObjects, TypeAttribute myTypeAttribute, DBContext myDBContext, params Functions.ParameterValue[] myParameters)
        {
            return new Exceptional<IObject>(new DBUInt64(myDBObjects.ULongCount()));
        }

        #endregion

        #region Index aggregation

        public override Exceptional<IObject> Aggregate(AAttributeIndex attributeIndex, GraphDBType graphDBType, DBContext dbContext)
        {
            if (graphDBType.IsAbstract)
            {
                #region For abstract types, count all attribute idx of the subtypes

                UInt64 count = 0;

                foreach (var aSubType in dbContext.DBTypeManager.GetAllSubtypes(graphDBType, false))
                {
                    if (!aSubType.IsAbstract)
                    {
                        count += aSubType.GetUUIDIndex(dbContext.DBTypeManager).GetValueCount(aSubType, dbContext);
                    }
                }

                return new Exceptional<IObject>(new DBUInt64(count));

                #endregion
            }
            else
            {
                #region Return the count of idx values

                var indexRelatedType = dbContext.DBTypeManager.GetTypeByUUID(attributeIndex.IndexRelatedTypeUUID);

                return new Exceptional<IObject>(new DBUInt64(attributeIndex.GetValueCount(indexRelatedType, dbContext)));

                #endregion
            }
        }

        #endregion


    }

}

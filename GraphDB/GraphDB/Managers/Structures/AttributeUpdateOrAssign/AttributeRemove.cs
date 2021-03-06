﻿/*
 * AttributeRemove
 * (c) Stefan Licht, 2010
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sones.Lib.ErrorHandling;
using sones.GraphDB.TypeManagement;

using sones.GraphDB.ObjectManagement;
using sones.GraphDB.Errors;
using sones.Lib;
using sones.GraphDB.TypeManagement;

namespace sones.GraphDB.Managers.Structures
{

    /// <summary>
    /// Removes some attributes
    /// </summary>
    public class AttributeRemove : AAttributeRemove
    {
        
        #region Properties

        public List<string> ToBeRemovedAttributes { get; private set; }

        #endregion

        #region Ctor

        public AttributeRemove(List<string> _toBeRemovedAttributes)
        {
            // TODO: Complete member initialization
            this.ToBeRemovedAttributes = _toBeRemovedAttributes;
        }

        #endregion

        #region override AAttributeAssignOrUpdateOrRemove.Update

        public override Exceptional<Dictionary<string, Tuple<TypeAttribute, IObject>>> Update(DBContext myDBContext, DBObjectStream myDBObjectStream, GraphDBType myGraphDBType)
        {

            Dictionary<String, Tuple<TypeAttribute, IObject>> attrsForResult = new Dictionary<String, Tuple<TypeAttribute, IObject>>();

            #region AttributeRemove

            #region divide remove list in undefined and defined attributes

            var undefAttrsExcept = myDBObjectStream.GetUndefinedAttributes(myDBContext.DBObjectManager);

            if (undefAttrsExcept.Failed())
            {
                return new Exceptional<Dictionary<string, Tuple<TypeAttribute, IObject>>>(undefAttrsExcept);
            }


            var undefAttrsOfObject = undefAttrsExcept.Value;

            var undefAttrsToRemove = ToBeRemovedAttributes.Where(item => undefAttrsOfObject.ContainsKey(item)).ToList();

            List<String> defAttrsToRemove = new List<String>();
            List<String> unknowAttrs = new List<String>();

            foreach (var item in ToBeRemovedAttributes.Where(item => !undefAttrsOfObject.ContainsKey(item)).ToList())
            {
                if (myGraphDBType.GetTypeAttributeByName(item) != null)
                    defAttrsToRemove.Add(item);
                else
                    unknowAttrs.Add(item);
            }

            #endregion

            #region remove undefined attributes
            
            if (!unknowAttrs.IsNullOrEmpty())
            {
                return new Exceptional<Dictionary<string, Tuple<TypeAttribute, IObject>>>(new Error_InvalidUndefinedAttributes(unknowAttrs));
            }

            foreach (var aAttribute in undefAttrsToRemove)
            {
                var removeExcept = myDBContext.DBObjectManager.RemoveUndefinedAttribute(aAttribute, myDBObjectStream);

                if (removeExcept.Failed())
                {
                    return new Exceptional<Dictionary<string, Tuple<TypeAttribute, IObject>>>(removeExcept);
                }

                attrsForResult.Add(aAttribute, new Tuple<TypeAttribute, IObject>(new UndefinedTypeAttribute(aAttribute), null));
            }

            //if (!undefAttrsToRemove.IsNullOrEmpty())
            //    sthChanged = true;

            #endregion

            #region RemoveAttribute

            var applyRemoveResult = ApplyRemoveAttribute(defAttrsToRemove, myDBContext, myDBObjectStream, myGraphDBType);

            if (applyRemoveResult.Failed())
            {
                return new Exceptional<Dictionary<string, Tuple<TypeAttribute, IObject>>>(applyRemoveResult);
            }

            if (applyRemoveResult.Value.Count > 0)
            {
                //sthChanged = true;

                #region Add to queryResult

                foreach (var attr in applyRemoveResult.Value)
                {
                    attrsForResult.Add(attr.Name, new Tuple<TypeAttribute, IObject>(attr, null));
                }

                #endregion

            }

            #endregion

            #endregion

            return new Exceptional<Dictionary<string, Tuple<TypeAttribute, IObject>>>(attrsForResult);

        }

        private Exceptional<List<TypeAttribute>> ApplyRemoveAttribute(List<string> myAttrsToRemove, DBContext dbContext, DBObjectStream aDBObject, GraphDBType myGraphDBType)
        {
            #region data

            List<TypeAttribute> removedAttributes = new List<TypeAttribute>();

            #endregion

            var MandatoryTypeAttrib = myGraphDBType.GetMandatoryAttributesUUIDs(dbContext.DBTypeManager);
            foreach (String aAttribute in myAttrsToRemove)
            {
                TypeAttribute typeAttribute = myGraphDBType.GetTypeAttributeByName(aAttribute);

                if (aDBObject.HasAttribute(typeAttribute.UUID, myGraphDBType))
                {
                    if (!MandatoryTypeAttrib.Contains(typeAttribute.UUID))
                    {
                        #region remove backward edges

                        if (typeAttribute.GetDBType(dbContext.DBTypeManager).IsUserDefined)
                        {
                            var userdefinedAttributes = new Dictionary<AttributeUUID, object>();
                            userdefinedAttributes.Add(typeAttribute.UUID, aDBObject.GetAttribute(typeAttribute.UUID));

                            RemoveBackwardEdges(myGraphDBType.UUID, userdefinedAttributes, aDBObject.ObjectUUID, dbContext);
                        }

                        #endregion

                        aDBObject.RemoveAttribute(typeAttribute.UUID);
                        removedAttributes.Add(typeAttribute);
                    }
                    else
                    {
                        return new Exceptional<List<TypeAttribute>>(new Error_MandatoryConstraintViolation("Error in update statement. The attribute \"" + typeAttribute.Name + "\" is mandatory and can not be removed."));
                    }
                }
            }


            return new Exceptional<List<TypeAttribute>>(removedAttributes);
        }

        #endregion

    }
}

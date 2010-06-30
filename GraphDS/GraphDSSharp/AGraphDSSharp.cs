﻿/*
* sones GraphDB - OpenSource Graph Database - http://www.sones.com
* Copyright (C) 2007-2010 sones GmbH
*
* This file is part of sones GraphDB OpenSource Edition.
*
* sones GraphDB OSE is free software: you can redistribute it and/or modify
* it under the terms of the GNU Affero General Public License as published by
* the Free Software Foundation, version 3 of the License.
*
* sones GraphDB OSE is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU Affero General Public License for more details.
*
* You should have received a copy of the GNU Affero General Public License
* along with sones GraphDB OSE. If not, see <http://www.gnu.org/licenses/>.
*/

/* 
 * AGraphDSSharp
 * Achim Friedland, 2009 - 2010
 */

#region Usings

using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

using sones.GraphDS.API.CSharp.Reflection;
using sones.GraphDB.QueryLanguage.Result;
using sones.GraphDB.Transactions;
using sones.GraphFS.Transactions;

#endregion

namespace sones.GraphDS.API.CSharp
{

    public abstract class AGraphDSSharp
    {


        #region Query(myQuery)

        /// <summary>
        /// This will execute a usual query on the current GraphDBSharp implementation.
        /// Be aware that under some circumstances (e.g. REST) you will not get a valid result!
        /// </summary>
        /// <param name="myQuery"></param>
        /// <returns></returns>
        public abstract QueryResult Query(String myQuery);
        
        /// <summary>
        /// This will execute a usual query on the current GraphDBSharp implementation and returns the result in a xml representation
        /// </summary>
        /// <param name="myQuery"></param>
        /// <returns></returns>
        //internal abstract String QueryXml(String myQuery);

        public abstract SelectToObjectGraph QuerySelect(String myQuery);

        #endregion

        #region ActionQuery(myAction, myQuery)

        public QueryResult ActionQuery(Action<QueryResult> myAction, String myQuery)
        {

            var _QueryResult = Query(myQuery);
            
            if (myAction != null)
                myAction(_QueryResult);

            return _QueryResult;

        }

        #endregion


        #region CreateType/-Types

        #region CreateType(myTypeName)

        public CreateTypeQuery CreateType(String myTypeName)
        {
            return new CreateTypeQuery(this, myTypeName);
        }

        #endregion

        #region CreateType(myCreateTypeQuery)

        public QueryResult CreateType(CreateTypeQuery myCreateTypeQuery)
        {
            return Query(myCreateTypeQuery.ToString());
        }

        #endregion

        #region CreateType(myDBObject)

        public QueryResult CreateType(DBObject myDBObject)
        {
            return CreateTypes(null, new DBObject[] { myDBObject });
        }

        #endregion

        #region CreateType(myAction, myCreateTypeQuery)

        public QueryResult CreateType(Action<QueryResult> myAction, CreateTypeQuery myCreateTypeQuery)
        {
            return ActionQuery(myAction, myCreateTypeQuery.ToString());
        }

        #endregion

        #region CreateType(myAction, myDBObject)

        public QueryResult CreateType(Action<QueryResult> myAction, DBObject myDBObject)
        {

            var _Result = CreateType(myDBObject);

            myAction(_Result);

            return _Result;

        }

        #endregion


        #region CreateTypes(params myDBObjects)

        public QueryResult CreateTypes(params DBObject[] myDBObjects)
        {
            return CreateTypes(null, myDBObjects);
        }

        #endregion

        #region CreateTypes(myAction, params myDBObjects)

        public QueryResult CreateTypes(Action<QueryResult> myAction, params DBObject[] myDBObjects)
        {

            var _CreateTypesQuery = new StringBuilder("CREATE TYPES ");
            var _CreateIndiciesQueries = new List<String>();

            foreach (var _DBObject in myDBObjects)
            {

                _CreateTypesQuery.Append(_DBObject.CreateTypeQuery.Replace("CREATE TYPE ", "") + ", ");

                if (_DBObject.CreateIndicesQueries != null)
                    _CreateIndiciesQueries.AddRange(_DBObject.CreateIndicesQueries);

            }

            _CreateTypesQuery.Remove(_CreateTypesQuery.Length - 2, 2);

            var _QueryResult = Query(_CreateTypesQuery.ToString());
            if (myAction != null)
                myAction(_QueryResult);

            foreach (var _CreateIndexQuery in _CreateIndiciesQueries)
            {
                _QueryResult = Query(_CreateIndexQuery);
                if (myAction != null)
                    myAction(_QueryResult);
            }

            return _QueryResult;

        }

        #endregion

        #region CreateTypes(myAction, params myType)

        public QueryResult CreateTypes(Action<QueryResult> myAction, params Type[] myType)
        {
            return CreateTypes(myAction, myType.Select(type => Activator.CreateInstance(type) as DBObject).ToArray());
        }

        #endregion

        #region CreateTypes(params myCreateTypeQueries)

        public QueryResult CreateTypes(params CreateTypeQuery[] myCreateTypeQueries)
        {
            return CreateTypes(null, myCreateTypeQueries);
        }

        #endregion

        #region CreateTypes(myAction, params myCreateTypeQueries)

        public QueryResult CreateTypes(Action<QueryResult> myAction, params CreateTypeQuery[] myCreateTypeQueries)
        {

            var _CreateTypesQuery = new StringBuilder("CREATE TYPES ");

            foreach (var CreateTypeQuery in myCreateTypeQueries)
                _CreateTypesQuery.Append(CreateTypeQuery.GetGQLQuery().Replace("CREATE TYPE ", "") + ", ");

            _CreateTypesQuery.Remove(_CreateTypesQuery.Length - 2, 2);

            var _QueryResult = Query(_CreateTypesQuery.ToString());

            if (myAction != null)
                myAction(_QueryResult);

            return _QueryResult;

        }

        #endregion

        #endregion


        #region AlterType(myCreateTypeQuery)

        public AlterTypeQuery AlterType(CreateTypeQuery myCreateTypeQuery)
        {
            return new AlterTypeQuery(this, myCreateTypeQuery.Name);
        }

        #endregion

        #region AlterType(myTypeName)

        public AlterTypeQuery AlterType(String myTypeName)
        {
            return new AlterTypeQuery(this, myTypeName);
        }

        #endregion



        #region CreateIndex/-Indices

        #region CreateIndex()

        public CreateIndexQuery CreateIndex()
        {
            return new CreateIndexQuery(this);
        }

        #endregion

        #region CreateIndex(myIndexName)

        public CreateIndexQuery CreateIndex(String myIndexName)
        {
            return new CreateIndexQuery(this, myIndexName);
        }

        #endregion

        #region CreateIndices(myAction, myDBObject)

        public void CreateIndices(Action<QueryResult> myAction, DBObject myDBObject)
        {

            QueryResult _QueryResult = null;

            foreach (var _CreateIndexCommands in myDBObject.CreateIndicesQueries)
            {
                //_QueryResult = _PandoraDB.Query(_CreateIndexCommands, _SessionToken);
                myAction(_QueryResult);
            }

        }

        #endregion

        #endregion



        #region Select(mySelection)

        public SelectQuery Select(String mySelection)
        {
            return new SelectQuery(this, mySelection);
        }

        #endregion



        #region Insert(myAction, myDBObjects)

        public QueryResult Insert(Action<QueryResult> myAction, params DBObject[] myDBObjects)
        {
            return new QueryResult();
        }

        #endregion

        #region Insert(myAction, myGraphDBType, myValues)

        public QueryResult Insert(Action<QueryResult> myAction, CreateTypeQuery myGraphDBType, String myValues)
        {
            return ActionQuery(myAction, "INSERT INTO " + myGraphDBType.Name + " VALUES (" + myValues + ")");
        }

        #endregion

        #region Insert(myAction, myGraphDBType, myValues)

        public QueryResult Insert<T>(Action<QueryResult> myAction, CreateTypeQuery myGraphDBType, T myValues)
        {

            var _StringBuilder = new StringBuilder("INSERT INTO ").Append(myGraphDBType.Name).Append(" VALUES (");
            Object _Value;
            String _StringValue;


            foreach (var property in myValues.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                if (property.CanRead)
                {

                    _Value       = property.GetValue(myValues, null);
                    _StringValue = _Value as String;

                    if (_StringValue != null)
                        _StringValue = "'" + _StringValue + "'";
                    else
                        _StringValue = _Value.ToString();

                    _StringBuilder.Append(property.Name).Append(" = ").Append(_StringValue).Append(", ");

                }

            _StringBuilder.Length = _StringBuilder.Length - 2;

            _StringBuilder.Append(")");

            return ActionQuery(myAction, _StringBuilder.ToString());

        }

        #endregion


        public abstract DBTransaction BeginTransaction(Boolean myDistributed = false, Boolean myLongRunning = false, IsolationLevel myIsolationLevel = IsolationLevel.Serializable, String myName = "", DateTime? myCreated = null);


    }

}
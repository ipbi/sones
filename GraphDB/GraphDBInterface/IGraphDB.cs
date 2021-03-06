﻿/*
 * IGraphDBInterface
 * (c) Achim Friedland, 2008 - 2010
 */

#region Usings

using System;
using sones.GraphFS.DataStructures;
using sones.GraphFS.Session;

using sones.GraphDB.Result;
using sones.GraphDB.NewAPI;
using System.Collections.Generic;

#endregion

namespace sones.GraphDB
{

    public interface IGraphDB
    {

//        QueryResult AlterType(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName, System.Collections.Generic.List<sones.GraphDB.Managers.AlterType.AAlterTypeCommand> myAlterCommands);
//        DBTransaction BeginTransaction(SessionToken mySessionToken, DBContext dbContext, bool myDistributed = false, bool myLongRunning = false, sones.GraphFS.Transactions.IsolationLevel myIsolationLevel = IsolationLevel.Serializable, string myName = "", DateTime? timestamp = null);
//        QueryResult CreateIndex(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName, string myIndexName, string myIndexEdition, string myIndexType, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.IndexAttributeDefinition> myAttributeList);
//        QueryResult CreateTypes(SessionToken mySessionToken, DBContext mySessionContext, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.GraphDBTypeDefinition> myGraphDBTypeDefinitions);
        ObjectLocation DatabaseRootPath { get; }
//        System.Collections.Generic.Dictionary<string, sones.GraphDB.Settings.ADBSettingsBase> DBSettings { get; }
//        QueryResult Delete(SessionToken mySessionToken, DBContext mySessionContext, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.TypeReferenceDefinition> myTypeReferenceDefinitions, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.IDChainDefinition> myIDChainDefinitions, sones.GraphDB.Managers.Structures.BinaryExpressionDefinition myWhereExpression);
//        QueryResult Describe(SessionToken mySessionToken, DBContext mySessionContext, sones.GraphDB.Managers.Structures.Describe.ADescribeDefinition myDescribeDefinition);
//        QueryResult DropIndex(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName, string myIndexName, string myIndexEdition);
//        QueryResult DropType(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName);
//        QueryResult Export(SessionToken mySessionToken, DBContext mySessionContext, string myDumpFormat, string myDestination, sones.GraphDB.Interfaces.IDumpable myGrammar, System.Collections.Generic.IEnumerable<string> myTypes, sones.GraphDB.ImportExport.DumpTypes myDumpType, sones.GraphDB.ImportExport.VerbosityTypes myVerbosityType = VerbosityTypes.Errors);
//        System.Collections.Generic.IEnumerable<T2> FilterMapReduce<T1, T2>(DBContext dbContext, string myDBTypeName, Func<sones.GraphDB.ObjectManagement.DBObjectMR, bool> myFilter, Func<sones.GraphDB.ObjectManagement.DBObjectMR, T1> myMap, Func<System.Collections.Generic.IEnumerable<T1>, System.Collections.Generic.IEnumerable<T2>> myReduce);
//        sones.GraphFS.DataStructures.EntityUUID GetDatabaseUserID();
//        UUID GetDatabaseUUID();
//        sones.GraphDB.Transactions.DBTransaction GetLatestTransaction(SessionToken mySessionToken);
//        sones.Notifications.NotificationDispatcher GetNotificationDispatcher(SessionToken mySessionToken);
//        sones.Notifications.NotificationSettings GetNotificationSettings(SessionToken mySessionToken);
//        sones.GraphFS.Session.IGraphFSSession GraphFSSession { get; }
//        QueryResult Import(IGraphDBSession myGraphDBSession, SessionToken mySessionToken, DBContext mySessionContext, string myImportFormat, string myLocation, uint myParallelTasks = 1, System.Collections.Generic.IEnumerable<string> myComments = null, ulong? myOffset = null, ulong? myLimit = null, sones.GraphDB.ImportExport.VerbosityTypes myVerbosityType = VerbosityTypes.Errors);
//        QueryResult Insert(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.AAttributeAssignOrUpdate> myAttributeAssignList);
//        QueryResult InsertOrReplace(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.AAttributeAssignOrUpdate> myAttributeAssignList, sones.GraphDB.Managers.Structures.BinaryExpressionDefinition myWhereExpression);
//        QueryResult InsertOrUpdate(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.AAttributeAssignOrUpdate> myAttributeAssignList, sones.GraphDB.Managers.Structures.BinaryExpressionDefinition myWhereExpression);
//        bool IsWriteTransaction(SessionToken mySessionToken);
//        sones.Lib.ErrorHandling.Exceptional<object> MapAndReduce(DBContext dbContext, string myDBTypeName, Func<sones.GraphDB.ObjectManagement.DBObjectMR, object> myMap, Func<object, object> myReduce);
//        QueryResult RebuilIndices(SessionToken mySessionToken, DBContext mySessionContext, System.Collections.Generic.HashSet<string> myTypeNames = null);
//        QueryResult Replace(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.AAttributeAssignOrUpdate> myAttributeAssignList, sones.GraphDB.Managers.Structures.BinaryExpressionDefinition myWhereExpression);
//        QueryResult Select(SessionToken mySessionToken, DBContext mySessionContext, System.Collections.Generic.Dictionary<sones.GraphDB.Managers.Structures.AExpressionDefinition, string> mySelectedElements, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.TypeReferenceDefinition> myReferenceAndTypeList, sones.GraphDB.Managers.Structures.BinaryExpressionDefinition myWhereExpressionDefinition = null, System.Collections.Generic.List<sones.GraphDB.Managers.Structures.IDChainDefinition> myGroupBy = null, sones.GraphDB.Managers.Structures.BinaryExpressionDefinition myHaving = null, sones.GraphDB.Managers.Structures.OrderByDefinition myOrderByDefinition = null, ulong? myLimit = null, ulong? myOffset = null, long myResolutionDepth = -1, bool myRunWithTimeout = false);
//        void SetNotificationDispatcher(sones.Notifications.NotificationDispatcher myNotificationDispatcher, SessionToken mySessionToken);
//        void SetNotificationSettings(sones.Notifications.NotificationSettings myNotificationSettings, SessionToken mySessionToken);
//        QueryResult Setting(SessionToken mySessionToken, DBContext mySessionContext, sones.GraphDB.Structures.Enums.TypesOfSettingOperation myTypeOfSettingOperation, System.Collections.Generic.Dictionary<string, string> mySettings, sones.GraphDB.Managers.Structures.Setting.ASettingDefinition myASettingDefinition);
        void Shutdown(SessionToken mySessionToken);
//        QueryResult Truncate(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName);
        //        QueryResult Update(SessionToken mySessionToken, DBContext mySessionContext, string myTypeName, System.Collections.Generic.HashSet<sones.GraphDB.Managers.Structures.AAttributeAssignOrUpdateOrRemove> myListOfUpdates, sones.GraphDB.Managers.Structures.BinaryExpressionDefinition myWhereExpression);

        #region Traverser

        /// <summary>
        /// Starts a traversal and returns the found paths or an aggreagted result
        /// </summary>
        /// <typeparam name="T">The resulttype after applying the result transformation</typeparam>
        /// <param name="mySessionToken">The currenct session</param>
        /// <param name="myStartVertex">The starting vertex</param>
        /// <param name="TraversalOperation">BreathFirst|DepthFirst</param>
        /// <param name="myFollowThisEdge">Follow this edge? Based on its TYPE or any other property/characteristic...</param>
        /// <param name="myFollowThisPath">Follow this path (== actual path + NEW edge + NEW dbobject? Based on edge/object UUID, TYPE or any other property/characteristic...</param>
        /// <param name="myMatchEvaluator">Mhm, this vertex/path looks interesting!</param>
        /// <param name="myMatchAction">Hey! I have found something interesting!</param>
        /// <param name="myStopEvaluator">Will stop the traversal on a condition</param>
        /// <param name="myWhenFinished">Finish this traversal by calling (a result transformation method and) an external method...</param>
        /// <returns></returns>
        T TraversePath<T>(          SessionToken                             mySessionToken,
                                    Vertex                                 myStartVertex,
                                    TraversalOperation                       TraversalOperation  = TraversalOperation.BreathFirst,
                                    Func<DBPath, EdgeLabel, Boolean> myFollowThisEdge = null,
                                    Func<DBPath, EdgeLabel, Vertex, Boolean> myFollowThisPath = null,
                                    Func<DBPath, Boolean>                    myMatchEvaluator    = null,
                                    Action<DBPath>                           myMatchAction       = null,
                                    Func<TraversalState, Boolean>            myStopEvaluator     = null,
                                    Func<IEnumerable<DBPath>, T>             myWhenFinished      = null);

        /// <summary>
        /// Starts a traversal and returns the found vertices or an aggreagted result
        /// </summary>
        /// <typeparam name="T">The resulttype after applying the result transformation</typeparam>
        /// <param name="mySessionToken">The currenct session</param>
        /// <param name="myStartVertex">The starting vertex</param>
        /// <param name="TraversalOperation">BreathFirst|DepthFirst</param>
        /// <param name="myFollowThisEdge">Follow this edge? Based on its TYPE or any other property/characteristic...</param>
        /// <param name="myMatchEvaluator">Mhm, this vertex/path looks interesting!</param>
        /// <param name="myMatchAction">Hey! I have found something interesting!</param>
        /// <param name="myStopEvaluator">Will stop the traversal on a condition</param>
        /// <param name="myWhenFinished">Finish this traversal by calling (a result transformation method and) an external method...</param>
        /// <returns></returns>
        T TraverseVertex<T>(        SessionToken                            mySessionToken,
                                    Vertex                                myStartVertex,
                                    TraversalOperation                      TraversalOperation  = TraversalOperation.BreathFirst,
                                    Func<Vertex, EdgeLabel, Boolean> myFollowThisEdge = null,
                                    Func<Vertex, Boolean>                 myMatchEvaluator    = null,
                                    Action<Vertex>                        myMatchAction       = null,
                                    Func<TraversalState, Boolean>           myStopEvaluator     = null,
                                    Func<IEnumerable<Vertex>, T>          myWhenFinished      = null);

        #endregion
    }

}

﻿/* 
 * XML_IO_Extensions
 * Achim 'ahzf' Friedland, 2009 - 2010
 */

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using sones.GraphDB.NewAPI;
using sones.GraphDB.ObjectManagement;
using sones.GraphDB.Result;
using sones.GraphFS.DataStructures;
using sones.GraphFS.Objects;
using sones.Lib;

#endregion

namespace sones.GraphIO.XML
{

    /// <summary>
    /// Extension methods to transform datastructures into an
    /// application/xml representation an vice versa.
    /// </summary>

    public static class XML_IO_Extensions
    {

        #region ToXML(this myINode)

        public static XElement ToXML(this INode myINode)
        {

            return
                new XElement("INode",
                    new XAttribute("Version",               myINode.StructureVersion),
                    new XAttribute("ObjectUUID",            myINode.ObjectUUID),

                    new XElement("CreationTime",            myINode.CreationTime),
                    new XElement("LastAccessTime",          myINode.LastAccessTime),
                    new XElement("LastModificationTime",    myINode.LastModificationTime),
                    new XElement("DeletionTime",            myINode.DeletionTime),
                    new XElement("ReferenceCount",          myINode.ReferenceCount),
                    new XElement("ObjectSize",              myINode.ObjectSize),
                    new XElement("IntegrityCheckAlgorithm", myINode.IntegrityCheckAlgorithm),
                    new XElement("EncryptionAlgorithm",     myINode.EncryptionAlgorithm),

                    new XElement("ObjectLocatorPosition",
                        new XAttribute("Length",            myINode.ObjectLocatorLength),
                        new XAttribute("NumberOfCopies",    myINode.ObjectLocatorCopies),

                        from _ExtendedPosition in myINode.ObjectLocatorPositions
                        select new XElement("ExtendedPosition",
                            new XAttribute("StorageID",     _ExtendedPosition.StorageUUID),
                            new XAttribute("Position",      _ExtendedPosition.Position)))

            );

        }

        #endregion

        #region ToXML(this ObjectLocator)

        public static XElement ToXML(this ObjectLocator myObjectLocator)
        {

            return
                new XElement("ObjectLocator",
                    new XAttribute("Version", myObjectLocator.StructureVersion),
                    new XAttribute("ObjectUUID", myObjectLocator.ObjectUUID.ToString()),

                new XElement("ObjectStreams",

                    from _ObjectStream in myObjectLocator
                    select
                        new XElement("ObjectStream",
                        new XAttribute("Name", _ObjectStream.Key),
                        new XAttribute("Type", "tobedone!"),

                        new XElement("ObjectEditions",
                        new XAttribute("DefaultEdition", _ObjectStream.Value.DefaultEditionName),

                        from _ObjectEdition in _ObjectStream.Value
                        select
                            new XElement("ObjectEdition",
                            new XAttribute("Name", _ObjectEdition.Key),
                            new XAttribute("IsDeleted", _ObjectEdition.Value.IsDeleted),

                            new XElement("ObjectRevisions",
                            new XAttribute("MinNumberOfRevisions", _ObjectEdition.Value.MinNumberOfRevisions),
                            new XAttribute("MaxNumberOfRevisions", _ObjectEdition.Value.MaxNumberOfRevisions),
                            new XAttribute("MinRevisionDelta", _ObjectEdition.Value.MinRevisionDelta),
                            new XAttribute("MaxRevisionAge", _ObjectEdition.Value.MaxRevisionAge),

                            from _ObjectRevisionsEnumerator in _ObjectEdition.Value
                            select new XElement("ObjectRevision",
                                new XAttribute("RevisionID", _ObjectRevisionsEnumerator.Key),

                                new XElement("ParentRevisions",
                                from _ParentRevisionID in _ObjectRevisionsEnumerator.Value.ParentRevisionIDs
                                select
                                    new XElement("ParentRevision",
                                    new XAttribute("RevisionID", _ParentRevisionID)



                                    )))))))));


            //                            XMLString.AppendLine("<ParentRevisions>".SpacingLeft(16));

            //                            if (ObjectEditionsEnumerator.Value._ParentRevisions.Count > 0 && ObjectEditionsEnumerator.Value._ParentRevisions.ContainsKey(ObjectRevisionsEnumerator.Key))
            //                                foreach (RevisionID _ParentRevision in ObjectEditionsEnumerator.Value._ParentRevisions[ObjectRevisionsEnumerator.Key])
            //                                    XMLString.AppendFormat("<ParentRevision Timestamp=\"{0}\" />".SpacingLeft(18), _ParentRevision.ToString()); XMLString.AppendLine();

            //                            XMLString.AppendLine("</ParentRevisions>".SpacingLeft(16));

            //                            XMLString.AppendFormat("<ObjectCopies MinNumberOfCopies=\"{0}\" MaxNumberOfCopies=\"{1}\" CacheUUID=\"{2}\">".SpacingLeft(16), ObjectRevisionsEnumerator.Value.MinNumberOfCopies, ObjectRevisionsEnumerator.Value.MaxNumberOfCopies, ObjectRevisionsEnumerator.Value.CacheUUID); XMLString.AppendLine();

            //                            #region Write ObjectStreams

            ////                            if (ObjectRevisionsEnumerator.Value.Co != null)
            //                                foreach (ObjectStream ObjectStream in ObjectRevisionsEnumerator.Value)
            //                                {

            //                                    XMLString.Append("<ObjectStream".SpacingLeft(18));
            //                                    XMLString.AppendFormat(" Algorithm=\"{0}\"",              ObjectStream.Compression);
            //                                    XMLString.AppendFormat(" ForwardErrorCorrection=\"{0}\"", ObjectStream.ForwardErrorCorrection);
            //                                    XMLString.AppendFormat(" IntegrityCheckValue=\"{0}\"",    ByteArrayHelper.ByteArrayToFormatedString(ObjectStream.IntegrityCheckValue));
            //                                    XMLString.AppendFormat(" ObjectUUID=\"{0}\"",             ObjectStream.ObjectUUID);
            //                                    XMLString.AppendFormat(" Redundancy=\"{0}\"",             ObjectStream.Redundancy);
            //                                    XMLString.AppendFormat(" ReservedLength=\"{0}\"",         ObjectStream.ReservedLength);
            //                                    XMLString.AppendFormat(" StreamLength=\"{0}\"",           ObjectStream.StreamLength);
            //                                    XMLString.AppendLine(">");

            //                                    #region Write AccessRights

            //                                    if (ObjectStream.AccessRights != null)
            //                                        if (ObjectStream.AccessRights.Count > 0)
            //                                        {

            //                                            XMLString.AppendLine("<AccessRights>".SpacingLeft(20));

            //                                            foreach (AccessRight _AccessRight in ObjectStream.AccessRights)
            //                                            {
            //                                                XMLString.AppendFormat("<AccessRight AccessFlags=\"{0}\" EncryptionParameters=\"{1}\" UserID=\"{2}\" />".SpacingLeft(20), _AccessRight.AccessFlags.ToString(), _AccessRight.EncryptionParameters.ToString(), _AccessRight.UserID); XMLString.AppendLine();
            //                                            }

            //                                            XMLString.AppendLine("</AccessRights>".SpacingLeft(20));

            //                                        }

            //                                        else XMLString.AppendLine("<AccessRights />".SpacingLeft(20));

            //                                    else XMLString.AppendLine("<AccessRights />".SpacingLeft(20));

            //                                    #endregion

            //                                    #region AvailableStorageIDs

            //                                    if (ObjectStream.AvailableStorageIDs != null)
            //                                        if (ObjectStream.AvailableStorageIDs.Count > 0)
            //                                        {


            //                                            XMLString.AppendLine("<AvailableStorageIDs>".SpacingLeft(20));

            //                                            foreach (UInt64 _StorageID in ObjectStream.AvailableStorageIDs)
            //                                            {
            //                                                XMLString.AppendFormat("<AvailableStorageID StorageID=\"{0}\">".SpacingLeft(20), _StorageID);
            //                                                XMLString.AppendLine();
            //                                            }

            //                                            XMLString.AppendLine("</AvailableStorageIDs>".SpacingLeft(20));

            //                                        }

            //                                        else XMLString.AppendLine("<AvailableStorageIDs />".SpacingLeft(20));

            //                                    else XMLString.AppendLine("<AvailableStorageIDs />".SpacingLeft(20));

            //                                    #endregion

            //                                    #region BlockIntegrityArrays

            //                                    //if (ObjectStream.BlockIntegrityArrays != null)
            //                                        //if (ObjectStream.BlockIntegrityArrays.Count > 0)
            //                                        //{


            //                                        //    XMLString.AppendLine("<BlockIntegrityArrays>".SpacingLeft(20));

            //                                        //    foreach (BlockIntegrity _BlockIntegrity in ObjectStream.BlockIntegrityArrays)
            //                                        //    {
            //                                        //        XMLString.AppendFormat("<BlockIntegrity XXX=\"{0}\">".SpacingLeft(20), _BlockIntegrity);
            //                                        //        XMLString.AppendLine();
            //                                        //    }

            //                                        //    XMLString.AppendLine("</BlockIntegrityArrays>".SpacingLeft(20));

            //                                        //}

            //                                        //else XMLString.AppendLine("<BlockIntegrityArrays />".SpacingLeft(20));

            //                                    //else
            //                                    XMLString.AppendLine("<BlockIntegrityArrays />".SpacingLeft(20));

            //                                    #endregion

            //                                    #region Write ObjectExtent

            //                                    XMLString.AppendLine("<Extents>".SpacingLeft(20));

            //                                    if (ObjectStream.Extents != null)
            //                                        foreach (ObjectExtent ObjectExtent in ObjectStream.Extents)
            //                                        {
            //                                            XMLString.Append("<Extent".SpacingLeft(22));
            //                                            XMLString.AppendFormat(" Length=\"{0}\"",               ObjectExtent.Length);
            //                                            XMLString.AppendFormat(" LogicalPosition=\"{0}\"",      ObjectExtent.LogicalPosition);
            //                                            XMLString.AppendFormat(" StorageID=\"{0}\"",            ObjectExtent.StorageID);
            //                                            XMLString.AppendFormat(" PhysicalPosition=\"{0}\"",     ObjectExtent.PhysicalPosition);
            //                                            XMLString.AppendFormat(" NextExtent_StorageID=\"{0}\"", ObjectExtent.NextExtent.StorageID);
            //                                            XMLString.AppendFormat(" NextExtent_Position=\"{0}\"",  ObjectExtent.NextExtent.Position);
            //                                            XMLString.AppendLine(" />");
            //                                        }

            //                                    XMLString.AppendLine("</Extents>".SpacingLeft(20));

            //                                    #endregion

            //                                    XMLString.AppendLine("</ObjectStream>".SpacingLeft(18));

            //                                }

            //                        #endregion

            //                        XMLString.AppendLine("</ObjectCopies>".SpacingLeft(16));
            //                        XMLString.AppendLine("</ObjectRevisions>".SpacingLeft(14));

            //                    }

            //                    #endregion

            //                    XMLString.AppendLine("</ObjectRevisions>".SpacingLeft(12));
            //                    XMLString.AppendLine("</ObjectEditions>".SpacingLeft(10));

            //                }

            //                #endregion

            //                XMLString.AppendLine("</ObjectEditions>".SpacingLeft(8));
            //                XMLString.AppendLine("</ObjectStream>".SpacingLeft(6));

            //            }

            //            XMLString.AppendLine("</ObjectStreamTypes>".SpacingLeft(4));
            //            XMLString.AppendLine("</ObjectLocator>".SpacingLeft(2));

            //            return XMLString.ToString();

        }

        #endregion

        #region ToXML(this myAFSObject)

        public static XElement ToXML(this AFSObject myAFSObject)
        {
            return
                new XElement("AFSObject"
            );
        }

        #endregion


        #region ToXML(this myQueryResult)

        public static XElement ToXML(this QueryResult myQueryResult)
        {

            // root element...
            var _Query = new XElement("queryresult", new XAttribute("version", "1.0"));


            // query --------------------------------
            _Query.Add(new XElement("query", myQueryResult.Query));

            // result -------------------------------
            _Query.Add(new XElement("result", myQueryResult.ResultType));

            // duration -----------------------------
            _Query.Add(new XElement("duration", new XAttribute("resolution", "ms"), myQueryResult.Duration));

            // warnings -----------------------------
            _Query.Add(new XElement("warnings",
                from _Warning in myQueryResult.Warnings
                select
                    new XElement("warning",
                    new XAttribute("code", _Warning.GetType().FullName),
                    _Warning.ToString())
                ));

            // errors -------------------------------
            _Query.Add(new XElement("errors",
                from _Error in myQueryResult.Errors
                select
                    new XElement("error",
                    new XAttribute("code", _Error.GetType().FullName),
                    _Error.ToString())
                ));

            // results ------------------------------
            _Query.Add(new XElement("results", GetXMLFromResult(myQueryResult.Vertices)));
            
            return _Query;

        }

        #endregion

        private static IEnumerable<XElement> GetXMLFromResult(IEnumerable<Vertex> myVertices)
        {

            if (myVertices != null)
            {
                foreach (var _XElement in from _Vertex in myVertices select _Vertex.ToXML())
                {
                    yield return _XElement;
                }
            }

            yield break;

        }

        #region ToXML(this myVertex)

        public static XElement ToXML(this Vertex myVertex)
        {
            return myVertex.ToXML(false);
        }

        #endregion

        #region (private) ToXML(this myVertex, myRecursion)

        private static XElement ToXML(this Vertex myVertex, Boolean myRecursion)
        {

            Type _AttributeType       = null;
            var _AttributeTypeString  = "";
            var __Vertex              = new XElement("vertex");

            VertexGroup             _VertexGroup           = null;
            Vertex_WeightedEdges    _WeightedDBObject      = null;
            IEnumerable<Vertex>     _Vertices              = null;
            IEnumerable<Object>     _AttributeValueList    = null;
            IGetName                _IGetName              = null;

            #region VertexHavingWeightedEdges

            var _WeightedDBObject1 = myVertex as Vertex_WeightedEdges;

            if (_WeightedDBObject1 != null)
            {
                __Vertex.Add(new XElement("edgelabel", new XElement("attribute", new XAttribute("name", "weight"), new XAttribute("type", _WeightedDBObject1.TypeName), _WeightedDBObject1.Weight)));
            }

            #endregion

            #region VertexGroup

            var _GroupedDBObject1 = myVertex as VertexGroup;

            if (_GroupedDBObject1 != null)
            {

                var _groupedElements = new XElement("attribute", new XAttribute("name", "group"));

                var _groupedElementEdgeLabel = new XElement("edgelabel");

                foreach (var _Vertex in _GroupedDBObject1.GroupedVertices)
                    _groupedElements.Add(_Vertex.ToXML());

                _groupedElementEdgeLabel.Add(_groupedElements);

                __Vertex.Add(_groupedElementEdgeLabel);
            }

            #endregion

            foreach (var _Attribute in myVertex)
            {

                if (_Attribute.Value != null)
                {

                    #region VertexGroup

                    _VertexGroup = _Attribute.Value as VertexGroup;

                    if (_VertexGroup != null)
                    {

                        var _Grouped = new XElement("grouped");

                        if (_VertexGroup.GroupedVertices != null)
                            foreach (var _Vertex in _VertexGroup.GroupedVertices)
                                _Grouped.Add(_Vertex.ToXML());

                        __Vertex.Add(_Grouped);

                        continue;

                    }

                    #endregion

                    #region Vertex_WeightedEdges

                    _WeightedDBObject = _Attribute.Value as Vertex_WeightedEdges;

                    if (_WeightedDBObject != null)
                    {
                        __Vertex.Add(new XElement("edgelabel", new XElement("attribute", new XAttribute("name", "weight"), new XAttribute("type", _WeightedDBObject1.TypeName), _WeightedDBObject1.Weight)));
                        continue;
                    }

                    #endregion

                    #region IEnumerable<Vertex>

                    _Vertices = _Attribute.Value as IEnumerable<Vertex>;

                    if (_Vertices != null && _Vertices.Count() > 0)
                    {

                        var _EdgeInfo = (_Attribute.Value as Edge);
                        var _EdgeType = (_EdgeInfo != null) ? _EdgeInfo.EdgeTypeName : "";

                        var _ListAttribute = new XElement("edge",
                            new XAttribute("name", _Attribute.Key.EscapeForXMLandHTML()),
                            new XAttribute("type", _EdgeType));

                        // An edgelabel for all edges together...
                        _ListAttribute.Add(new XElement("hyperedgelabel"));

                        foreach (var _DBObjectReadout in _Vertices)
                            _ListAttribute.Add(_DBObjectReadout.ToXML());

                        __Vertex.Add(_ListAttribute);
                        continue;

                    }

                    #endregion

                    #region Attribute Type (may be generic!)

                    _AttributeType = _Attribute.Value.GetType();

                    if (_AttributeType.IsGenericType)
                    {
                        

                        _AttributeTypeString = _AttributeType.Name;
                        _AttributeTypeString = _AttributeTypeString.Substring(0, _AttributeTypeString.IndexOf('`')).ToUpper();
                        _AttributeTypeString += "&lt;";
                        _AttributeTypeString += _AttributeType.GetGenericArguments()[0].Name;
                        _AttributeTypeString += "&gt;";
                    }

                    else
                        _AttributeTypeString = _AttributeType.Name;

                    #endregion

                    #region Add result to _DBObject

                    var _AttributeTag = new XElement("attribute",
                        new XAttribute("name", _Attribute.Key.EscapeForXMLandHTML()),
                        new XAttribute("type", _AttributeTypeString)
                    );

                    __Vertex.Add(_AttributeTag);

                    #endregion

                    #region Attribute value and attribute value lists

                    _AttributeValueList = _Attribute.Value as IEnumerable<Object>;

                    if (_AttributeValueList != null)
                    {
                        foreach (var _Value in _AttributeValueList)
                        {

                            // _Value.ToString() may not always return the information we need!
                            _IGetName = _Value as IGetName;
                            if (_IGetName != null)
                                _AttributeTag.Add(new XElement("item", _IGetName.Name));
                            else
                                _AttributeTag.Add(new XElement("item", _Value.ToString().EscapeForXMLandHTML()));

                        }
                    }

                    else
                    {

                        // _Attribute.Value.ToString() may not always return the information we need!
                        _IGetName = _Attribute.Value as IGetName;

                        if (_IGetName != null)
                            _AttributeTag.Value = _IGetName.Name;
                        else
                            _AttributeTag.Value = _Attribute.Value.ToString().EscapeForXMLandHTML();

                    }

                    #endregion

                }

            }

            return __Vertex;

        }

        #endregion


        #region BuildXMLDocument(params myXElements)

        public static XDocument BuildXMLDocument(params XElement[] myXElements)
        {

            var _XMLDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));

            var _Sones       = new XElement("sones",   new XAttribute("version", "1.0"));
            var _GraphDB     = new XElement("graphdb", new XAttribute("version", "1.0"));

            foreach (var _XElement in myXElements)
                _GraphDB.Add(_XElement);

            _Sones.Add(_GraphDB);
            _XMLDocument.Add(_Sones);

            return _XMLDocument;

        }

        #endregion

        #region XMLDocument2String(this myXDocument)

        public static String XMLDocument2String(this XDocument myXDocument)
        {

         //   var _StringWriter = new StringWriter();

         //   var _XmlWriterSettings = new XmlWriterSettings()
         //   {
         //       Encoding         = Encoding.UTF8,
         //   //    ConformanceLevel = ConformanceLevel.Document,
         //   //    Indent           = true
         //   };

         //   var writer = XmlWriter.Create(_StringWriter, _XmlWriterSettings);
         //   myXDocument.Save(writer);

         //   return _StringWriter.GetStringBuilder().ToString();

            var _String = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" + Environment.NewLine;
            _String += myXDocument.ToString();

            Console.WriteLine(_String);

            return _String;

        }

        #endregion


        #region AddValidationInformation(myValidationType, params myXElements)

        public static String AddValidationInformation(XMLValidationTypes myValidationType, params XElement[] myXElements)
        {

            #region XML Header

            var _XMLString = new StringBuilder();
            _XMLString.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

            #endregion


            #region DTD

            if (myValidationType == XMLValidationTypes.DTD)
            {

                _XMLString.AppendLine("<!DOCTYPE sones [");

                _XMLString.AppendLine("<!ELEMENT sones                   (graphfs|graphdb)+>");
                _XMLString.AppendLine("<!ATTLIST sones");
                _XMLString.AppendLine("version                           CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                #region GraphFS

                _XMLString.AppendLine("<!ELEMENT GraphFS                 (INode|ObjectLocator)+>");
                _XMLString.AppendLine("<!ATTLIST GraphFS");
                _XMLString.AppendLine("version                           CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                #region INode

                _XMLString.AppendLine("<!ELEMENT INode                   (CreationTime, LastAccessTime, LastModificationTime, DeletionTime, ReferenceCount, ObjectSize, IntegrityCheckAlgorithm, EncryptionAlgorithm, ObjectLocatorPosition)>");
                _XMLString.AppendLine("<!ATTLIST INode");
                _XMLString.AppendLine("Version                           CDATA #REQUIRED");
                _XMLString.AppendLine("ObjectUUID                        CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT CreationTime            (#PCDATA)>");
                _XMLString.AppendLine("<!ELEMENT LastAccessTime          (#PCDATA)>");
                _XMLString.AppendLine("<!ELEMENT LastModificationTime    (#PCDATA)>");
                _XMLString.AppendLine("<!ELEMENT DeletionTime            (#PCDATA)>");
                _XMLString.AppendLine("<!ELEMENT ReferenceCount          (#PCDATA)>");
                _XMLString.AppendLine("<!ELEMENT ObjectSize              (#PCDATA)>");
                _XMLString.AppendLine("<!ELEMENT IntegrityCheckAlgorithm (#PCDATA)>");
                _XMLString.AppendLine("<!ELEMENT EncryptionAlgorithm     (#PCDATA)>");

                _XMLString.AppendLine("<!ELEMENT ObjectLocatorPosition   (ExtendedPosition)+>");
                _XMLString.AppendLine("<!ATTLIST ObjectLocatorPosition");
                _XMLString.AppendLine("Length                            CDATA #REQUIRED");
                _XMLString.AppendLine("Reserve                           CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT ExtendedPosition        EMPTY>");
                _XMLString.AppendLine("<!ATTLIST ExtendedPosition");
                _XMLString.AppendLine("StorageID                         CDATA #REQUIRED");
                _XMLString.AppendLine("Position                          CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                #endregion

                #region ObjectLocator

                _XMLString.AppendLine("<!ELEMENT ObjectLocator           (ObjectStreamTypes)>");
                _XMLString.AppendLine("<!ATTLIST ObjectLocator");
                _XMLString.AppendLine("Version                           CDATA #REQUIRED");
                _XMLString.AppendLine("ObjectUUID                        CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT ObjectStreamTypes       (ObjectStream)+>");

                _XMLString.AppendLine("<!ELEMENT ObjectStream            (ObjectEditions)>");
                _XMLString.AppendLine("<!ATTLIST ObjectStream");
                _XMLString.AppendLine("Type                              CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT ObjectEditions          (ObjectEditions)+>");
                _XMLString.AppendLine("<!ATTLIST ObjectEditions");
                _XMLString.AppendLine("DefaultEdition                    CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT ObjectEditions          (ObjectRevisions)>");
                _XMLString.AppendLine("<!ATTLIST ObjectEditions");
                _XMLString.AppendLine("myLogin                           CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT ObjectRevisions         (ObjectRevisions)+>");
                _XMLString.AppendLine("<!ATTLIST ObjectRevisions");
                _XMLString.AppendLine("MinNumberOfRevisions              CDATA #REQUIRED");
                _XMLString.AppendLine("MaxNumberOfRevisions              CDATA #REQUIRED");
                _XMLString.AppendLine("MinRevisionDelta                  CDATA #REQUIRED");
                _XMLString.AppendLine("MaxRevisionAge                    CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT ObjectRevisions         (ObjectCopies)+>");
                _XMLString.AppendLine("<!ATTLIST ObjectRevisions");
                _XMLString.AppendLine("RevisionID                        CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT ObjectCopies            (ObjectStream)+>");
                _XMLString.AppendLine("<!ATTLIST ObjectCopies");
                _XMLString.AppendLine("MinNumberOfCopies                 CDATA #REQUIRED");
                _XMLString.AppendLine("MaxNumberOfCopies                 CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT ObjectStream            (AccessRights, AvailableStorageIDs, BlockIntegrityArrays, Extents)>");
                _XMLString.AppendLine("<!ATTLIST ObjectStream");
                _XMLString.AppendLine("Algorithm                         CDATA #REQUIRED");
                _XMLString.AppendLine("ForwardErrorCorrection            CDATA #REQUIRED");
                _XMLString.AppendLine("IntegrityCheckValue               CDATA #REQUIRED");
                _XMLString.AppendLine("ObjectUUID                        CDATA #REQUIRED");
                _XMLString.AppendLine("Redundancy                        CDATA #REQUIRED");
                _XMLString.AppendLine("ReservedLength                    CDATA #REQUIRED");
                _XMLString.AppendLine("StreamLength                      CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT AccessRights            EMPTY>");
                _XMLString.AppendLine("<!ELEMENT AvailableStorageIDs     EMPTY>");
                _XMLString.AppendLine("<!ELEMENT BlockIntegrityArrays    EMPTY>");
                _XMLString.AppendLine("<!ELEMENT Extents                 (Extent)+>");

                _XMLString.AppendLine("<!ELEMENT Extent                  EMPTY>");
                _XMLString.AppendLine("<!ATTLIST Extent");
                _XMLString.AppendLine("Length                            CDATA #REQUIRED");
                _XMLString.AppendLine("LogicalPosition                   CDATA #REQUIRED");
                _XMLString.AppendLine("StorageID                         CDATA #REQUIRED");
                _XMLString.AppendLine("PhysicalPosition                  CDATA #REQUIRED");
                _XMLString.AppendLine("NextExtent_StorageID              CDATA #REQUIRED");
                _XMLString.AppendLine("NextExtent_Position               CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                #endregion

                #endregion

                #region GraphDB

                _XMLString.AppendLine("<!ELEMENT graphdb                 (queryresult)+>");
                _XMLString.AppendLine("<!ATTLIST graphdb");
                _XMLString.AppendLine("version                           CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT queryresult             (query, result, duration)>");
                _XMLString.AppendLine("<!ATTLIST queryresult");
                _XMLString.AppendLine("version                           CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                _XMLString.AppendLine("<!ELEMENT query                   (#PCDATA)>");
                _XMLString.AppendLine("<!ELEMENT result                  (#PCDATA)>");

                _XMLString.AppendLine("<!ELEMENT duration                (#PCDATA)>");
                _XMLString.AppendLine("<!ATTLIST duration");
                _XMLString.AppendLine("resolution                        CDATA #REQUIRED");
                _XMLString.AppendLine(">");

                #endregion

                _XMLString.AppendLine("]>");

            }

            #endregion

            #region DTD_URL

            else if (myValidationType == XMLValidationTypes.DTD_URL)
                _XMLString.AppendLine("<!DOCTYPE spec SYSTEM \"http://www.sones.de/Graph/XML/DTD/GraphXML.dtd\">");

            #endregion

            #region Schema

            else if (myValidationType == XMLValidationTypes.Schema)
            {
            }

            #endregion

            #region Schema_URL

            else if (myValidationType == XMLValidationTypes.Schema_URL)
            {
            }

            #endregion


            #region Print <Graph ...> ... </Graph>

            _XMLString.AppendFormat("<Graph>"); _XMLString.AppendLine();

            foreach (var _XElement in myXElements)
                _XMLString.Append(_XElement.ToString());

            _XMLString.AppendLine("</Graph>");
            _XMLString.AppendLine("");

            #endregion

            return _XMLString.ToString();

        }

        #endregion

    }

}

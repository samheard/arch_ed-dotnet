'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'

Option Strict On

Public Class RmReference
    Inherits RmElement
    Private mPath As String
    Private mElement As RmElement

    Public Overrides ReadOnly Property Type() As StructureType
        Get
            Return StructureType.Reference
        End Get
    End Property

    Public Property Path() As String
        Get
            Return mPath
        End Get
        Set(ByVal Value As String)
            If Value.EndsWith("/") Then
                Value = Value.TrimEnd(CType("/", Char))
            End If

            Dim i As Integer = Value.LastIndexOf("/")
            Dim s As String = Value.Substring(i + 1)
            i = s.IndexOf("[")
            s = s.Substring(i + 1)
            i = s.IndexOf("]")

            If i >= 0 Then
                s = s.Substring(0, i)
            End If

            sNodeId = s
            mPath = Value
        End Set
    End Property

    Public Overrides ReadOnly Property IsReference() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Property HasReferences() As Boolean
        ' cannot be true
        Get
            Return False
        End Get
        Set(ByVal Value As Boolean)
            boolHasReferences = False
        End Set
    End Property

    Public Overrides Function CanChangeDataType(ByVal archetypeSpecialisationDepth As Integer) As Boolean
        Return False
    End Function

    Public Overrides ReadOnly Property DataType() As String
        Get
            Debug.Assert(Not mElement Is Nothing, "rm Argument is nothing")
            Return mElement.DataType
        End Get
    End Property

    Public Overrides Property Constraint() As Constraint
        Get
            Return mElement.Constraint
        End Get
        Set(ByVal Value As Constraint)
            mElement.Constraint = Value
        End Set
    End Property

    Sub SetElement(ByVal rm As RmElement)
        Debug.Assert(Not rm Is Nothing, "rm Argument is nothing")

        If sNodeId <> rm.NodeId Then
            If rm.HasNameConstraint Then
                mNameConstraint = rm.NameConstraint
            End If

            mStructureType = StructureType.Element
            sNodeId = rm.NodeId
            cOccurrences = rm.Occurrences.Copy()
        End If

        mElement = rm
    End Sub

    Sub New()
        'allows reference to be created before element is known
        MyBase.New("?")
    End Sub

    Sub New(ByRef el As RmElement)
        MyBase.New(el)
        el.HasReferences = True
        mElement = el
    End Sub

End Class

'
'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1/GPL 2.0/LGPL 2.1
'
'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/
'
'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.
'
'The Original Code is RmReference.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'
'Alternatively, the contents of this file may be used under the terms of
'either the GNU General Public License Version 2 or later (the "GPL"), or
'the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
'in which case the provisions of the GPL or the LGPL are applicable instead
'of those above. If you wish to allow use of your version of this file only
'under the terms of either the GPL or the LGPL, and not to allow others to
'use your version of this file under the terms of the MPL, indicate your
'decision by deleting the provisions above and replace them with the notice
'and other provisions required by the GPL or the LGPL. If you do not delete
'the provisions above, a recipient may use your version of this file under
'the terms of any one of the MPL, the GPL or the LGPL.
'
'***** END LICENSE BLOCK *****
'

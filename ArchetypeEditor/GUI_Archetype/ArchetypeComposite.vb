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

Public Class ArchetypeComposite
    Inherits ArchetypeNodeAbstract

    Private mIsFixed As Boolean
    Private mCardinality As New RmCardinality(1)

    Public Property Cardinality() As RmCardinality
        Get
            Return mCardinality
        End Get
        Set(ByVal Value As RmCardinality)
            mCardinality = Value
        End Set
    End Property

    Public Property IsOrdered() As Boolean
        Get
            Return mCardinality.Ordered
        End Get
        Set(ByVal Value As Boolean)
            mCardinality.Ordered = Value
        End Set
    End Property

    Public Overrides Property Constraint() As Constraint
        Get
            Dim result As Constraint = Nothing

            If Item.Type = StructureType.Cluster Then
                result = New Constraint_Cluster
            End If

            Return result
        End Get
        Set(ByVal value As Constraint)
        End Set
    End Property

    Public Overrides Function Copy() As ArchetypeNode
        Return New ArchetypeComposite(Me)
    End Function

    Public Overrides Function ToRichText(ByVal level As Integer) As String
        Dim result, s1 As String
        Dim nl As String = Chr(10) & Chr(13)

        result = (Space(3 * level) & "\ul " & RichTextBoxUnicode.EscapedRtfString(mFileManager.OntologyManager.GetText(NodeId)) & "\ulnone  (" & mItem.Occurrences.ToString & ")\par") & nl
        result &= (Space(3 * level) & "\i    - " & RichTextBoxUnicode.EscapedRtfString(mFileManager.OntologyManager.GetDescription(NodeId)) & "\i0\par") & nl

        s1 = "\cf2 Items \cf0"

        If IsOrdered Then
            s1 &= " ordered"
        End If

        If mIsFixed Then
            s1 &= " fixed"
        End If

        s1 = s1.Trim
        s1 &= "\par"
        result &= Space(3 * (level + 1)) & s1
        Return result
    End Function

    Public Overrides Function ToHTML(ByVal level As Integer, ByVal showComments As Boolean) As String
        Dim s As String
        Dim result As System.Text.StringBuilder = New System.Text.StringBuilder("<tr>")

        Select Case mItem.Type
            Case StructureType.Cluster
                result.AppendFormat("{0}<td><table><tr><td width=""{1}""></td><td><img border=""0"" src=""Images/compound.gif"" width=""32"" height=""32"" align=""middle""><b><i>{2}</i></b></td></table></td>", Environment.NewLine, (level * 20).ToString, Text)
                s = Filemanager.GetOpenEhrTerm(313, "Cluster")

            Case StructureType.SECTION
                result.AppendFormat("{0}<td><table><tr><td width=""{1}""></td><td><img border=""0"" src=""Images/section.gif"" width=""32"" height=""32"" align=""middle""><b><i>{2}</i></b></td></table></td>", Environment.NewLine, (level * 20).ToString, Text)
                s = Filemanager.GetOpenEhrTerm(314, "Section")
            Case Else
                Debug.Assert(False)
                Return ""
        End Select

        result.AppendFormat("{0}<td>{1}</td>", Environment.NewLine, Description)
        result.AppendFormat("{0}<td><b><i>{1}</b></i><br>", Environment.NewLine, s)
        result.AppendFormat("{0}{1}", Environment.NewLine, mItem.Occurrences.ToString)

        If mIsFixed Then
            result.Append(", fixed")
        End If

        If IsOrdered Then
            result.Append(", ordered")
        End If

        result.Append("</td><td>&nbsp;</td>")

        If showComments Then
            Dim commentString As String = Comment

            If commentString = "" Then
                commentString = "&nbsp;"
            End If
            result.AppendFormat("{0}<td>{1}</td>", Environment.NewLine, commentString)
        End If

        Return result.ToString
    End Function

    Public Sub New(ByVal text As String, ByVal type As StructureType, ByVal fileManager As FileManagerLocal)
        MyBase.New(New RmStructure(fileManager.OntologyManager.AddTerm(text).Code, type), fileManager)
        Item.Occurrences.MaxCount = 1
    End Sub

    Sub New(ByVal struct As RmStructureCompound, ByVal fileManager As FileManagerLocal)
        MyBase.New(New RmStructure(struct), fileManager)
        mCardinality = struct.Children.Cardinality
        mIsFixed = struct.Children.Fixed
    End Sub

    Sub New(ByVal aNode As ArchetypeNodeAbstract)
        MyBase.New(aNode)
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
'The Original Code is ArchetypeComposite.vb.
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

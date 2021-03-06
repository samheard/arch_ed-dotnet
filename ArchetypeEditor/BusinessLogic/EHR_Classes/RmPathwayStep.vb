'
'	component:   "openEHR Archetype Project"
'	description: "This Reference model structure models the idea of a clinical state based on a reference model state"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'
Public Class RmPathwayStep
    Inherits RmStructure

    Private mStateType As StateMachineType = StateMachineType.Not_Set
    Private mAlternativeState As StateMachineType

    Property StateType() As StateMachineType
        Get
            Return mStateType
        End Get
        Set(ByVal value As StateMachineType)
            mStateType = value
        End Set
    End Property

    ReadOnly Property HasAlternativeState() As Boolean
        Get
            Return mAlternativeState <> StateMachineType.Not_Set
        End Get
    End Property

    Property AlternativeState() As StateMachineType
        Get
            Return mAlternativeState
        End Get
        Set(ByVal value As StateMachineType)
            mAlternativeState = value
        End Set
    End Property

    Sub New(ByVal nodeID As String, ByVal stateType As StateMachineType)
        MyBase.New(nodeID, StructureType.CarePathwayStep)
        mStateType = stateType
    End Sub

    Sub New(ByVal nodeID As String, ByVal pathwayStep As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)
        MyBase.New(nodeID, StructureType.CarePathwayStep)

        For i As Integer = 1 To pathwayStep.attributes.count
            Dim attribute As openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE = pathwayStep.attributes.i_th(i)

            If attribute.has_children Then
                Dim codedText As openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT = CType(attribute.children.first, openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT)

                Select Case attribute.rm_attribute_name.to_cil.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    Case "current_state"
                        Dim t As Constraint_Text = ArchetypeEditor.ADL_Classes.ADL_RmElement.ProcessText(codedText)

                        If t.AllowableValues.Codes.Count > 0 Then
                            mStateType = Integer.Parse(t.AllowableValues.Codes(0))
                        End If

                        If t.AllowableValues.Codes.Count > 1 Then
                            mAlternativeState = Integer.Parse(t.AllowableValues.Codes(1))
                        End If
                    Case "careflow_step"
                        'No action now as atcode is set for RmPathwayStep and reproduced here
                    Case Else
                        Debug.Assert(False, pathwayStep.rm_type_name.to_cil & " not handled")
                End Select
            End If
        Next
    End Sub

    Sub New(ByVal nodeID As String, ByVal pathwayStep As XMLParser.C_COMPLEX_OBJECT)
        MyBase.New(nodeID, StructureType.CarePathwayStep)

        Dim attribute As XMLParser.C_ATTRIBUTE

        For Each attribute In pathwayStep.attributes
            Dim codedText As XMLParser.C_COMPLEX_OBJECT = CType(attribute.children(0), XMLParser.C_COMPLEX_OBJECT)

            Select Case attribute.rm_attribute_name.ToLower(System.Globalization.CultureInfo.InvariantCulture)
                Case "current_state"
                    Dim t As Constraint_Text = ArchetypeEditor.XML_Classes.XML_RmElement.ProcessText(codedText)

                    If t.AllowableValues.Codes.Count > 0 Then
                        mStateType = Integer.Parse(t.AllowableValues.Codes(0))
                    End If

                    If t.AllowableValues.Codes.Count > 1 Then
                        mAlternativeState = Integer.Parse(t.AllowableValues.Codes(1))
                    End If
                Case "careflow_step"
                    'No action now as atcode is set for RmPathwayStep and reproduced here
                Case Else
                    Debug.Assert(False, pathwayStep.rm_type_name & " not handled")
            End Select
        Next
    End Sub

    Overrides Function Copy() As RmStructure
        Dim result As New RmPathwayStep(NodeId, StateType)
        result.AlternativeState = AlternativeState
        result.Occurrences = Occurrences.Copy()
        Return result
    End Function

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
'The Original Code is RmMachineSlot.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
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

'
'	component:   "openEHR Archetype Project"
'	description: "Constraint on real number"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'

Option Strict On

Public Class Constraint_Real
    Inherits Constraint_Count

    Public Overrides ReadOnly Property Type() As ConstraintType
        Get
            Return ConstraintType.Real
        End Get
    End Property

    Public Shadows Property MinimumValue() As Single
        Get
            Return mMinval
        End Get
        Set(ByVal Value As Single)
            mMinval = Value
            mHasMinVal = True
        End Set
    End Property
    Public Shadows Property MaximumValue() As Single
        Get
            Return mMaxval
        End Get
        Set(ByVal Value As Single)
            mHasMaxVal = True
            mMaxval = Value
        End Set
    End Property

    Public Overrides Property AssumedValue() As Object
        Get
            If HasAssumedValue Then
                Return CSng(mAssumedValue)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Object)
            Try
                mAssumedValue = CSng(Value)
                HasAssumedValue = True
            Catch ex As Exception
                Debug.Assert(False, Value.ToString & "is not valid value for this type")
            End Try
        End Set
    End Property

    Public Sub SetFromCount(ByVal c As Constraint_Count)

        Me.HasMaximum = c.HasMaximum
        If c.HasMaximum Then
            Me.MaximumValue = c.MaximumValue
        End If
        Me.HasAssumedValue = c.HasAssumedValue
        If c.HasAssumedValue Then
            Me.AssumedValue = c.AssumedValue
        End If
        Me.HasMinimum = c.HasMinimum
        If c.HasMinimum Then
            Me.MinimumValue = c.MinimumValue
        End If
        Me.IncludeMaximum = c.IncludeMaximum
        Me.IncludeMinimum = c.IncludeMinimum
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
'The Original Code is RealConstraint.vb.
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
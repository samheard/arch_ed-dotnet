﻿'
'	component:   "openEHR Archetype Project"
'	description: "Constraint on a cluster"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Peter Gummer"
'	support:     https://openehr.atlassian.net/browse/AEPR
'	copyright:   "Copyright (c) 2011 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL: http://www.openehr.org/svn/knowledge_tools_dotnet/TRUNK/ArchetypeEditor/DataConstraints/ChoiceConstraint.vb $"
'	revision:    "$LastChangedRevision: 676 $"
'	last_change: "$LastChangedDate: 2010-11-11 11:55:24 +1100 (Thu, 11 Nov 2010) $"
'

Option Strict On

Public Class Constraint_Cluster
    Inherits Constraint

    Public Overrides ReadOnly Property Kind() As ConstraintKind
        Get
            Return ConstraintKind.Cluster
        End Get
    End Property

    Public Overrides Function Copy() As Constraint
        Return New Constraint_Cluster
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
'The Original Code is ChoiceCluster.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.com).
'Portions created by the Initial Developer are Copyright (C) 2011
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
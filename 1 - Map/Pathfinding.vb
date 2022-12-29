Friend Class Pathfinding

    '------------------------------------------------------------------------'
    '--------------------------METTRE A JOUR---------------------------------'
    '------------------------------------------------------------------------'

    Private cases(2500) As String
    Private openlist As New ArrayList
    Private closelist As New ArrayList
    Private Plist(1025) As Integer
    Private Flist(1025) As Integer
    Private Glist(1025) As Integer
    Private Hlist(1025) As Integer
    Private MapHandler() As Cell
    Private fight As Boolean
    Private nombreDePM As Integer = 9999
    Private mapLargeur As Integer
    Private EviteChangeur As Boolean

    Private Sub LoadSprites(cellEnd As Integer)

        For i As Integer = 1 To 1000

            If MapHandler(i).active AndAlso MapHandler(i).movement > 1 Then

                If MapHandler(i).lineOfSight = False Then

                    closelist.Add(i)

                    'Je suis pas en combat.
                ElseIf fight = False Then

                    'S'il s'agit d'une case avec des soleils pour changer de map
                    If (MapHandler(i).layerObject1Num = 1030) OrElse (MapHandler(i).layerObject2Num = 1030) Then

                        'Je vérifie qu'il s'agit d'une case autre de celle que je souhaite.
                        If i <> cellEnd AndAlso EviteChangeur Then

                            closelist.Add(i)

                        End If

                    End If

                End If

            Else

                closelist.Add(i)

            End If

        Next

    End Sub

    Private Sub LoadEntity(cellEnd As Integer, caseActuel As Integer)

        With Bot

            For Each pair As Map_Variable.Entite In .Map.Entite.Values

                If pair.Cellule <> caseActuel AndAlso pair.Cellule <> cellEnd Then

                    closelist.Add(pair.Cellule)

                End If

            Next

        End With

    End Sub

    Private Sub LoadCell()
        For i = 0 To 1024
            Plist(i) = 0
            Flist(i) = 0
            Glist(i) = 0
            Hlist(i) = 0
        Next
    End Sub

    Public Function Pathing(nCellEnd As Integer,
                            Optional nbrPM As Integer = 9999)

        With Bot

            Try

                If .Map.Handler(nCellEnd).active = False Then

                    Return ""

                End If

                mapLargeur = If(.Map.Largeur = 0, 15, .Map.Largeur)
                EviteChangeur = Not .Combat.Combat
                MapHandler = .Map.Handler
                fight = .Combat.Combat

                InitializeCells()
                LoadCell()

                If fight Then

                    If nbrPM = 9999 Then

                        nombreDePM = 3
                    Else

                        nombreDePM = nbrPM

                    End If

                Else

                    nombreDePM = nbrPM

                End If

                LoadSprites(nCellEnd)

                LoadEntity(nCellEnd, .Map.Entite(.Personnage.ID).Cellule)

                closelist.Remove(nCellEnd)

                Dim returnPath As String = Findpath(.Map.Entite(.Personnage.ID).Cellule, nCellEnd)

                .Map.PathTotal = returnPath

                Return cleanPath(returnPath)

            Catch ex As Exception

            End Try

            Return ""

        End With
    End Function

    Private Function Findpath(cell1 As Integer, cell2 As Integer) As String

        Dim current As Integer
        Dim i As Integer = 0

        openlist.Add(cell1)

        Do Until openlist.Contains(cell2)

            i += 1
            If i > 1000 Then

                Return ""

            End If

            current = getFpoint()

            If current = cell2 Then

                Exit Do

            End If

            closelist.Add(current)
            openlist.Remove(current)

            For Each cell As Integer In getChild(current)

                If closelist.Contains(cell) = False Then

                    If openlist.Contains(cell) Then

                        If Glist(current) + 5 < Glist(cell) Then
                            Plist(cell) = current
                            Glist(cell) = Glist(current) + 5
                            Hlist(cell) = goalDistance(cell, cell2)
                            Flist(cell) = Glist(cell) + Hlist(cell)
                        End If

                    Else
                        openlist.Add(cell)
                        openlist.Item(openlist.Count - 1) = cell
                        Glist(cell) = Glist(current) + 5
                        Hlist(cell) = goalDistance(cell, cell2)
                        Flist(cell) = Glist(cell) + Hlist(cell)
                        Plist(cell) = current
                    End If

                End If

            Next
        Loop

        Return (GetParent(cell1, cell2))

    End Function
    Private Function GetParent(ByVal cell1 As Integer, ByVal cell2 As Integer)

        Dim current As Integer = cell2
        Dim pathCell As New ArrayList From {current}

        Do Until current = cell1
            pathCell.Add(Plist(current))
            current = Plist(current)
            If current = 0 Then
                Exit Do
            End If
        Loop
        Return getPath(pathCell)
    End Function


    Private Sub InitializeCells()
        Dim Number As Integer = 0
        Dim hash() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-", "_"}
        Dim hash2() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
        For i = 0 To hash2.Length - 1
            For j = 0 To hash.Length - 1
                cases(Number) = hash2(i) & hash(j)
                Number += 1
            Next
        Next i
    End Sub

    Private Function GetPath(pathCell As ArrayList)
        pathCell.Reverse()
        Dim pathing As String = ""
        Dim current
        Dim child
        Dim PMUsed As Integer = 0

        For i = 0 To pathCell.Count - 2
            PMUsed += 1
            If (PMUsed > nombreDePM) Then
                Return pathing
            End If
            current = pathCell(i)
            child = pathCell(i + 1)
            pathing &= getOrientation(current, child, mapLargeur) & cases(child)
        Next
        Return pathing
    End Function

    Private Function getChild(ByVal cell As Integer)

        Dim x = getCaseCoordonneeX(cell)
        Dim y = getCaseCoordonneeY(cell)
        Dim children As New ArrayList
        Dim temp
        Dim locx, locy

        If fight = False Then

            temp = cell - (mapLargeur * 2 - 1)
            locx = getCaseCoordonneeX(temp)
            locy = getCaseCoordonneeY(temp)
            If temp > 1 And temp < 1024 And locx = x - 1 And locy = y - 1 And closelist.Contains(temp) = False Then
                children.Add(temp)
            End If

            temp = cell + (mapLargeur * 2 - 1)
            locx = getCaseCoordonneeX(temp)
            locy = getCaseCoordonneeY(temp)
            If temp > 1 And temp < 1024 And locx = x + 1 And locy = y + 1 And closelist.Contains(temp) = False Then
                children.Add(temp)
            End If

        End If

        temp = cell - mapLargeur
        locx = getCaseCoordonneeX(temp)
        locy = getCaseCoordonneeY(temp)
        If temp > 1 And temp < 1024 And locx = x - 1 And locy = y And closelist.Contains(temp) = False Then
            children.Add(temp)
        End If

        temp = cell + mapLargeur
        locx = getCaseCoordonneeX(temp)
        locy = getCaseCoordonneeY(temp)
        If temp > 1 And temp < 1024 And locx = x + 1 And locy = y And closelist.Contains(temp) = False Then
            children.Add(temp)
        End If

        temp = cell - (mapLargeur - 1)
        locx = getCaseCoordonneeX(temp)
        locy = getCaseCoordonneeY(temp)
        If temp > 1 And temp < 1024 And locx = x And locy = y - 1 And closelist.Contains(temp) = False Then
            children.Add(temp)
        End If

        temp = cell + (mapLargeur - 1)
        locx = getCaseCoordonneeX(temp)
        locy = getCaseCoordonneeY(temp)
        If temp > 1 And temp < 1024 And locx = x And locy = y + 1 And closelist.Contains(temp) = False Then
            children.Add(temp)
        End If

        If fight = False Then

            temp = cell - 1
            locx = getCaseCoordonneeX(temp)
            locy = getCaseCoordonneeY(temp)
            If temp > 1 And temp < 1024 And locx = x - 1 And locy = y + 1 And closelist.Contains(temp) = False Then
                children.Add(temp)
            End If

            temp = cell + 1
            locx = getCaseCoordonneeX(temp)
            locy = getCaseCoordonneeY(temp)
            If temp > 1 And temp < 1024 And locx = x + 1 And locy = y - 1 And closelist.Contains(temp) = False Then
                children.Add(temp)
            End If

        End If

        Return children

    End Function

    Private Function GetFpoint()

        Dim x As Integer = 9999
        Dim cell As Integer

        For Each item As Integer In openlist
            If closelist.Contains(item) = False Then
                If Flist(item) < x Then
                    x = Flist(item)
                    cell = item
                End If
            End If
        Next

        Return cell
    End Function

    Public Class loc8
        Public y As Integer = 0
        Public x As Integer = 0
    End Class

    Private Function getCaseCoordonneeX(ByVal nNum As Integer) As Integer
        Dim _loc4 = mapLargeur
        Dim _loc5 = Math.Floor(nNum / (_loc4 * 2 - 1))
        Dim _loc6 = nNum - _loc5 * (_loc4 * 2 - 1)
        Dim _loc7 = _loc6 Mod _loc4
        Dim _loc8 As New loc8

        Dim y As Integer = _loc5 - _loc7
        Dim x As Integer = (nNum - (_loc4 - 1) * y) / _loc4
        Return x
    End Function

    Private Function getCaseCoordonneeY(ByVal nNum As Integer) As Integer
        Dim _loc4 = mapLargeur
        Dim _loc5 = Math.Floor(nNum / (_loc4 * 2 - 1))
        Dim _loc6 = nNum - _loc5 * (_loc4 * 2 - 1)
        Dim _loc7 = _loc6 Mod _loc4
        Dim _loc8 As New loc8

        Dim y As Integer = _loc5 - _loc7
        Dim x As Integer = (nNum - (_loc4 - 1) * y) / _loc4
        Return y
    End Function

    Private Function goalDistance(ByVal nCell1 As Integer, ByVal nCell2 As Integer)
        Dim _loc5x = getCaseCoordonneeX(nCell1)
        Dim _loc5y = getCaseCoordonneeY(nCell1)
        Dim _loc6x = getCaseCoordonneeX(nCell2)
        Dim _loc6y = getCaseCoordonneeY(nCell2)
        Dim _loc7 = Math.Abs(_loc5x - _loc6x)
        Dim _loc8 = Math.Abs(_loc5y - _loc6y)
        Return (_loc7 + _loc8)
    End Function

    Private Shared Function getOrientation(ByVal cell1 As Integer, ByVal cell2 As Integer, ByVal Map_Largeur As Integer) As Object

        Dim obj As Object

        Dim num As Integer = cell2 - cell1

        Select Case num
            Case 0 - (Map_Largeur * 2 - 1), -29
                obj = "g"
            Case Map_Largeur * 2 - 1, 29
                obj = "c"
            Case -1
                obj = "e"
            Case 1
                obj = "a"
            Case CShort(-Map_Largeur)
                obj = "f"
            Case Map_Largeur
                obj = "b"
            Case <> 0 - (Map_Largeur - 1)
                obj = If(num <> Map_Largeur - 1, "a", "d")
            Case Else
                obj = "h"
        End Select

        Return obj

    End Function

    Private Function cleanPath(ByVal path As String) As String

        Dim cleanedPath As String = ""

        If (path.Length > 3) Then
            For i As Integer = 1 To path.Length Step 3
                If (Mid(path, i, 1) <> Mid(path, i + 3, 1)) Then cleanedPath &= Mid(path, i, 3)
            Next
        Else
            cleanedPath = path
        End If
        Return cleanedPath

    End Function

End Class

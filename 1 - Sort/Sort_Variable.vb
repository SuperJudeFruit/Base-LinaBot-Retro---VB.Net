Namespace Sort_Variable

    Public Class Base


        Public Event EventSort(choix As String, value As Object)

        Dim _Sort As New Dictionary(Of Integer, Information)


        Public ReadOnly Property Sort(Optional IdUnique As Integer = -1) As Object

            Get

                If IdUnique = -1 Then

                    Return _Sort

                Else

                    Return _Sort(IdUnique)

                End If

            End Get

        End Property


        Public WriteOnly Property Ajoute As Information

            Set(value As Information)

                _Sort.Add(value.ID, value)
                RaiseEvent EventSort("Ajoute", value)

            End Set

        End Property


        Public WriteOnly Property Modifie As Information

            Set(value As Information)

                _Sort(value.ID) = value
                RaiseEvent EventSort("Modifie", value)

            End Set

        End Property


        Dim _capital As Integer = 0
        Public Property Capital As Integer

            Get

                Return _capital

            End Get

            Set(value As Integer)

                _capital = value
                RaiseEvent EventSort("Capital", value)

            End Set

        End Property


        Public Sub Reset()

            _Sort = New Dictionary(Of Integer, Information)

        End Sub

    End Class

    Public Class Information

        Public ID As Integer = -1
        Public Niveau As Integer = -1
        Public Nom As String = ""
        Public PO As New MinMax

        Public PA As Integer = -1
        Public NombreLancerParTour As Integer = -1
        Public NombreLancerParTourParJoueur As Integer = -1
        Public NombreToursEntreDeuxLancers As Integer = -1
        Public POModifiable As Boolean = False
        Public LigneDeVue As Boolean = False
        Public LancerEnLigne As Boolean = False
        Public CelluleLibre As Boolean = False
        Public ECFiniTour As Boolean = False
        Public Zone As New MinMax
        Public ZoneEffet As String = ""
        Public NiveauRequisUp As Integer = -1
        Public SortClasse As String = ""
        Public Definition As String = ""
        Public BarreSort As String = ""

    End Class

End Namespace

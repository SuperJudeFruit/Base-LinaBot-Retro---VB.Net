Namespace Ami_Variable

    Public Class Base

        Public Event EventAmi(choix As String, value As Object)

        Private _Personnage As New Dictionary(Of String, Information)
        Public ReadOnly Property Liste As Dictionary(Of String, Information)

            Get

                Return _Personnage

            End Get

        End Property

        Public WriteOnly Property Ajoute As Information

            Set(value As Information)

                If _Personnage.ContainsKey(value.Pseudo) Then

                    _Personnage(value.Pseudo) = value

                Else

                    _Personnage.Add(value.Pseudo, value)

                End If

                RaiseEvent EventAmi("Ajoute", value)

            End Set

        End Property

        Private _Averti As Boolean = False
        Public Property Averti As Boolean

            Get

                Return _Averti

            End Get

            Set(value As Boolean)

                _Averti = value
                RaiseEvent EventAmi("Averti", value)

            End Set

        End Property

        Public Sub Reset()

            _Personnage = New Dictionary(Of String, Information)

        End Sub

    End Class

    Public Class Information

        Public Ajoute As Boolean = False
        Public Pseudo As String = ""
        Public Connecte As Boolean = False
        Public Nom As String = ""
        Public Niveau As Integer = -1
        Public Alignement As String = ""
        Public Classe As String = ""
        Public Sex As String = ""
        Public ClasseSex As String = ""
        Public Zone As Integer = -1

    End Class

End Namespace

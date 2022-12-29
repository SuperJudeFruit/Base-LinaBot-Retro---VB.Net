Namespace Tchat_Variable

    Public Class Base

        Public Event EventTchat(Choix As String, value As Object)

        Private _Message As New List(Of Information)
        Public Property Message As Object

            Get

                Return _Message

            End Get

            Set(value As Object)

                _Message.Add(value)
                RaiseEvent EventTchat("Message", value)

            End Set

        End Property

        Private _Canaux As New Canaux
        Public Property Canaux As Canaux

            Get

                Return _Canaux

            End Get

            Set(value As Canaux)

                _Canaux = value
                RaiseEvent EventTchat("Canal", value)

            End Set

        End Property

    End Class

    Public Class Information

        Public Heure As Date = TimeOfDay
        Public Canal As String = ""
        Public Id_Joueur As Integer = 0
        Public Nom_Joueur As String = ""
        Public Message As String = ""
        Public Item As String = ""
        Public Couleur As Color = Color.White

    End Class

    Public Class Canaux

        Public Information As Boolean = False
        Public Commun As Boolean = False
        Public GroupeEquipeMP As Boolean = False
        Public Guilde As Boolean = False
        Public Alignement As Boolean = False
        Public Recrutement As Boolean = False
        Public Commerce As Boolean = False
        Public Evenement As Boolean = False

    End Class

End Namespace
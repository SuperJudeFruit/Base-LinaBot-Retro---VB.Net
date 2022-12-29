Namespace Connexion_Variable

    Public Class Base

        Public Event EventConnexion(choix As String, value As Object)

        Private _Connecter As Boolean = False
        Private _Connexion As Boolean = False
        Private _Authentification As Boolean = False

        Public Property Connecter As Boolean

            Get

                Return _Connecter

            End Get

            Set(value As Boolean)

                _Connecter = value

                RaiseEvent EventConnexion("Connecter", value)

            End Set

        End Property

        Public Property Connexion As Boolean

            Get

                Return _Connexion

            End Get

            Set(value As Boolean)

                _Connexion = value

                RaiseEvent EventConnexion("Connexion", value)

            End Set

        End Property

        Public Property Authentification As Boolean

            Get

                Return _Authentification

            End Get

            Set(value As Boolean)

                _Authentification = value

                RaiseEvent EventConnexion("Authentification", value)

            End Set

        End Property

    End Class

End Namespace
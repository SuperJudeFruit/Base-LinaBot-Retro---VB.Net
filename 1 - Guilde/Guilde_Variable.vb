Namespace Guilde_Variable

    Public Class Base

        Public Niveau As Integer = -1
        Public Experience As New MinMax
        Public Guilde As Boolean = False
        Public Invitation As Boolean = False
        Public Nom As String = ""
        Public Inviteur As String = ""
        Public Inviter As String = ""
        Public Membre As New Dictionary(Of String, Membre)
        Public Percepteur As New Percepteur
        Public Enclos As New Dictionary(Of String, Enclos)
        Public Maison As New Dictionary(Of String, Maison)

    End Class

    Public Class Membre

        Public Classe As String = ""
        Public Nom As String = ""
        Public ID As Integer = -1
        Public Rang As String = ""
        Public Rang_Chiffre As Integer = -1
        Public Droit As New Droit
        Public Droit_Chiffre As Integer = -1
        Public Experience As New MinMax
        Public Niveau As Integer = -1
        Public Alignement As String = ""
        Public Connecter As Boolean = False
        Public DerniereConnection As String = ""

    End Class

    Public Class Droit

        Public GererLesBoosts As Boolean = False
        Public GererLesDroits As Boolean = False
        Public InviterDeNouveauxMembres As Boolean = False
        Public Bannir As Boolean = False
        Public GererLesRepartitionsXP As Boolean = False
        Public GererSaRepartitionXP As Boolean = False
        Public GererLesRangs As Boolean = False
        Public PoserUnPercepteur As Boolean = False
        Public CollecterSurUnPercepteur As Boolean = False
        Public UtiliserLesEnclos As Boolean = False
        Public AmenagerLesEnclos As Boolean = False
        Public GererLesMonturesDesAutresMembres As Boolean = False

    End Class

    Public Class Percepteur

        Public PointsDeVie As Integer = -1
        Public BonusAuxDommages As Integer = -1
        Public Prospection As Integer = -1
        Public Sagesse As Integer = -1
        Public Pods As Integer = -1
        Public NombreDePercepteur As Integer = -1
        Public ResteARepartir As Integer = -1
        Public ActuellementPercepteur As Integer = -1
        Public CoutPourPoserPercepteur As Integer = -1

        'Spell
        Public ArmureAqueuse As Integer = -1
        Public ArmureIncandescente As Integer = -1
        Public ArmureTerrestre As Integer = -1
        Public ArmureVenteuse As Integer = -1
        Public Flamme As Integer = -1
        Public Cyclone As Integer = -1
        Public Vague As Integer = -1
        Public Rocher As Integer = -1
        Public MotSoignant As Integer = -1
        Public Desenvoutement As Integer = -1
        Public CompulsionDeMasse As Integer = -1
        Public Destabilisation As Integer = -1

    End Class

    Public Class Enclos

        Public MapID As Integer = -1
        Public Position As String = ""
        Public Dragodinde As New MinMax

    End Class

    Public Class Maison

        Public MaisonVisiblePourGuilde As Boolean = False
        Public BlasonVisiblePourGuilde As Boolean = False
        Public BlasonVisiblePourToutMonde As Boolean = False
        Public AccesAutoriserMembreGuilde As Boolean = False
        Public AccesInterditNonMembreGuilde As Boolean = False
        Public AccesCoffresAutoriseMembreGuilde As Boolean = False
        Public AccesCoffresInterditNonMembreGuilde As Boolean = False
        Public TeleportationAutoriser As Boolean = False
        Public ReposAutoriser As Boolean = False

        Public ID As Integer = -1
        Public Prorietaire As String = ""
        Public Position As String = ""
        Public Competence As String = ""

    End Class

End Namespace

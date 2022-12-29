Namespace Maison

    Module Maison

        Sub Porte_Verouiller(data As String)

            With Bot

                Try

                    ' hX 999 | 0
                    ' hX Id  | Vérouillé (oui ou non)

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    If .Maison.Map.ContainsKey(separateData(0)) Then

                        .Maison.Map(separateData(0)).Verouiller = separateData(1)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Porte_Verouiller", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Coffre_Verouiller(data As String)

            With Bot

                Try


                    ' sX 9999   _ 999     | 0
                    ' sX Id map _ cellule | Vérouillé (oui ou non)

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    Dim MapID As Integer = Split(separateData(0), "_")(0)
                    Dim CelluleID As Integer = Split(separateData(0), "_")(1)

                    If .Maison.Map(MapID).Coffre.ContainsKey(MapID & "_" & CelluleID) Then

                        .Maison.Map(MapID).Coffre(MapID & "_" & CelluleID).Verouiller = separateData(1)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Coffre_Verouiller", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub MaMaison(data As String)

            With Bot

                Try

                    ' hL + 999       ; 1          ; 0        ; 0
                    ' hL + Id Maison ; Verouiller ; En Vente ; En Guilde

                    Dim separate As String() = Split(Mid(data, 4), ";")

                    With .Maison.Personnelle

                        .ID = separate(0)
                        .Verouiller = separate(1)
                        .Vente = separate(2)
                        .Guilde = separate(3)
                        .Cellule = VarMaison(.ID).CellulePorte
                        .MapID = VarMaison(.ID).MapId
                        .Coordonnees = VarMaison(.ID).Map
                        .Prix = 0
                        .Code = 0

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Maison", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Supprime(data As String)

            With Bot

                Try

                    ' hL -
                    ' hL -

                    .Maison.Personnelle = New Maison_Variable.Maison

                    EcritureMessage("[Dofus]", "Vous n'avez plus de maison.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Vendu(data As String)

            With Bot

                Try

                    ' M15 | 1000 ; slider 
                    ' M15 | Prix ; Pseudo 

                    Dim separateData As String() = Split(data, "|")
                    separateData = Split(separateData(1), ";")

                    EcritureMessage("[Dofus]", "L'une de vos maisons vient d'être achetée " & separateData(0) & " kamas par " & separateData(1) & ". La somme a été placée sur votre compte en banque.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Vendu", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Coffres(data As String)

            With Bot

                Try


                    ' sL + 9999   _ 114     ; 1          | Next
                    ' sL + Id Map _ Cellule ; Verouiller | Next

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    For i = 0 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        Dim Verouiller As Boolean = separate(1)
                        Dim MapID As Integer = Split(separate(0), "_")(0)
                        Dim CelluleID As Integer = Split(separate(0), "_")(1)

                        Dim varCoffre As New Maison_Variable.Coffre

                        With varCoffre

                            .Coordonnees = VarMap(MapID)
                            .MapID = MapID
                            .Cellule = CelluleID
                            .Verouiller = Verouiller
                            .Code = -1

                        End With

                        If .Maison.Personnelle.Coffre.ContainsKey(MapID & "_" & CelluleID) Then

                            .Maison.Personnelle.Coffre(MapID & "_" & CelluleID) = varCoffre

                        Else

                            .Maison.Personnelle.Coffre.Add(MapID & "_" & CelluleID, varCoffre)

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Coffres", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Map(data As String)

            With Bot

                Try

                    ' hP 444 | Linacu ; 0            ; Lenculeur lourd ; a     , 0    ,i     ,9drge
                    ' hP Id  | Pseudo ; pas en vente ; Nom guilde      ; blason,blason,blason,blason

                    data = Mid(data, 3)

                    Dim separateData As String() = Split(data, "|")

                    Dim id As Integer = separateData(0)

                    separateData = Split(separateData(1), ";")

                    If Not VarMaison.ContainsKey(id) Then

                        MaisonAjouteInformation(id)

                    End If

                    Dim newMaison As New Maison_Variable.Maison

                    With newMaison

                        .ID = id
                        .Verouiller = False
                        .Vente = separateData(1)
                        .Guilde = False
                        .Proprietaire = separateData(0)
                        .Cellule = VarMaison(id).CellulePorte
                        .MapID = Bot.Map.ID
                        .Coordonnees = Bot.Map.Coordonnees
                        .Prix = -1
                        .Code = -1

                        If separateData.Length > 2 Then

                            .Nom_Guilde = separateData(2)

                        End If

                    End With

                    If .Maison.Map.ContainsKey(id) Then

                        .Maison.Map(id) = newMaison

                    Else

                        .Maison.Map.Add(id, newMaison)

                    End If

                    If VarMaison(id).Map = "[X,Y]" OrElse VarMaison(id).MapId = "0" Then

                        MaisonChangeInformation(id)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_mdlMaison_GiMaisonMap", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Private Sub MaisonChangeInformation(id As String)

            With Bot

                Try


                    'Mise à jour de la version automatiquement (celui du fichier)
                    Dim swLecture As New IO.StreamReader(Application.StartupPath & "\Data/Maison.txt")

                    Dim ligneFinal As String = ""

                    Do Until swLecture.EndOfStream

                        Dim Ligne As String = swLecture.ReadLine

                        If Ligne <> "" Then

                            Dim separate As String() = Split(Ligne, " | ")

                            If separate(0) = "hP : " & id Then

                                ligneFinal &= "hP : " & id & " | Porte : " & VarMaison(id).CellulePorte & " | Map : " & .Map.Coordonnees & " | Mapid : " & .Map.ID & " | Nom : " & VarMaison(id).Nom & vbCrLf

                            Else

                                ligneFinal &= Ligne & vbCrLf

                            End If

                        End If

                    Loop

                    'Puis je ferme le fichier.
                    swLecture.Close()

                    'J'ouvre le fichier pour y écrire se que je souhaite
                    Dim Sw_Ecriture As New IO.StreamWriter(Application.StartupPath + "\Data/Maison.txt")

                    'J'écris dedans avant de le fermer.
                    Sw_Ecriture.Write(ligneFinal)

                    'Puis je le ferme.
                    Sw_Ecriture.Close()

                    ChargeMaison()


                Catch ex As Exception

                End Try

            End With

        End Sub

        Private Sub MaisonAjouteInformation(id As String)

            With Bot

                Try

                    'Mise à jour de la version automatiquement (celui du fichier)
                    Dim swLecture As New IO.StreamReader(Application.StartupPath & "\Data/Maison.txt")

                    Dim ligneFinal As String = ""

                    Do Until swLecture.EndOfStream

                        Dim ligne As String = swLecture.ReadLine

                        If ligne <> "" Then

                            ligneFinal &= ligne & vbCrLf

                        End If

                    Loop

                    'Puis je ferme le fichier.
                    swLecture.Close()

                    ligneFinal &= "hP : " & id & " | Porte : 0 | Map : " & .Map.Coordonnees & " | Mapid : " & .Map.ID & " | Nom : Maison"

                    'J'ouvre le fichier pour y écrire se que je souhaite
                    Dim Sw_Ecriture As New IO.StreamWriter(Application.StartupPath + "\Data/Maison.txt")

                    'J'écris dedans avant de le fermer.
                    Sw_Ecriture.Write(ligneFinal)

                    'Puis je le ferme.
                    Sw_Ecriture.Close()

                    ChargeMaison()

                Catch ex As Exception

                End Try

            End With

        End Sub

        Sub Quitte_Achat(data As String)

            With Bot

                Try


                    ' hV

                    .Personnage.EnInteraction = False

                    EcritureMessage("[Dofus]", "Le bot n'est plus en achat de maison.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Quitte_Achat", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Prix(data As String)

            With Bot

                Try

                    ' hCK 607 | 8999999
                    ' hCK Id  | Prix

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    If .Maison.Map.ContainsKey(separateData(0)) Then

                        .Maison.Map(separateData(0)).Prix = separateData(1)

                    End If


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Prix", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Achete(data As String)

            With Bot

                Try

                    ' hBK 999 | 10000000 
                    ' hBK ID  | Prix 

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    EcritureMessage("[Dofus]", "Tu viens d'acheter 'Maison' pour " & separateData(1) & " kamas.", Color.Green)


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Achete", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Mis_En_Vente(data As String)

            With Bot

                Try

                    ' hSK 999 |10000000 
                    ' hSK ID  | Prix 

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    EcritureMessage("[Dofus]", "'Maison' est mise en vente au prix de " & separateData(1) & " kamas.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Mis_En_Vente", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Ouverture(data As String)

            With Bot

                Try

                    ' KCK 1 | 8 
                    ' KCK 1 | lenombre de chiffre maximum  

                    .Maison.Ouverture = True

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    Select Case separateData(0)

                        Case "0"

                            EcritureMessage("[Dofus]", "Veuillez saisir le code.", Color.Green)

                        Case "1"

                            EcritureMessage("[Dofus]", "Veuillez saisir le nouveau code (maximum de " & separateData(1) & " chiffres).", Color.Green)

                    End Select

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Code", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Code_Modifie(data As String)

            With Bot

                Try


                    ' KKK

                    EcritureMessage("[Dofus]", "Code changé", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Code_Modifie", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Guilde(data As String)

            With Bot

                Try

                    ' hG 999
                    ' hG ID

                    EcritureMessage("[Dofus]", "Vous pouvez changer les droits de la maison de guilde.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_mdlMaison_GiMaisonGuilde", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Code_Echec(data As String)

            With Bot

                Try

                    ' KKE

                    EcritureMessage("[Dofus]", "Code erroné", Color.Red)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Code_Echec", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Fermeture(data As String)

            With Bot

                Try

                    ' KV

                    .Maison.Ouverture = False

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Fermeture", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace
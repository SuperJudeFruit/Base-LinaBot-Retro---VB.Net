Namespace Dragodinde

    Module Dragodinde

        Sub Ouvre(data As String)

            With Bot

                Try

                    .Dragodinde.Enclos.Clear()
                    .Dragodinde.Etable.Clear()

                    'ECK16 etc...
                    Dim separateData As String() = Split(data, "|")

                    separateData = Split(separateData(1), "~")

                    If separateData(0) <> "" Then

                        Information(separateData(0), "Etable")

                    End If

                    If separateData(1) <> "" Then

                        Information(separateData(1), "Enclos")

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Dragodinde_Ouvre", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Information(data As String, choix As String)

            With Bot

                Try

                    ' Re+ 1200859   : 46 : 10,3,10,15,22,10,10,15,46,23,18,3,22,10 :           ,            :     : 0    :  7587      , 7250   , 9250   : 7      : 1        : 185 : 0 : 2249      , 10000         : 2000     , 2000         : 206     , 1205        : -10000       , -10000    , 10000  : 2500  , 10000     : -1          : 0 : 7d#7#0#0 , 7c#1#0#0 : 0       , 240         : 5     , 20
                    ' Re+ Id unique : Id : Arbre généalogique                      : Capacité1 , Capacité 2 : Nom : Sexe :  Xp actuel , Xp Min , Xp Max : Niveau : Montable : ?   : ? : Endurance , Endurance Max : Maturité , Maturité max : Energie , Energie max :  Agressivité , Equilibré , Serein : Amour , Amour max : Fécondation : ? : +7 vita  , +1 sag   : Fatigue , Fatigue max : Repro , Repro max

                    Dim sexe As String() = {"Male", "Femelle"}
                    Dim capacite As String() = {"Infatigable", "Porteuse", "Reproductrice", "Sage", "Endurante", "Amoureuse", "Precoce", "Predisposee Genetique", "Cameleone"}

                    Dim separateData As String() = Split(data.Replace("Rd", "").Replace("Re+", "").Replace("Ee+", "").Replace("Ef+", "").Replace("Ee~", ""), ";")

                    For i = 0 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ":")

                        Dim newDragodinde As New Dragodinde_Variable.Information

                        With newDragodinde

                            ' Id Unique
                            .IdUnique = separate(0)

                            'ID
                            .ID = separate(1)

                            'Type
                            If VarDragodindeId.ContainsKey(separate(1)) Then

                                .Type = VarDragodindeId(separate(1))

                            Else

                                .Type = "Unknow"

                            End If

                            'Arbre Généalogique
                            .ArbreGenealogique = separate(2)

                            'Nom
                            .Nom = If(separate(4) = Nothing, "SansNom", separate(4))

                            'Sexe
                            .Sexe = sexe(separate(5))

                            'Niveau
                            .Niveau = separate(7)

                            'Expérience
                            With .Experience

                                .Minimum = Split(separate(6), ",")(1)
                                .Actuelle = Split(separate(6), ",")(0)
                                .Maximum = Split(separate(6), ",")(2)
                                .Pourcentage = .Actuelle / .Maximum * 100

                            End With

                            'Montable
                            .Montable = separate(8) <> "0"

                            'Endurance
                            With .Endurance

                                .Actuelle = Split(separate(11), ",")(0)
                                .Maximum = Split(separate(11), ",")(1)
                                .Pourcentage = .Actuelle / .Maximum * 100

                            End With

                            'Maturité
                            With .Maturite

                                .Actuelle = Split(separate(12), ",")(0)
                                .Maximum = Split(separate(12), ",")(1)
                                .Pourcentage = .Actuelle / .Maximum * 100

                            End With

                            'Amour
                            With .Amour

                                .Actuelle = Split(separate(15), ",")(0)
                                .Maximum = Split(separate(15), ",")(1)
                                .Pourcentage = .Actuelle / .Maximum * 100

                            End With

                            'Etat
                            With .Etat

                                .Agressiviter = Split(separate(14), ",")(0)
                                .Equilibrer = Split(separate(14), ",")(1)
                                .Sereniter = Split(separate(14), ",")(2)

                            End With

                            'Energie
                            With .Energie

                                .Actuelle = Split(separate(13), ",")(0)
                                .Maximum = Split(separate(13), ",")(1)
                                .Pourcentage = .Actuelle / .Maximum * 100

                            End With

                            'Fatigue
                            With .Fatigue

                                .Actuelle = Split(separate(19), ",")(0)
                                .Maximum = Split(separate(19), ",")(1)
                                .Pourcentage = .Actuelle / .Maximum * 100

                            End With

                            'Capacité
                            With .Capacite

                                .Primaire = capacite(Split(separate(3), ",")(0))
                                .Secondaire = capacite(Split(separate(3), ",")(1))

                            End With

                            'Caractéristique
                            If separate(18) <> Nothing Then

                                .Caracteristique = Item.Caracteristique(separate(18))

                            End If

                            'Fécondation
                            If .Endurance.Actuelle >= 7500 AndAlso .Maturite.Actuelle = .Maturite.Maximum AndAlso .Amour.Actuelle >= 7500 AndAlso .Niveau >= 5 Then

                                .Fecondation.Fecondable = True

                            Else

                                .Fecondation.Fecondable = False

                            End If

                            If separate(16) <> "-1" Then

                                .Fecondation.Heure = separate(16)

                            End If

                            'Reproduction
                            With .Reproduction

                                .Actuelle = Split(separate(20), ",")(0)
                                .Maximum = Split(separate(20), ",")(1)
                                .Pourcentage = .Actuelle / .Maximum * 100

                            End With

                        End With

                        With .Dragodinde

                            Select Case choix.ToLower

                                Case "information"

                                    .Information = newDragodinde

                                Case "equipe", "equiper"

                                    .Equiper = newDragodinde

                                Case "enclos", "enclo"

                                    If .Enclos.ContainsKey(separate(0)) Then

                                        .Enclos(separate(0)) = newDragodinde

                                    Else

                                        .Enclos.Add(separate(0), newDragodinde)

                                    End If

                                Case "etable"

                                    If .Etable.ContainsKey(separate(0)) Then

                                        .Etable(separate(0)) = newDragodinde

                                    Else

                                        .Etable.Add(separate(0), newDragodinde)

                                    End If

                            End Select

                        End With

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Dragodinde_Information", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Monter(data As String)

            With Bot

                Try

                    ' Rr +
                    ' Rr Monte ou descend

                    Select Case Mid(data, 3)

                        Case "+"

                            .Dragodinde.Monter = True
                            EcritureMessage("[Dofus]", "Vous êtes monté sur votre monture.", Color.Green)

                        Case "-"

                            .Dragodinde.Monter = False
                            EcritureMessage("[Dofus]", "Vous êtes déscendu de votre monture.", Color.Green)

                    End Select

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Dragodinde_Monter", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub XP(data As String)

            With Bot

                Try

                    ' Rx 95
                    ' Rx Xp donnée

                    .Dragodinde.Xp = Mid(data, 3)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Dragondinde_XP", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Nom(data As String)

            With Bot

                Try

                    ' Rn Linaculer
                    ' Rn Nom donnée

                    .Dragodinde.Equiper.Nom = Mid(data, 3)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Dragodinde_Nom", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Pods(data As String)

            With Bot

                Try

                    'Ew 0        ; 185
                    'Ew Actuelle ; Maximum

                    Dim separateData As String() = Split(Mid(data, 3), ";")

                    With .Dragodinde.Equiper.Pods

                        .Actuelle = separateData(0)
                        .Maximum = separateData(1)
                        .Pourcentage = CInt(separateData(0)) / CInt(separateData(1)) * 100

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Dragodinde_Pods", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Etable_Supprime(data As String)

            With Bot

                Try


                    'Ee- 1714320
                    'Ee- id dragodinde

                    If .Dragodinde.Etable.ContainsKey(Mid(data, 4)) Then

                        .Dragodinde.Etable.Remove(Mid(data, 4))

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Dragodinde_Etable_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Enclos_Supprime(data As String)

            With Bot

                Try

                    'Ef- 1714320
                    'Ef- id dragodinde

                    If .Dragodinde.Enclos.ContainsKey(Mid(data, 4)) Then

                        .Dragodinde.Enclos.Remove(Mid(data, 4))

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Dragodinde_Enclos_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace
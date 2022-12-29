Namespace Combat

    Module Combat

        Sub Entrer(data As String)

            With Bot

                Try

                    ' GA ; 905 ; 3101697   ;
                    ' GA ; Id  ; Id Joueur ; ?

                    Dim separateData As String() = Split(data, ";")

                    If separateData(2) = .Personnage.ID Then

                        .Combat.Combat = True

                        .Map.Entite.Clear()

                        EcritureMessage("[Combat]", "Vous êtes entré en combat.", Color.Sienna)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Entrer", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Fin(data As String)

            With Bot

                Try

                    'GE29226|2521478|0|2;2521478  ;Linaculer;60     ;0               ;11655000;11712633  ;12450000;93; ; ;388~1,393~1,394~1    ;12         |joueur suivant
                    '                   ;ID UNIQUE;Nom      ;niveau ;0 = win 1 = lose;Exp Min ;Exp Actuel;Xp Max  ;? ;?;?;ID Objet+Quantité,etc;Kamas dropé

                    .Combat = New Combat_Variable.Base
                    .Defi = New Defi_Variable.Base
                    .Map = New Map_Variable.Base

                    Dim separateData As String() = Split(data, "|")

                    For i = 3 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        If separate(1) = separateData(1) Then

                            With .Drop

                                .Kamas += CInt(separate(12))

                                Select Case separate(4)

                                    Case "0"

                                        .Gagne += 1

                                    Case "1"

                                        .Perdu += 1

                                End Select

                                Dim separateItem As String() = Split(separate(11), ",")

                                For a = 0 To separateItem.Length - 1

                                    separate = Split(separateItem(a), "~")

                                    If .Item.ContainsKey(separate(0)) Then

                                        .Item(separate(0)) += separate(1)

                                    Else

                                        .Item.Add(separate(0), separate(1))

                                    End If

                                Next

                            End With

                        End If

                    Next

                    EcritureMessage("[Dofus]", "Fin du combat.", Color.Sienna)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Fin", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Lancer(data As String)

            With Bot

                Try

                    'Indique l'épée et le tas de monstre (se qu'il contient)

                    ' Gt -42                     | + 1234567   ; Linaculer ; 60 
                    ' Gt -43                     | + -1        ; 63        ; 3      |+-2;63;2 
                    ' Gt Id Unique Epee/Tas Mobs | + Id Unique ; Nom/ID    ; Niveau | Suivant

                    Dim separateData As String() = Split(data, "|")
                    Dim id As Integer = Mid(separateData(0), 3, separateData(0).Length)

                    For i = 1 To separateData.Count - 1

                        Dim separate As String() = Split(separateData(i), ";")
                        Dim newMapCombatLancer As New Combat_Variable.Lancer

                        With newMapCombatLancer

                            .idUnique = separate(0).Replace("+", "")
                            .IdNom = separate(1)
                            .Niveau = separate(2)

                        End With

                        If .Combat.Lancer.ContainsKey(id) Then

                            .Combat.Lancer(id).Add(newMapCombatLancer)

                        Else

                            .Combat.Lancer.Add(id, New List(Of Combat_Variable.Lancer) From {newMapCombatLancer})

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Lancer", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Echec(data As String)

            With Bot

                Try

                    'GA ; 903 ; 1234567    ; f
                    'GA ; 903 ; Id Lanceur ; Cadenas/Groupe

                    Dim separateData As String() = Split(data, ";")

                    Select Case separateData(3)

                        Case "f"

                            EcritureMessage("[Dofus]", "Impossible de rejoindre le combat car l'équipe est fermée (ou limitée au groupe du joueur principal).", Color.Red)

                        Case "p"

                            EcritureMessage("[Dofus]", "Action impossible sur cette carte.", Color.Red)

                        Case Else

                            ErreurFichier(.Personnage.NomDuPersonnage, "GiMapAgressionEchec", data)

                    End Select


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Echec", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Expulser(data As String)

            With Bot

                Try

                    'GV

                    'Si en SOCKET
                    '.Mitm.Send("GC1")

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Expulser", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

    Namespace Sort

        Module Sort

            Sub Normal(data As String)

                With Bot

                    Try

                        'GA ; 300 ; 1234567    ; 61      , 148     , 902 , 5 , 31 , 2 , 1 
                        'GA ; 300 ; Id Lanceur ; Id Sort , Cellule , ?   , ? , ?  , ? , ?

                        Dim separateData As String() = Split(data, ";")
                        Dim separateSort As String() = Split(separateData(3), ",")

                        If separateData(2) = .Personnage.ID Then

                            .Combat.Echec = False

                        End If

                        If VarSort.ContainsKey(separateSort(0)) Then

                            EcritureMessage("[Combat]", .Map.Entite(separateData(2)).Nom & " lance le sort : " & VarSort(separateSort(0)).Values(0).Nom &
                                                          " sur la cellule : " & separateSort(1), Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Sort_Normal", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub CoupCritique(data As String)

                With Bot

                    Try

                        ' GA ; 301 ; 1234567    ; 161
                        ' GA ; 301 ; ID_Lanceur ; ID_Sort

                        Dim separateData As String() = Split(data, ";")

                        If separateData(2) = .Personnage.ID Then

                            .Combat.Echec = False
                            EcritureMessage("[Combat]", "Coup Critique !", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Sort_CoupCritique", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub EchecCritique(data As String)

                With Bot

                    Try

                        ' GA ; 302 ; 1234567    ; 161
                        ' GA ; 302 ; ID_Lanceur ; ID_Sort

                        Dim separateData As String() = Split(data, ";")

                        If VarSort.ContainsKey(CInt(separateData(3))) Then

                            EcritureMessage("[Combat]", .Map.Entite(separateData(2)).Nom & " lance le sort : " & VarSort(separateData(3)).Values(0).Nom & ".", Color.Sienna)
                            EcritureMessage("[Combat]", "Echec Critique !", Color.Sienna)

                        End If

                        If separateData(2) = .Personnage.ID Then

                            .Combat.Echec = True

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Sort_EchecCritique", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace Challenge

        Module Challenge

            Sub Information(data As String)

                With Bot

                    Try

                        ' Gd 2            ; 0 ;   ; 25 ; 5         ; 25    ; 5 
                        ' Gd ID Challenge ; ? ; ? ; Xp ; Xp Groupe ; Butin ; Butin Groupe

                        Dim separateData As String() = Split(Mid(data, 3), ";")

                        Dim newCombat As New Combat_Variable.Challenge

                        With newCombat

                            Select Case separateData(0)

                                Case "2"

                                    .Nom = "Statue"

                                Case "4"

                                    .Nom = "Sursis"

                                Case "17"

                                    .Nom = "Intouchable"

                                Case "24"

                                    .Nom = "Borne"

                                Case "31"

                                    .Nom = "Statue"

                                Case "38"

                                    .Nom = "Blitzkrieg"

                                Case Else

                                    .Nom = "Inconnu"

                            End Select

                            .ID = separateData(0)
                            .Rate = False
                            .Xp = separateData(3)
                            .Xp_Groupe = separateData(4)
                            .Butin = separateData(5)
                            .Butin_Groupe = separateData(6)

                        End With

                        If .Combat.Challenge.ContainsKey(separateData(0)) Then

                            .Combat.Challenge(separateData(0)) = newCombat

                        Else

                            .Combat.Challenge.Add(separateData(0), newCombat)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Challenge_Information", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Reussi(data As String)

                With Bot

                    Try

                        ' GdOK 41
                        ' GdOK id Challenge

                        If .Combat.Challenge.ContainsKey(Mid(data, 5)) Then

                            EcritureMessage("[Combat]", "Vous avez réussi le challenge : " & .Combat.Challenge(Mid(data, 5)).Nom, Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Challenge_Reussi", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Echoue(data As String)

                With Bot

                    Try

                        ' GdKO 23
                        ' GdKO id Challenge

                        If .Combat.Challenge.ContainsKey(Mid(data, 3)) Then

                            .Combat.Challenge(Mid(data, 3)).Rate = True

                            EcritureMessage("[Combat]", "Challenge raté : " & .Combat.Challenge(Mid(data, 3)).Nom, Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Challenge_Echoue", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace Information

        Module Information

            Sub Mort(data As String)

                With Bot

                    Try

                        ' GA ; 103     ; 01234567 ; -5 
                        ' GA ; ID Info ; ID Tueur ; ID Tué

                        .Combat.Pause += 1800

                        Dim separateData As String() = Split(data, ";")

                        If .Combat.Entite.ContainsKey(separateData(3)) Then

                            .Combat.Entite(separateData(3)).Vivant = False

                            EcritureMessage("[Combat]", .Map.Entite(separateData(3)).Nom & " est mort.", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Information_Mort", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub PointDeVie_Perdu(data As String)

                With Bot

                    Try


                        ' GA ; 100 ; 123456789  ; -1       , -3       , 2
                        ' GA ; 100 ; 1234567    ; 7654321  , 50
                        ' GA ; 100 ; Id Lanceur ; Id Cible , Quantité , ?

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Combat.Entite.ContainsKey(separateInfo(0)) Then

                            If CInt(separateInfo(1)) < 0 Then

                                EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " perd " & separateInfo(1) & " points de vie.", Color.Sienna)
                                .Combat.Entite(separateInfo(0)).Vitalite = CInt(.Combat.Entite(separateInfo(0)).Vitalite + separateInfo(1))

                            Else

                                EcritureMessage("[Dofus]", .Map.Entite(separateInfo(0)).Nom & " n'a rien subi.", Color.Sienna)

                            End If

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Information_PointDeVia_Perdu", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Tour_Actuel(data As String)

                With Bot

                    Try

                        ' GTS 1234567   | 29000 
                        ' GTS Id Unique | Temps restant

                        Dim separateData As String() = Split(Mid(data, 4), "|")

                        If .Personnage.ID = separateData(0) Then

                            .Combat.MonTour = True

                        End If

                        If .Combat.Entite.ContainsKey(separateData(0)) Then

                            .Combat.Entite(separateData(0)).NumeroTour += 1

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Information_Tour_Actuel", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Tour_Passe(data As String)

                With Bot

                    Try

                        ' GTR -1
                        ' GTR Id Joueur/mobs

                        'Si je suis en MITM
                        '.Mitm.Send("GT")

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Information_Tour_Passe", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Tour_Passe_Entite(data As String)

                With Bot

                    Try

                        ' GTF -1
                        ' GTF Id Joueur/mobs

                        If Mid(data, 4) = .Personnage.ID Then

                            .Combat.MonTour = False

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Information_Tour_Passe_Entite", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Action(data As String)

                With Bot

                    Try

                        ' GAF 0  | 01234567 
                        ' GAF Id | Id Entite

                        Dim separateData() As String = Split(Mid(data, 4), "|")

                        'Task.Delay(.Combat.Pause).Wait()

                        .Mitm.Send("GKK" & separateData(0))

                        .Combat.Pause = 0

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Information_Action", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Tour_Information(data As String)

                With Bot

                    Try

                        ' GTM |-1         ; 0               ; 45         ; 5  ; 3  ; 330     ;   ; 45      | 1234567;0;145;6;3;309;;145  
                        ' GTM | ID Unique ; Vivant=0/Mort=1 ; Pdv actuel ; PA ; PM ; Cellule ; ? ; Pdv Max | Next

                        Dim separateData As String() = Split(data, "|")

                        For i = 1 To separateData.Length - 1

                            Dim separate As String() = Split(separateData(i), ";")

                            If .Combat.Entite.ContainsKey(separate(0)) Then

                                With .Combat.Entite(separate(0))

                                    Select Case separate(1)

                                        Case 0 ' Vivant

                                            .Vitalite = separate(2)
                                            .PA = separate(3)
                                            .PM = separate(4)

                                            .Vivant = True

                                        Case 1 ' Mort

                                            .Vivant = False

                                    End Select

                                End With

                            End If

                            If .Map.Entite.ContainsKey(separate(0)) Then

                                .Map.Entite(separate(0)).Cellule = If(separate(1) = "0", separate(5), -1)

                            End If

                        Next

                        .Mitm.Send("GT")

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Information_Tour_Information", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Tour_Ordre(data As String)

                With Bot

                    Try

                        ' GTL | 1234567   | -1 
                        ' GTL | Id Unique | Next

                        Dim separateData As String() = Split(data, "|")

                        For i = 1 To separateData.Length - 1

                            If .Combat.Entite.ContainsKey(separateData(i)) Then

                                .Combat.Entite(separateData(i)).OrdreTour = i

                            End If

                        Next

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Information_Tour_Ordre", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace PM

        Module PM

            Sub Perdu(data As String)

                With Bot

                    Try


                        ' GA ; 129 ; 1234567    ; -1       , 3
                        ' GA ; 129 ; ID_Lanceur ; ID_Cible , Quantité  

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separateInfo(0)) Then

                            If separateData(2) = separateInfo(0) Then

                                EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " utilise " & separateInfo(1) & " PM.", Color.Sienna)

                            Else

                                EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " perd " & separateInfo(1) & " PM.", Color.Sienna)

                            End If

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_PM_Perdu", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Gagne(data As String)

                With Bot

                    Try


                        ' GA ; 128 ; 1234567    ; -1       , 3
                        ' GA ; 128 ; ID_Lanceur ; ID_Cible , Quantité  

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separateInfo(0)) Then

                            EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " gagne " & separateInfo(1) & " PM.", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_PM_Gagne", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Esquive(data As String)

                With Bot

                    Try


                        ' GA ; 309 ; -1         ; 1234567  , 1 
                        ' GA ; 309 ; ID_Lanceur ; ID_Cible , Quantité  

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separateInfo(0)) Then

                            EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " a esquivé la perte de " & separateInfo(1) & " PM.", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_PM_Esquive", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace PO

        Module PO

            Sub Gagne(data As String)

                With Bot

                    Try

                        ' GA ; 117 ; 1234567    ; 7654321  , 1        , 2
                        ' GA ; 117 ; ID_Lanceur ; ID_Cible , Quantité , Nbr tour

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separateInfo(0)) Then

                            EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " gagne " & separateInfo(1) & " PO pendant " & separateInfo(2) & " tours.", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_PO_Gagne", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Perdu(data As String)

                With Bot

                    Try

                        ' GA ; 116 ; 1234567    ; -1       , 1        , 1
                        ' GA ; 116 ; ID_Lanceur ; ID_Cible , Quantité , Nbr tour

                        Dim separateData As String() = Split(data, ";")
                        Dim separate As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separate(0)) Then

                            EcritureMessage("[Combat]", .Map.Entite(separate(0)).Nom & " : " & separate(1) & " à la portée (" & separate(2) & " tour).", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_PO_Perdu", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace PA

        Module PA

            Sub Perdu(data As String)

                With Bot

                    Try

                        ' GA ; 102 ; -1         ; -1       , -5
                        ' GA ; 102 ; ID_Lanceur ; ID_Cible , Quantité

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separateInfo(0)) Then

                            EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " utilise " & separateInfo(1) & " PA.", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_PA_Utilise", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Gagne(data As String)

                With Bot

                    Try

                        ' GA ; 111 ; -1         ; -1       , -5
                        ' GA ; 111 ; ID_Lanceur ; ID_Cible , Quantité

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separateInfo(0)) Then

                            EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " gagne " & separateInfo(1) & " PA.", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_PA_Gagne", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Esquive(data As String)

                With Bot

                    Try

                        ' GA ; 308 ; -1         ; 1234567  , 1 
                        ' GA ; 308 ; ID_Lanceur ; ID_Cible , Quantité  

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separateInfo(0)) Then

                            EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " a esquivé la perte de " & separateInfo(1) & " PA.", Color.Sienna)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_PA_Esquive", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace Etat

        Module Etat_Combat

            Sub Etat(data As String)

                With Bot

                    Try

                        ' GA ; 950 ; 7654321    ; 1234567  , 7       , 0
                        ' GA ; 950 ; id Lanceur ; ID_Cible , Id Etat , nbr de tour ? 

                        Dim separateData As String() = Split(data, ";")
                        Dim separateInfo As String() = Split(separateData(3), ",")

                        If .Combat.Entite.ContainsKey(separateInfo(0)) Then

                            Select Case separateInfo(1)

                                Case "3"

                                Case "7"

                                    .Combat.Entite(separateInfo(0)).Etat = "Pesanteur"
                                    EcritureMessage("[Combat]", .Map.Entite(separateInfo(0)).Nom & " entre dans l'état Pesanteur.", Color.Sienna)

                                Case "8"

                                    .Combat.Entite(separateInfo(0)).Etat = ""

                            End Select

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Etat_Etat", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace Preparation

        Module Preparation

            Sub En_Placement(data As String)

                With Bot

                    Try

                        ' GIC | 1234567   ; 207     ; 1 
                        ' GIC | Id unique ; Cellule ; Numéro equipe

                        Dim separateData As String() = Split(data, "|")

                        separateData = Split(separateData(1), ";")

                        If .Map.Entite.ContainsKey(separateData(0)) Then

                            .Map.Entite(separateData(0)).Cellule = separateData(1)
                            .Combat.Entite(separateData(0)).Equipe = separateData(2)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Preparation_", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Temps_Preparation(data As String)

                With Bot

                    Try


                        ' GJK2 | 0 | 1 | 0 | 30000                                      | 4 
                        ' GJK2 | ? | ? | ? | Temps restant avant que le combat se lance | ?

                        Dim separateData As String() = Split(data, "|")

                        With .Combat

                            .Combat = True
                            .Preparation = CInt(separateData(4)) >= 1
                            .Entite.Clear()

                        End With

                        .Map.Entite.Clear()

                        EcritureMessage("[Combat]", "Il reste " & separateData(4) & " millisecondes avant que le combat se lance automatiquement.", Color.Sienna)


                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Preparation_Temps_Preparation", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Pret(data As String)

                With Bot

                    Try

                        ' GR 1           3107486
                        ' GR Prêt ou non Id Unique

                        Dim id As Integer = Mid(data, 4)

                        If .Combat.Entite.ContainsKey(id) Then

                            .Combat.Entite(id).Pret = Mid(data, 3, 1) = "1"

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Preparation_Pret", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Placement_Case(data As String)

                With Bot

                    Try

                        ' GP bfbubBbPbYcbcfct  | fBfPfXf1f_gdgOg2  | 0 
                        ' GP Cellules Equipe 1 | Cellules equipé 2 | Indique l'équipe dans laquel vous êtes (couleur des cases)

                        Dim separateData As String() = Split(Mid(data, 3), "|")

                        With .Combat

                            .Placement_Cellule.Clear()
                            .Placement = True

                            For i = 0 To 1

                                For a = 1 To separateData(i).Length Step 2

                                    If .Placement_Cellule.ContainsKey(i) Then

                                        .Placement_Cellule(i).Add(ReturnLastCell(Mid(separateData(separateData(2)), a, 2)))

                                    Else

                                        .Placement_Cellule.Add(i, New List(Of Integer)(ReturnLastCell(Mid(separateData(separateData(2)), a, 2))))

                                    End If

                                Next

                            Next

                        End With

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Combat_Preparation_Placement_Case", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

End Namespace
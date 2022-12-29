Namespace Sort

    Module Sort

        Sub Ajoute(data As String)

            With Bot

                Try

                    'SL 179     ~ 5     ~ b                    ; Next Sort
                    'SL Id Sort ~ Level ~ Position Bar de sort ; 

                    Dim separateData As String() = Split(Mid(data, 3), ";")

                    For i = 0 To separateData.Length - 2

                        Dim separate As String() = Split(separateData(i), "~")

                        Dim newSort As New Sort_Variable.Information

                        If VarSort.ContainsKey(separate(0)) AndAlso VarSort(separate(0)).ContainsKey(separate(1)) Then

                            With newSort

                                .ID = separate(0)
                                .Niveau = separate(1)
                                .Nom = VarSort(separate(0))(separate(1)).Nom.ToLower
                                .PO.Minimum = VarSort(separate(0))(separate(1)).PO.Minimum
                                .PO.Maximum = VarSort(separate(0))(separate(1)).PO.Maximum
                                .PA = VarSort(separate(0))(separate(1)).PA
                                .NombreLancerParTour = VarSort(separate(0))(separate(1)).NombreLancerParTour
                                .NombreLancerParTourParJoueur = VarSort(separate(0))(separate(1)).NombreLancerParTourParJoueur
                                .NombreToursEntreDeuxLancers = VarSort(separate(0))(separate(1)).NombreToursEntreDeuxLancers
                                .POModifiable = VarSort(separate(0))(separate(1)).POModifiable
                                .LigneDeVue = VarSort(separate(0))(separate(1)).LigneDeVue
                                .LancerEnLigne = VarSort(separate(0))(separate(1)).LancerEnLigne
                                .CelluleLibre = VarSort(separate(0))(separate(1)).CelluleLibre
                                .ECFiniTour = VarSort(separate(0))(separate(1)).ECFiniTour
                                .Zone.Minimum = VarSort(separate(0))(separate(1)).Zone.Minimum
                                .Zone.Maximum = VarSort(separate(0))(separate(1)).Zone.Maximum
                                .ZoneEffet = VarSort(separate(0))(separate(1)).ZoneEffet
                                .NiveauRequisUp = VarSort(separate(0))(separate(1)).NiveauRequisUp
                                .SortClasse = VarSort(separate(0))(separate(1)).SortClasse
                                .Definition = VarSort(separate(0))(separate(1)).Definition.ToLower
                                .BarreSort = separate(2)

                            End With

                        End If

                        .Sort.Ajoute = newSort

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Sort_Ajoute", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub


        Sub Up(data As String)

            With Bot

                ' SUK 142     ~ 4      ~ B
                ' SUK id sort ~ Niveau ~ barre de sort

                Try

                    Dim separateData As String() = Split(Mid(data, 4), "~")

                    Dim newSort As New Sort_Variable.Information

                    If VarSort.ContainsKey(separateData(0)) AndAlso VarSort(separateData(0)).ContainsKey(separateData(1)) Then

                        With newSort

                            .ID = separateData(0)
                            .Niveau = separateData(1)
                            .Nom = VarSort(separateData(0))(separateData(1)).Nom
                            .PO.Minimum = VarSort(separateData(0))(separateData(1)).PO.Minimum
                            .PO.Maximum = VarSort(separateData(0))(separateData(1)).PO.Maximum
                            .PA = VarSort(separateData(0))(separateData(1)).PA
                            .NombreLancerParTour = VarSort(separateData(0))(separateData(1)).NombreLancerParTour
                            .NombreLancerParTourParJoueur = VarSort(separateData(0))(separateData(1)).NombreLancerParTourParJoueur
                            .NombreToursEntreDeuxLancers = VarSort(separateData(0))(separateData(1)).NombreToursEntreDeuxLancers
                            .POModifiable = VarSort(separateData(0))(separateData(1)).POModifiable
                            .LigneDeVue = VarSort(separateData(0))(separateData(1)).LigneDeVue
                            .LancerEnLigne = VarSort(separateData(0))(separateData(1)).LancerEnLigne
                            .CelluleLibre = VarSort(separateData(0))(separateData(1)).CelluleLibre
                            .ECFiniTour = VarSort(separateData(0))(separateData(1)).ECFiniTour
                            .Zone.Minimum = VarSort(separateData(0))(separateData(1)).Zone.Minimum
                            .Zone.Maximum = VarSort(separateData(0))(separateData(1)).Zone.Maximum
                            .ZoneEffet = VarSort(separateData(0))(separateData(1)).ZoneEffet
                            .NiveauRequisUp = VarSort(separateData(0))(separateData(1)).NiveauRequisUp
                            .SortClasse = VarSort(separateData(0))(separateData(1)).SortClasse
                            .Definition = VarSort(separateData(0))(separateData(1)).Definition
                            .BarreSort = ""

                        End With

                    End If

                    If separateData(1) = "1" Then

                        .Sort.Ajoute = newSort
                        EcritureMessage("[Dofus]", "Tu as appris le sort " & newSort.Nom, Color.Green)

                    Else

                        .Sort.Modifie = newSort
                        EcritureMessage("[Dofus]", "Le sort '" & newSort.Nom & " est désormais niveau : " & separateData(1) & ".", Color.Green)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Sort_Up", ex.Message)

                End Try

            End With

        End Sub


    End Module

End Namespace
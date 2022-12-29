Namespace Map

    Namespace Chargement

        Module Map_Chargement

            Sub Base(data As String)

                With Bot

                    Try

                        'GDM | 534    | 0706131721 | 755220465939692F276671264132675c756345246c4b463b43427a3a4d38556e3c722a356362224e343d3423333e722c3f3a7a4e23553555672c733d602062454e3d474b20633c6335763e63682c43554937222f79333f253235346863387a287039474d4070302532357d586327675668752a3b6a24622962426e78787373512c5853515626536239367320643c53 
                        'GDM | ID Map | Indice     | Clef

                        Dim separateData As String() = Split(data, "|")

                        .Map.Deplacement = False
                        .Map.Bloque.Set()

                        .Combat = New Combat_Variable.Base
                        .Defi = New Defi_Variable.Base
                        .Map = New Map_Variable.Base
                        .Enclos = New Enclos_Variable.Base

                        .Maison.Map.Clear()

                        .Map.ID = separateData(1)

                        Information(separateData(1), separateData(2), separateData(3))

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Chargement_Base", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Private Sub Information(idMap As String, indice As String, clef As String)

                With Bot

                    Try

                        .Map.Bas = Nothing
                        .Map.Droite = Nothing
                        .Map.Gauche = Nothing
                        .Map.Haut = Nothing

                        'Si le dossier map n'existe pas, alors je le créer
                        If Not IO.Directory.Exists("Maps") Then IO.Directory.CreateDirectory("Maps")

                        'Si le fichier de la map n'existe pas alors je le créer et ajoute les infos dedans.
                        If Not IO.File.Exists("Maps/" & idMap & "_" & indice & "X.txt") Then
                            Dim Unpacker As New SwfUnpacker
                            Unpacker.SwfUnpack(idMap & "_" & indice & "X.swf")
                        End If

                        'Je lis le fichier voulu. 
                        Dim mapReader As New IO.StreamReader("Maps/" & idMap & "_" & indice & "X.txt")
                        Dim mapData As String() = Split(mapReader.ReadLine, "|")
                        mapReader.Close()

                        .Map.Largeur = mapData(2)
                        .Map.Hauteur = mapData(3)

                        'Je prépare le nécessaire pour décrypt la map et connaitre se qu'il se trouve dessus.
                        Dim preparedKey As String = PrepareKey(clef)
                        .Map.Handler = UncompressMap(DecypherData(mapData(1), preparedKey, Convert.ToInt64(Checksum(preparedKey), 16) * 2))
                        .Map.Coordonnees = VarMap(idMap)

                        Dim count As Integer = .Map.Handler.Length - 1
                        Dim num As Integer = 0

                        'J'obtient les cellules qui me permet de changer de map via les soleils.
                        For i As Integer = 1 To .Map.Handler.Length - 1
                            If (.Map.Handler(i).movement > 0) Then
                                If (.Map.Handler(i).layerObject1Num = 1030) OrElse (.Map.Handler(i).layerObject2Num = 1030) OrElse (.Map.Handler(i).layerObject2Num = 4088) OrElse (.Map.Handler(i).layerObject1Num = 4088) Then
                                    Dim x As Integer = getX(i, .Map.Largeur)
                                    Dim y As Integer = getY(i, .Map.Largeur)
                                    If x - 1 = y OrElse x - 2 = y Then
                                        If .Map.Gauche = Nothing Then
                                            .Map.Gauche = i 'Gauche
                                        End If
                                    ElseIf x - (.Map.Largeur + .Map.Hauteur) + 5 = y OrElse x - (.Map.Largeur + .Map.Hauteur) + 5 = y - 1 Then
                                        .Map.Droite = i 'Droite
                                    ElseIf y + x = (.Map.Largeur + .Map.Hauteur) - 1 OrElse y + x = (.Map.Largeur + .Map.Hauteur) - 2 OrElse (y + x = (.Map.Largeur + .Map.Hauteur)) Then
                                        If .Map.Bas = Nothing Then
                                            .Map.Bas = i 'Bas
                                        End If
                                    ElseIf y <= 0 Then
                                        y = Math.Abs(y)
                                        If x - y < 3 Then
                                            If .Map.Haut = Nothing Then
                                                .Map.Haut = i 'Haut
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        Interaction(.Map.Handler)

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Chargement_Information", ex.Message)

                    End Try

                End With

            End Sub

            Private Sub Interaction(spritesHandler() As Cell)

                With Bot

                    Try

                        .Map.Interaction.Clear()

                        ' id sprite | nom action | nom item , id action

                        For i As Integer = 1 To 1000

                            If VarInteraction.ContainsKey(spritesHandler(i).layerObject2Num) Then

                                Dim newInteraction As New Interaction_Variable.Base

                                With newInteraction

                                    .Sprite = spritesHandler(i).layerObject2Num.ToString

                                    .Cellule = i.ToString

                                    .Nom = VarInteraction(spritesHandler(i).layerObject2Num).Name.ToLower

                                    .Disponible = True

                                    .Action = VarInteraction(spritesHandler(i).layerObject2Num).DicoInteraction

                                End With

                                .Map.Interaction.Add(i.ToString, newInteraction)

                            End If

                        Next

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Chargement_Interaction", ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace Information

        Module Map_Information

            Sub Entite_Ajoute(data As String)

                With Bot

                    Try

                        Dim separateData As String() = Split(data, "|+")

                        For i = 1 To separateData.Length - 1

                            Dim separate As String() = Split(separateData(i), ";")

                            Dim newMap As New Map_Variable.Entite
                            Dim newCombat As New Combat_Variable.Entite

                            With newMap

                                .Cellule = separate(0)
                                .IDUnique = separate(3)
                                .Orientation = separate(1)
                                .Categorie = separate(5)

                                Select Case separate(5)

                                    Case -1, -2 ' Mobs (en combat)

                                        ' GM|+ 369     ; 1           ; 0 ; -1        ; 149     ; -2      ; 1571^95 ; 2          ; -1 ; -1 ; -1 ; 0 , 0 , 0 , 0 ; 18       ; 5  ; 3  ; 1 
                                        ' GM|+ Cellule ; Orientation ; ? ; id Unique ; Id Mobs ; indice  ; ?       ; Level mobs ; ?  ; ?  ; ?  ; ? , ? , ? , ? ; Vitalité ; PA ; PM ; ? 

                                        .Nom = VarMobs(separate(4))(CInt(separate(7) - 1)).Nom
                                        .Niveau = VarMobs(separate(4))(CInt(separate(7) - 1)).Niveau
                                        .ID = separate(4)

                                        With newCombat

                                            .Vitalite = separate(12)
                                            .PA = separate(13)
                                            .PM = separate(14)
                                            .Resistance.Neutre = VarMobs(separate(4))(separate(7) - 1).RésistanceNeutre
                                            .Resistance.Terre = VarMobs(separate(4))(separate(7) - 1).RésistanceTerre
                                            .Resistance.Feu = VarMobs(separate(4))(separate(7) - 1).RésistanceFeu
                                            .Resistance.Eau = VarMobs(separate(4))(separate(7) - 1).RésistanceEau
                                            .Resistance.Air = VarMobs(separate(4))(separate(7) - 1).RésistanceAir
                                            .Esquive.PA = VarMobs(separate(4))(separate(7) - 1).EsquivePA
                                            .Esquive.PM = VarMobs(separate(4))(separate(7) - 1).EsquivePM

                                        End With

                                    Case -3 ' Mobs (Hors combat)

                                        ' GM|+ 439     ; 5           ; 21      ; -2     ; 198     , 241     ; -3     ;1135^110,1138^100 ; 36 , 32 ; -1       , -1       , -1       ;0,0,0,0;-1,-1,-1;0,0,0,0; 
                                        ' GM|+ Cellule ; Orientation ; Etoile% ; ID Map ; ID Mobs , Id Mobs ; Entité ;                  ; Lv , Lv ; Couleur1 , Couleur2 , Couleur3 ;?,?,?,?;Couleur1,etc... 

                                        .Nom = NomMobs(separate(4))
                                        .ID = separate(4)
                                        .Niveau = separate(7)
                                        .Etoile = separate(2)

                                    Case -4 ' Pnj-------------------

                                        ' GM|+ 152     ; 3           ; 0        ;-1      ; 100    ; -4     ; 9048^100 ; 0  ; -1 ; -1 ; e7b317 ;   ,   ,   ,   ,   ;   ; 0 |
                                        ' GM|+ Cellule ; Orientation ; Etoiles% ; ID Map ; ID PNJ ; Entité ; ?        ; Lv ; ?  ; ?  ; ?      ; ? , ? , ? , ? , ? ; ? ; ? | Next PNJ

                                        .Nom = VarPnj(separate(4))
                                        .Niveau = separate(7)
                                        .ID = separate(4)
                                        .Etoile = separate(2)
                                        .IDUnique = separate(3)
                                        .Classe = "Pnj"

                                    Case -5 ' Mode marchand

                                        ' GM|+ 412     ; 3           ; 0       ; -82    ; Blackarne ; -5     ; 60^100        ; 0  ; 0 ; -1 ; 22e4 , 27c7   , 22ac , 1cf7     , 27c6     ; Awesomes   ; c , 77f73 , 1m , 5w3r4 ; 0
                                        ' GM|+ Cellule ; Orientation ; Etoiles ; ID Map ; Nom       ; Entité ; Classe + sexe ; Lv ; ? ; ?  ; Cac  , Coiffe , Cape , Familier , Bouclier ; Nom guilde ; ? , ?     , ?  , ?     ; ID Sprite Sac du mode marchand

                                        'ID Sprite Sac (se que le marchand vend) 
                                        ' 0 = Tout
                                        ' 1 = Equipement
                                        ' 2 = Divers
                                        ' 3 = Ressource

                                        .Nom = separate(4)
                                        .Classe = ClasseJoueur(separate(6))
                                        .Sexe = SexeJoueur(separate(6))
                                        .Guilde = separate(11)
                                        .ModeMarchand = True
                                        .ID = separate(13)

                                        With .Equipement

                                            Dim separateEquipement As String() = Split(separate(10), ",")

                                            If separateEquipement(0) <> Nothing Then

                                                .Cac = VarItems(Convert.ToInt64(separateEquipement(0), 16)).ID

                                            End If

                                            With .Chapeau

                                                If separateEquipement(1).Contains("~"c) Then

                                                    Dim separateObvijevan As String() = Split(separateEquipement(1), "~")

                                                    .ID = VarItems(Convert.ToInt64(separateObvijevan(0), 16)).ID
                                                    .Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                                    .Forme = Convert.ToInt64(separateObvijevan(2), 16)


                                                ElseIf separateEquipement(1) <> Nothing Then

                                                    .ID = VarItems(Convert.ToInt64(separateEquipement(1), 16)).ID

                                                End If

                                            End With

                                            With .Cape

                                                If separateEquipement(2).Contains("~"c) Then

                                                    Dim separateObvijevan As String() = Split(separateEquipement(2), "~")

                                                    .ID = VarItems(Convert.ToInt64(separateObvijevan(0), 16)).ID
                                                    .Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                                    .Forme = Convert.ToInt64(separateObvijevan(2), 16)


                                                ElseIf separateEquipement(2) <> Nothing Then

                                                    .ID = VarItems(Convert.ToInt64(separateEquipement(2), 16)).ID

                                                End If

                                            End With

                                            If separateEquipement(3) <> Nothing Then

                                                .Familier = VarItems(Convert.ToInt64(separateEquipement(3), 16)).ID

                                            End If

                                            If separateEquipement(4) <> Nothing Then

                                                .Bouclier = VarItems(Convert.ToInt64(separateEquipement(4), 16)).ID

                                            End If

                                        End With

                                    Case -6 ' Percepteur

                                        ' GM|+ 383     ; 1 ; 0      ; -14    ; 2l , 3d ; -6     ; 6000^110 ; 66 ; The Chosen Few ; 8 , n2bh , 1 , 9zldr
                                        ' GM|+ Cellule ; ? ; Etoile ; ID Map ; Nom     ; Entité ; Sprite   ; Lv ; Nom Guilde     ; ? , ?    , ? , ?

                                        .Nom = separate(4)
                                        .IDUnique = separate(3)
                                        .Classe = separate(5)
                                        .Niveau = separate(7)
                                        .Etoile = separate(2)
                                        .Guilde = separate(8)

                                    Case -9 ' Dragodinde

                                            ' 252;1;-1;-9;M;-9;7002^100;Yoshimi;7;43;100

                                            '   .Name = separate(4)
                                            '   .Information = "Monture de : " & separate(7) & vbCrLf &
                                            '           "Niveau : " & separate(8) & vbCrLf &
                                            '           "Dragodinde " & DicoDragodindeId(separate(9))

                                    Case -10 ' Prisme

                                             ' GM|+ 256     ; 1           ; 0      ; -4     ; 1111 ; -10    ; 8101^90 ; 2 ; 4 ; 1
                                             ' GM|+ Cellule ; Orientation ; Etoile ; ID Map ; Nom  ; Entité ; Sprite  ; ? ; ? ; ?

                                             ' .Name = If(separate(4) = 1111, "Prisme Bontârien", "Prise Brâkmarien") ' Nom
                                             ' .Information = "Niveau : " & separate(7) & vbCrLf &
                                             '         "Etoile : " & separate(2)

                                    Case > 0 ' Joueur

                                        ' Hors Combat
                                        ' GM|+ 156     ; 7           ; 0 ; 0123456   ; Linaculer ; 9       ; 90^100      ; 0                          ; 0          , 0 , 0 , 1234567           ; -1       ; -1       ; -1       ;     , 2412~16~7                  , 2411~17~15               ,          ,          ; 0   ;   ;   ;           ;                 ; 0 ;    ;   | Next tchatJoueur
                                        ' GM|~ 300     ; 1           ; 0 ; 0123456   ; linaculer ; 9       ; 90^100      ; 0                          ; 0          , 0 , 0 , 1234567           ; 0        ; 1eeb13   ; 0        ; b4  , 2412~16~18                 , 2411~17~19               ,          ,          ; 1   ;   ;   ; Chernobil ; f,9zldr,x,6k26u ; 0 ; 88 ;
                                        ' GM|+ Cellule ; Orientation ; ? ; Id Unique ; Nom       ; ID Race ; Classe+sexe ; Combat (Equipe bleu/rouge) ; Alignement , ? , ? , ID Unique + Level ; Couleur1 ; Couleur2 ; Couleur3 ; Cac , Coiffe (ID Objet~Lv~Forme) , Cape (ID Objet~Lv~Forme) , Familier , Bouclier ; ?   ; ? ; ? ; Guilde    ; ?               ; ? ; ?  ; ?  

                                        ' En combat 
                                        ' GM|+ 105     ; 1           ; 0 ; 0123456   ; Linaculer ; 9       ; 90^100      ; 0                          ; 99 ; 0          , 0 , 0 , 1234567           ; -1       ; -1       ; -1       ; 241 , 1bea                       , 6ab                      ,          ,          ; 672      ; 7  ; 3  ; 0           ; 1          ; 0        ; 2         ; 0        ; 77         ; 77         ; 0 ;   ;                         
                                        ' GM|+ Cellule ; Orientation ; ? ; Id Unique ; Nom       ; ID Race ; Classe+sexe ; Combat (Equipe bleu/rouge) ; Lv ; Alignement , ? , ? , ID Unique + Level ; Couleur1 ; Couleur2 ; Couleur3 ; Cac , Coiffe (ID Objet~Lv~Forme) , Cape (ID Objet~Lv~Forme) , Familier , Bouclier ; Vitalité ; PA ; PM ; %Rés neutre ; %Rés Terre ; %Rés feu ; %Rés Eau  ; %Res air ; Esquive PA ; Esquive PM ; ? ; ? ; ? 
                                        ' GM|~ = Sur une dragodinde

                                        Dim calculLevel As String()
                                        Dim separateEquipement As String()

                                        If Bot.Combat.Combat Then

                                            separateEquipement = Split(separate(13), ",")
                                            calculLevel = Split(separate(9), ",")

                                        Else

                                            separateEquipement = Split(separate(12), ",")
                                            calculLevel = Split(separate(8), ",")

                                        End If

                                        .Nom = separate(4)
                                        .ModeMarchand = False

                                        .Sexe = SexeJoueur(separate(6))
                                        .Classe = ClasseJoueur(separate(6))

                                        If Bot.Combat.Combat Then

                                            .Alignement = AlignementJoueur(calculLevel(0))
                                            .Niveau = separate(8)

                                            With newCombat

                                                .Vitalite = separate(14)
                                                .PA = separate(15)
                                                .PM = separate(16)
                                                .Resistance.Neutre = separate(17)
                                                .Resistance.Terre = separate(18)
                                                .Resistance.Feu = separate(19)
                                                .Resistance.Eau = separate(20)
                                                .Resistance.Air = separate(21)
                                                .Esquive.PA = separate(22)
                                                .Esquive.PM = separate(23)
                                                .Equipe = separate(7)

                                            End With

                                        Else

                                            .Alignement = AlignementJoueur(calculLevel(0))
                                            .Guilde = separate(16)
                                            .Niveau = CInt(calculLevel(3)) - CInt(separate(3))

                                        End If

                                        With .Equipement

                                            If separateEquipement(0) <> Nothing Then

                                                .Cac = VarItems(Convert.ToInt64(separateEquipement(0), 16)).ID

                                            End If

                                            With .Chapeau

                                                If separateEquipement(1).Contains("~"c) Then

                                                    Dim separateObvijevan As String() = Split(separateEquipement(1), "~")

                                                    .ID = VarItems(Convert.ToInt64(separateObvijevan(0), 16)).ID
                                                    .Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                                    .Forme = Convert.ToInt64(separateObvijevan(2), 16)


                                                ElseIf separateEquipement(1) <> Nothing Then

                                                    .ID = VarItems(Convert.ToInt64(separateEquipement(1), 16)).ID

                                                End If

                                            End With

                                            With .Cape

                                                If separateEquipement(2).Contains("~"c) Then

                                                    Dim separateObvijevan As String() = Split(separateEquipement(2), "~")

                                                    .ID = VarItems(Convert.ToInt64(separateObvijevan(0), 16)).ID
                                                    .Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                                    .Forme = Convert.ToInt64(separateObvijevan(2), 16)


                                                ElseIf separateEquipement(2) <> Nothing Then

                                                    .ID = VarItems(Convert.ToInt64(separateEquipement(2), 16)).ID

                                                End If

                                            End With

                                            If separateEquipement(3) <> Nothing Then

                                                .Familier = VarItems(Convert.ToInt64(separateEquipement(3), 16)).ID

                                            End If

                                            If separateEquipement(4) <> Nothing Then

                                                .Bouclier = VarItems(Convert.ToInt64(separateEquipement(4), 16)).ID

                                            End If

                                        End With

                                End Select

                            End With

                            If .Map.Entite.ContainsKey(separate(3)) Then

                                .Map.Entite(separate(3)) = newMap

                            Else

                                .Map.Entite.Add(separate(3), newMap)

                            End If

                            If .Combat.Combat Then

                                If .Combat.Entite.ContainsKey(separate(3)) Then

                                    .Combat.Entite(separate(3)) = newCombat

                                Else

                                    .Combat.Entite.Add(separate(3), newCombat)

                                End If

                            End If

                            If separate(3) = .Personnage.ID Then

                                .Map.StopDeplacement = False
                                .Map.Deplacement = False
                                .Map.Bloque.Set()
                                ._Send = ""

                            End If

                        Next

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Entite_Ajoute", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Entite_Supprime(data As String)

                With Bot

                    Try

                        ' GM|- 1234567
                        ' GM|- Id Unique

                        Dim idUnique As String = Mid(data, 5)

                        If .Map.Entite.ContainsKey(idUnique) Then

                            .Map.Entite.Remove(idUnique)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Entite_Supprime", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Entite_Deplacement(data As String)

                With Bot

                    Try

                        ' GA  ; 1 ; -1            ; adxfcB
                        ' GA0 ; 1 ; -1            ; adxfcB
                        ' GA  ; ? ; ID entité Map ; Path

                        Dim separateData As String() = Split(data, ";")

                        Dim cellule As Integer = ReturnLastCell(Mid(separateData(3), separateData(3).Length - 1, 2))

                        If .Map.Entite.ContainsKey(separateData(2)) Then

                            .Map.Entite(separateData(2)).Cellule = cellule

                        End If

                        If separateData(2) = .Personnage.ID.ToString Then

                            .Map.Bloque.Reset()

                            .Map.Deplacement = True

                            'Mettre ici le temps d'attente avant de pouvoir se deplacer a nouveau
                            'Temps de déplacement entre le point A et B.
                            Task.Run(Sub() ActionDeplacement(cellule))

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Entite_Deplacement", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Private Sub ActionDeplacement(cellule As Integer)

                With Bot

                    Try

                        If ._Send <> "" Then

                            .Mitm.Send(._Send)

                            ._Send = ""

                        End If

                        'Ici calcul du temps a attendre avant de pouvoir bouger à nouveau.

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "PauseDeplacement", ex.Message)

                    End Try

                End With

            End Sub

            Sub Entite_Escalier(data As String)

                With Bot

                    Try

                        'GA1 ; 4             ; 01234567  ; 76543210 , 342
                        'GA1 ; Teleportation ; ID Joueur ; ID_Cible , Cellule

                        Dim separateData As String() = Split(data, ";")

                        Dim separate As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separate(0)) Then

                            .Map.Entite(separate(0)).Cellule = separate(1)

                        End If

                        If separate(0) = .Personnage.ID Then

                            .Mitm.Send("GKK1")
                            .Map.Deplacement = False
                            .Map.Bloque.Set()

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Entite_Escalier", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub 'Sufokia > escalié

            Sub Entite_Teleportation(data As String)

                With Bot

                    Try

                        'GA ; 4             ; 01234567  ; 76543210 , 342
                        'GA ; Teleportation ; ID Joueur ; ID_Cible , Cellule

                        Dim separateData As String() = Split(data, ";")

                        Dim separate As String() = Split(separateData(3), ",")

                        If .Map.Entite.ContainsKey(separate(0)) Then

                            .Map.Entite(separate(0)).Cellule = separate(1)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Entite_Teleportation", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Objet_Ajoute(data As String)

                With Bot

                    Try

                        ' GDO+ 358     ; 7596     ; 1                          ; 1500              ; 1500           |
                        ' GDO+ Cellule ; Id Objet ; Information supplémentaire ; Résistance actuel ; Résistance Max | Next

                        Dim separateData As String() = Split(Mid(data, 5), "|")

                        For i = 0 To separateData.Length - 1

                            Dim separate As String() = Split(separateData(i), ";")

                            Dim newMap As New Map_Variable.Objet

                            With newMap

                                .Cellule = separate(0)

                                .IdUnique = separate(1)

                                .Nom = VarItems(separate(1)).Nom

                                If separate(2) = "1" Then

                                    .Resistance.Minimum = separate(3)
                                    .Resistance.Maximum = separate(4)

                                End If

                            End With

                            .Map.Objet.Add(separate(0), newMap)

                        Next


                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Objet_Ajoute", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Objet_Supprime(data As String)

                With Bot

                    Try

                        ' GDO- 123 
                        ' GDO- Cellule

                        Dim id As Integer = Mid(data, 5)

                        If .Map.Objet.ContainsKey(id) Then

                            .Map.Objet.Remove(id)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information__Objet_Supprime", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Orientation(data As String)

                With Bot

                    Try

                        ' eD 2594870   | 7 
                        ' eD Id Unique | Orientation

                        Dim separationData As String() = Split(Mid(data, 3), "|")

                        If .Map.Entite.ContainsKey(separationData(0)) Then

                            .Map.Entite(separationData(0)).Orientation = separationData(1)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Orientation", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Agression(data As String)

                With Bot

                    Try

                        ' GA ; 906 ; 123456789    ; 987654321
                        ' GA ; 906 ; Id agresseur ; id Agressé

                        Dim separateData As String() = Split(data, ";")

                        EcritureMessage("[Dofus]", .Map.Entite(separateData(2)).Nom & " agresse " & .Map.Entite(separateData(3)).Nom, Color.Green)

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Agression", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Assis_Debout(data As String)

                With Bot

                    Try

                        ' eUK Id Joueur | 1
                        ' eUK Id Joueur | Assis/Debout

                        Dim separateData As String() = Split(Mid(data, 4), "|")

                        If .Map.Entite.ContainsKey(separateData(0)) Then

                            .Map.Entite(separateData(0)).Assis = separateData(1) <> "0"

                        End If

                        If separateData(0) = .Personnage.ID Then

                            EcritureMessage("[Dofus]", "Vous êtes assis, votre régénération de point de vie augmente.", Color.Green)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Map_Information_Assis_Debout", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Module Map_Function

        Public Function NomMobs(name As String) As String

            Dim resultat As String = ""

            Try

                Dim separateName As String() = Split(name, ",")


                For i = 0 To separateName.Length - 1

                    resultat &= VarMobs(separateName(i))(0).Nom & " , "

                Next

            Catch ex As Exception

            End Try

            Return resultat

        End Function

        Public Function ClasseJoueur(Information As String) As String

            Try

                Dim Classe As String() = {"Feca", "Osamodas", "Enutrof", "Sram", "Xelor", "Ecaflip", "Eniripsa", "Iop", "Cra", "Sadida", "Sacrieur", "Pandawa"}
                Dim separate As String() = Split(Information, "^") ' 90^100
                Dim Résultat As Integer = Mid(separate(0), 1, Len(separate(0)) - 1) ' 90

                If Résultat < 12 Then

                    Return Classe(Résultat - 1)

                End If

            Catch ex As Exception

            End Try

            Return "Inconnu"

        End Function

        Public Function SexeJoueur(Information As String) As String

            Try

                Dim sexe As String() = {"Homme", "Femme"}
                Dim separate As String() = Split(Information, "^") ' 90^100

                Dim number As Integer = Mid(separate(0), Len(separate(0)), Len(separate(0)) - 1)

                Return If(number > 1, "Inconnu", sexe(number))

            Catch ex As Exception

            End Try

            Return "Inconnu"

        End Function

        Public Function AlignementJoueur(numero As String) As String

            Try

                Dim Alignement() As String = {"Neutre", "Bontarien", "Brakmarien"}

                Return Alignement(numero)

            Catch ex As Exception

            End Try

            Return "Inconnu"

        End Function

    End Module

End Namespace

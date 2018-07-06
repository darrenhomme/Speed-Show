'Speed Show 1.0

Dim ItemArray as Array[String]

Dim ItemArrayPosX as Array[Double]
Dim ItemArrayPosY as Array[Double]
Dim ItemArrayScaleX as Array[Double]
Dim ItemArrayScaleY as Array[Double]

Dim PromoArray as Array[Integer]


Dim Counter as Integer
Counter = 1

Sub ResetArrays()
     ItemArray.Clear

     ItemArrayPosX.Clear
     ItemArrayPosY.Clear
     ItemArrayScaleX.Clear
     ItemArrayScaleY.Clear

     PromoArray.Clear
End Sub

Sub LoadItems()
     Counter = CInt(GetSmm("Speed_Show_Preview")) 

     ResetArrays()
     ArrayLoadLoop()
     
     PopulateGraphic(Counter)
End Sub

Sub UpdateItems()
     ResetArrays()
     ArrayLoadLoop()

     Counter = CInt(GetSmm("Speed_Show_Preview"))     
     PopulateGraphic(Counter)
End Sub


Sub ArrayLoadLoop()
     For i = 1 to 60
           If GetText("Input" & i) <> "" then
                ItemArray.Push(GetText("Input" & i))
                
                ItemArrayPosX.Push(GetPositionX("Input" & i))
                ItemArrayPosY.Push(GetPositionY("Input" & i))
                ItemArrayScaleX.Push(GetScaleX("Input" & i))
                ItemArrayScaleY.Push(GetScaleY("Input" & i))

                PromoArray.Push(CInt(GetText("InputPromo" & i)))
           End If
     Next
End Sub

Sub PopulateGraphic(StartLocation as Integer)

     For i = 1 to 6
          ClearItem(i)
     Next

     Dim EndLocation as Integer
     EndLocation = ItemArray.UBound + 1

     For i = 0 to 5
          If StartLocation + i <= EndLocation Then SetItem(i + 1, StartLocation + i)
     Next

     If CInt(GetSmm("Item_1")) = 1 then
          DpSetInt("Next_IO", 1)
     Else
          DpSetInt("Next_IO", 0)
     End If

End Sub


Sub ClearItem(Value as Integer)
     DpSetInt("Item_" & Value, 0)
     DpSetInt("Item_" & Value & "_Promo", PromoArray[Value - 1])
     SetImage("Item_" & Value & "_Image", "")
     SetText("Label" & Value, "")
End Sub

Sub SetItem(Spot as Integer, Value as Integer)
     DpSetInt("Item_" & Spot, 1)
     DpSetInt("Item_" & Spot & "_Promo", PromoArray[Value - 1])
     SetImage("Item_" & Spot & "_Image", "http://images.shophq.com/is/image/ShopHQ/" & ItemArray[Value - 1] & "?hei=600&wid=600&op_sharpen=1")

     SetPositionX("Item_" & Spot & "_Image", ItemArrayPosX[Value - 1])
     SetPositionY("Item_" & Spot & "_Image", ItemArrayPosY[Value - 1])
     SetScaleX("Item_" & Spot & "_Image", ItemArrayScaleX[Value - 1])
     SetScaleY("Item_" & Spot & "_Image", ItemArrayScaleY[Value - 1])

     SetText("Label" & Spot, ItemArray[Value - 1])
End Sub

Sub NextItem()
     Counter = Counter + 1
     PopulateGraphic(Counter)
     stage.finddirector("Item_Move").startanimation()
End Sub


Sub PreviewItem()
     LoadItems()
     Dim NumForPreview as Integer
     NumForPreview = CInt(GetSmm("Speed_Show_Preview"))
     PopulateGraphic(NumForPreview)
End Sub









Function GetSmm(VarName As String) As String
	GetSmm = (string)Scene.Map[ VarName ]
End Function

Function GetText(VarName As String) As String
     GetText = Scene.FindContainer(VarName).geometry.text
End Function

Sub SetText(VarName as String,Value as String)
     Scene.FindContainer(VarName).geometry.text = Value
End Sub

Sub SetImage(VarName as String,Value as String)
     Scene.FindContainer(VarName).CreateTexture(Value)
End Sub

Sub DpSet(Name as String,Value as String)
	''Sets a DataPool variable AND Scene SMM to given value
	Scene.GetScenePluginInstance("DataPool").SetParameterString("Data1", Name & "=\"\"" & Value & "\"\";")
	Scene.Map[ Name ] = Value
End Sub

Sub DpSetInt(Name as String,Value as Integer)
	''Sets a DataPool variable AND Scene SMM to given value
	Scene.GetScenePluginInstance("DataPool").SetParameterString("Data1", Name & "=\"\"" & Value & "\"\";")
	Scene.Map[ Name ] = Value
End Sub

Function GetPositionX(Name as String) as Double
     GetPositionX = Scene.FindContainer(Name).position.x
End Function

Function GetPositionY(Name as String) as Double
     GetPositionY = Scene.FindContainer(Name).position.y
End Function

Function GetScaleX(Name as String) as Double
     GetScaleX = Scene.FindContainer(Name).scaling.x
End Function

Function GetScaleY(Name as String) as Double
     GetScaleY = Scene.FindContainer(Name).scaling.y
End Function

Sub SetPositionX(Name as String,Value as Double)
     Scene.FindContainer(Name).position.x = Value
End Sub

Sub SetPositionY(Name as String,Value as Double)
     Scene.FindContainer(Name).position.y = Value
End Sub

Sub SetScaleX(Name as String,Value as Double)
     Scene.FindContainer(Name).scaling.x = Value
End Sub

Sub SetScaleY(Name as String,Value as Double)
     Scene.FindContainer(Name).scaling.y = Value
End Sub
Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.IO
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [output template] [3 to 10 palm images]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "example {0} template image1.png image2.png image3.png", TutorialUtils.GetAssemblyName())
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.PalmExtraction"

		TutorialUtils.PrintTutorialHeader(args)
		If args.Length < 4 OrElse args.Length > 11 Then
			Return Usage()
		End If

		Try
			' Obtain license
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using biometricClient As New NBiometricClient()
				Using subject As New NSubject()
					For i As Integer = 1 To args.Length - 1
						Dim palm As NPalm = New NPalm With {.SessionId = 1, .FileName = args(i)}
						subject.Palms.Add(palm)
					Next i

					Dim status As NBiometricStatus = biometricClient.CreateTemplate(subject)
					If status <> NBiometricStatus.Ok Then
						Console.WriteLine("Failed to create or generalize templates. Status: {0}.", status)
						Return -1
					End If

					Console.WriteLine("Generalization completed successfully")
					Console.Write("Saving template to '{0}' ... ", args(0))
					Using buffer As NBuffer = subject.GetTemplateBuffer()
						File.WriteAllBytes(args(0), buffer.ToArray())
					End Using
					Console.WriteLine("done")
				End Using
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class

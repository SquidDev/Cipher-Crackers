@set PATH=%PATH%;"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow";"C:\Program Files (x86)\MSBuild\12.0\bin\"
MSBuild.exe "Cipher.sln" /verbosity:minimal && vstest.console "Testing\bin\Release\Testing.dll"

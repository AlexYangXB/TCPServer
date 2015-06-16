set curdir=%~dp0\
del /a /f /s /q "%curdir%*.FSN"
del /a /f /s /q "%curdir%*.log"
del /a /f /s /q "%curdir%MaterialSkin\bin\*.dll"
del /a /f /s /q "%curdir%ServerForm\bin\*.dll"
del /a /f /s /q "%curdir%TcpServer\bin\*.dll"
del /a /f /s /q "%curdir%KyBll\bin\Debug\*.dll"
del /a /f /s /q "%curdir%KyModel\bin\*.dll"
pause

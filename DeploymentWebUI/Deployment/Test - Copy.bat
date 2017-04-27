notepad.exe

set GIT_PATH="C:\Program Files (x86)\Git\bin\git.exe"

D:

cd D:\GitRepo\Deployment\MediaValetAPI

REM echo "=========clean -fdx branch=========" 

%GIT_PATH% clean -fdx 

ping 127.0.0.1 -n 7 > nul

%GIT_PATH% pull upstream stage



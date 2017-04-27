
set GIT_PATH="C:\Program Files (x86)\Git\bin\git.exe"

D:

cd D:\GitRepo\Deployment\MediaValetAPI


REM echo "=========clean -fdx branch=========" 

%GIT_PATH% clean -fdx 

echo "=========checkout stage=========" 

%GIT_PATH% checkout stage 

echo =========delete deployment branch========= 

%GIT_PATH% branch -D deployment 

echo "=========push delete branch=========" 

%GIT_PATH% push origin :deployment  


echo "=========clean -fdx branch=========" 

%GIT_PATH% clean -fdx  

echo "=========pull usptream stage=========" 

%GIT_PATH% pull upstream stage 


echo "=========pull checkout -b  deployment=========" 

%GIT_PATH% checkout -b  deployment 

%GIT_PATH% push origin deployment 



set GIT_PATH="C:\Program Files (x86)\Git\bin\git.exe"

D:

cd D:\GitRepo\Deployment\MediaValetAPI

DEL "D:\GitRepo\Deployment\MediaValetAPI\log.txt"

echo =========checkout deployment========= 

%GIT_PATH% checkout deployment 

echo "=========checkout -- .=========" 

%GIT_PATH% checkout -- . 

echo "=========reset head *.*=========" 

%GIT_PATH% reset head *.* 

echo =========pull origin deployment=========" 

%GIT_PATH% pull origin deployment 

echo "=========call UpdateDllReference.exe=========" 

call D:\Projects\UpdateDllReference\UpdateDllReference\bin\Debug\UpdateDllReference.exe 

echo "=========add *.csproj=========" 

%GIT_PATH% add *.csproj 

echo "=========commit -m=========" 

%GIT_PATH% commit -m "updated dll refernces" 

echo "=========push origin deployment=========" 

%GIT_PATH% push origin deployment



set GIT_PATH="C:\Program Files (x86)\Git\bin\git.exe"

D:

cd D:\GitRepo\Deployment\MediaValetAPI

DEL "D:\GitRepo\Deployment\MediaValetAPI\log.txt"

echo =========checkout deployment========= >>log.txt

%GIT_PATH% checkout deployment >> log.txt

echo "=========checkout -- .=========" >>log.txt

%GIT_PATH% checkout -- . >> log.txt

echo "=========reset head *.*=========" >>log.txt

%GIT_PATH% reset head *.* >> log.txt

echo =========pull origin deployment=========" >>log.txt

%GIT_PATH% pull origin deployment >> log.txt

echo "=========call UpdateDllReference.exe=========" >>log.txt

call D:\Projects\UpdateDllReference\UpdateDllReference\bin\Debug\UpdateDllReference.exe >> log.txt

echo "=========add *.csproj=========" >>log.txt

%GIT_PATH% add *.csproj >> log.txt

echo "=========commit -m=========" >>log.txt

%GIT_PATH% commit -m "updated dll refernces" >> log.txt

echo "=========push origin deployment=========" >>log.txt

%GIT_PATH% push origin deployment


set GIT_REDIRECT_STDERR=2
echo "Setting up the folders"

mkdir CASE_SV
cd CASE_SV

echo "Retrieving Project info"
git clone "https://github.com/Stefan94V/EndCase-InfoSupport"
cd EndCase-InfoSupport
cd FrontEnd
cd CursusAdmininistratie-WebApp

npm install
echo "Voor de zekerheid nog wat installs"
npm install @angular/cdk
npm install @angular/animations

echo "Installing done"
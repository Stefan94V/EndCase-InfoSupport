#!setup

echo "Setting up the folders"

mkdir "EndCase"
cd "EndCase"

echo "Retrieving Project info"
git clone "https://github.com/Stefan94V/EndCase-InfoSupport"

cd FrontEnd
cd CursusAdmininistratie-WebApp

npm install
echo "Voor de zekerheid nog wat installs"
npm install @angular/cdk
npm install @angular/animations

echo "Installing done"
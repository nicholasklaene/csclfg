echo "Deploying shared infrastructure"

cd cognito
bash build_config.sh

cd ../baseui
sam deploy
cd ../cognito
sam deploy
cd ..

echo "Deployed shared infrastructure"

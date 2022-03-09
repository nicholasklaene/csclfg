echo "Deploying shared infrastructure"

cd cognito
bash build_config.sh
sam deploy

cd ../base_ui
sam deploy
cd .. 

echo "Deployed shared infrastructure"

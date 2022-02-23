# TODO: setup true cicd
read -p "Environment: " ENV

if [ "$ENV" != "test" ] && [ "$ENV" != "prod" ];
then
    printf "Invalid environment. Aborting.\n"
    exit 1
fi

if [ "$ENV" == "prod" ];
then
    read -p "Are you sure you want to deploy to production? (y/n) " CONFIRM_PROD_DEPLOY
    
    if [ "$CONFIRM_PROD_DEPLOY" != "y" ];
    then
        printf "Aborting"
        exit 1
    fi
fi

cd api
bash build_layer.sh
cd ..

sam build
echo "y" | sam deploy --capabilities CAPABILITY_AUTO_EXPAND CAPABILITY_IAM --config-env $ENV

read -p "App subdomain: " subdomain

echo "$subdomain" | bash buildOneSiteConfig.sh

cd ../../../backend/api

bash buildLayer.sh

cd ../../infrastructure/individual

sam deploy --config-file "./config/sam/$subdomain-samconfig.toml"

cd scripts

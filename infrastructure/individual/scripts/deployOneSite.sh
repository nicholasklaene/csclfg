read -p "App subdomain: " subdomain

echo "$subdomain" | bash buildOneSite.sh

cd ..

sam deploy --config-file "./config/sam/$subdomain-samconfig.toml"

cd scripts

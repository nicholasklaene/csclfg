read -p "App subdomain: " subdomain

echo "$subdomain" | bash buildOneSiteConfig.sh

# prolly add buildLayer back 

sam build
sam deploy --config-file "./config/sam/$subdomain-samconfig.toml"

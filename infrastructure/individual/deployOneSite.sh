read -p "App subdomain: " subdomain

date=$(date '+%Y-%m-%d %H:%M:%S')

# For some reason log file redirection breaks the deployment
# sam build &> "./logs/$date-$subdomain-build.log" &
# sam deploy --config-file "./config/sam/$subdomain-samconfig.toml" &> "./logs/$date-$subdomain-deploy.log" &

sam build
sam deploy --config-file "./config/sam/$subdomain-samconfig.toml" 
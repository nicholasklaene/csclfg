read -p "App subdomain: " subdomain

date=$(date '+%Y-%m-%d %H:%M:%S')

sam build &> "./logs/$date-$subdomain-build.log" &
sam deploy --config-file "./config/sam/$subdomain-samconfig.toml" &> "./logs/$date-$subdomain-deploy.log" &

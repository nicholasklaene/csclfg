# All sites share the same cognito authentication provider
# so, each time a new site is added it must have its callback/logout
# urls added to the cognito config. TOML does not provider a nice
# way of doing this

read -p "Admin email: " admin_email
read -p "App name: " app_name
read -p "App domain: " app_domain
read -p "AWS Region: " aws_region

if aws s3api head-bucket --bucket "$app_name.templates" 2>/dev/null;
then
  aws s3api create-bucket --bucket "$app_name.templates" --region $aws_region
fi

readarray -t subdomains < ../../sites.txt

identity_samconfig="
version = 0.1
[default]
[default.deploy]
[default.deploy.parameters]
stack_name = \"$app_name-identity\"
s3_bucket = \"$app_name.templates\"
s3_prefix = \"$app_name-identity\"
region = \"$aws_region\"
confirm_changeset = false
capabilities = \"CAPABILITY_IAM\"
"

callback_urls="CallbackUrls=\\\""
logout_urls="LogoutUrls=\\\""

for i in ${!subdomains[@]};
do
  subdomain=${subdomains[$i]}
  callback_urls+="https://$subdomain.$app_domain/oauth/callback,"
  logout_urls+="https://$subdomain.$app_domain/logout,"
done

callback_urls+="http://localhost:8080/oauth/callback,https://oauth.pstmn.io/v1/callback"
callback_urls+="\\\""

logout_urls+="http://localhost:8080/logout"
logout_urls+="\\\""

# pain
identity_samconfig+="parameter_overrides = \"AppName=\\\""
identity_samconfig+="$app_name"
identity_samconfig+="\\\""
identity_samconfig+=", "
identity_samconfig+="AppDomain=\\\""
identity_samconfig+="$app_domain"
identity_samconfig+="\\\""
identity_samconfig+=", "
identity_samconfig+="AdminEmail=\\\""
identity_samconfig+="$admin_email"
identity_samconfig+="\\\""
identity_samconfig+=", "
identity_samconfig+=$callback_urls
identity_samconfig+=", "
identity_samconfig+=$logout_urls
identity_samconfig+="\""

identity_samconfig+=$'\nimage_repositories = []'

echo "$identity_samconfig" > samconfig.toml

# All sites share the same cognito authentication provider
# so, each time a new site is added it must have its callback/logout
# urls added to the cognito config. TOML does not provider a nice
# way of doing this

readarray -t subdomains < ../../sites.txt

identity_samconfig="
version = 0.1
[default]
[default.deploy]
[default.deploy.parameters]
stack_name = \"studyseeking-identity\"
s3_bucket = \"studyseeking.templates\"
s3_prefix = \"studyseeking-identity\"
region = \"us-east-1\"
confirm_changeset = false
capabilities = \"CAPABILITY_IAM\"
"

callback_urls="CallbackUrls=\\\""
logout_urls="LogoutUrls=\\\""

for i in ${!subdomains[@]};
do
  subdomain=${subdomains[$i]}
  callback_urls+="https://$subdomain.studyseeking.com/oauth/callback,"
  logout_urls+="https://$subdomain.studyseeking.com/logout,"
done

callback_urls+="http://localhost:8080/oauth/callback,https://oauth.pstmn.io/v1/callback"
callback_urls+="\\\""

logout_urls+="http://localhost:8080/logout"
logout_urls+="\\\""

identity_samconfig+="parameter_overrides = \""
identity_samconfig+=$callback_urls
identity_samconfig+=", "
identity_samconfig+=$logout_urls
identity_samconfig+="\""

identity_samconfig+=$'\nimage_repositories = []'

echo "$identity_samconfig" > samconfig.toml

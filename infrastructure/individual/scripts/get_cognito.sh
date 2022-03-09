all_user_pools_json=`aws cognito-idp list-user-pools --max-results 20`

user_pool=$(jq -r '.UserPools[] | select(.Name=="studyseeking-UserPool")' <<< "${all_user_pools_json}")

user_pool_id=$(jq -r '.Id' <<< ${user_pool})

all_user_pool_clients_json=`aws cognito-idp list-user-pool-clients --user-pool-id $user_pool_id` 

user_pool_client=$(jq -r '.UserPoolClients[] | select(.ClientName=="studyseeking-UserPoolClient") | .ClientId' <<< "${all_user_pool_clients_json}")

echo "$user_pool_id"
echo "$user_pool_client"

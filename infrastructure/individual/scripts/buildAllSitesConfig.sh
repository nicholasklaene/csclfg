readarray -t sites < ../../sites.txt

bash getCognito.sh

for i in ${!sites[@]};
do
  site=${sites[$i]}
  echo "$site" | bash buildOneSiteConfig.sh
done

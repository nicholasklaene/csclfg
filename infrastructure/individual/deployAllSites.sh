readarray -t sites < ../sites.txt

for i in ${!sites[@]};
do
  site=${sites[$i]}
  echo "$site" | bash deployOneSite.sh &
done

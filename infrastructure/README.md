# Infrastructure

## To add a new site:

- add site to sites.txt
- cd shared/
- bash deploy.sh
- cd ../individual/scripts
- bash deplyOneSite.sh
- create DNS records in AWS ACM console (must be done manually)

#!/bin/bash
BUCKET=csclfg.com
DISTRIBUTION=E2URAEONH262SG

npm run build

aws s3 cp ./dist/ s3://$BUCKET/ --recursive

aws cloudfront create-invalidation --distribution-id $DISTRIBUTION --paths '/*'

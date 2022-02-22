cd api
bash build_layer.sh
cd ..

sam build
echo "y" | sam deploy --capabilities CAPABILITY_AUTO_EXPAND CAPABILITY_IAM

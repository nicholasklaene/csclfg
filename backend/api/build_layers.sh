# Build utils layer
rm -rf src/lambda_layers/utils_layer/utils/*
cp src/utils/* src/lambda_layers/utils_layer/utils

# Build models layer
rm -rf src/lambda_layers/models_layer/models/*
cp src/models/* src/lambda_layers/models_layer/models

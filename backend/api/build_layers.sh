# Build utils layer
rm -rf layers/utils_layer/utils/*
cp src/utils.py layers/utils_layer/

# Build models layer
rm -rf layers/models_layer/models/*
cp src/models/* layers/models_layer/models

# Build data layer
rm -rf layers/data_layer/data/*
cp src/data/* layers/data_layer/data

# Build lib layer (nothing to do)

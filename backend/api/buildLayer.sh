rm -rf layer/models/* && rm -rf layer/data/*

rmdir layer/models && rmdir layer/data

rm -f layer/utils.py

mkdir layer/models && mkdir layer/data

cp src/models/* layer/models
cp src/data/* layer/data
cp src/utils.py layer/utils.py

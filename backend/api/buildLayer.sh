rm -rf layer/python/models/* && rm -rf layer/python/data/*

rmdir layer/python/models && rmdir layer/python/data

rm -f layer/python/utils.py

mkdir layer/python/models && mkdir layer/python/data

cp src/models/* layer/python/models
cp src/data/* layer/python/data
cp src/utils.py layer/python/utils.py

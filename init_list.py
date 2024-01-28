import pandas as pd

# read translation file

dic = {}
with open("translate.csv", "r", encoding='utf-8') as f:
    for line in f.readlines():
        line = line.strip()
        if line == "":
            continue
        # print(line)
        key, value, _ = line.split(",")
        dic[key] = value
# print(dic)

df = pd.read_csv("full.csv")

init_com_list = set()


for row in df.iterrows():
    if row[1]["Name"] not in dic:
        print(f"not found {row[1]['Name']}")
        continue
    self_name = dic[row[1]["Name"]]
    com_list = []
    result_list = []
    for col in df.columns:
        if col != "Name" and pd.notnull(row[1][col]):
            if row[1][col] not in dic:
                print(f"not found {row[1][col]}")
                continue
            if col not in dic:
                print(f"not found {col}")
                continue
            another_name = dic[col]
            result_name = dic[row[1][col]]
            init_com_list.add(another_name)
            init_com_list.add(self_name)

for row in df.iterrows():
    if row[1]["Name"] not in dic:
        print(f"not found {row[1]['Name']}")
        continue
    self_name = dic[row[1]["Name"]]
    com_list = []
    result_list = []
    for col in df.columns:
        if col != "Name" and pd.notnull(row[1][col]):
            if row[1][col] not in dic:
                print(f"not found {row[1][col]}")
                continue
            if col not in dic:
                print(f"not found {col}")
                continue
            another_name = dic[col]
            result_name = dic[row[1][col]]
            if result_name in init_com_list:
                init_com_list.remove(result_name)

print(list(init_com_list))
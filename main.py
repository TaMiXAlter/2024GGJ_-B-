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
            com_list.append(another_name)
            result_list.append(result_name)
    com_list_str = ""
    result_list_str = ""
    if len(com_list) != 0:
        com_list = [""]+com_list
        result_list = [""]+result_list
        com_list_str = "\n    - ".join(com_list)
        result_list_str = "\n    - ".join(result_list)
        # print(f"{self_name} - {com_list_str} - {result_list_str}")
            
    #     print(f"{self_name} - {c} - {r}")
    texture = '{fileID: 0}'
    try:
        with open(f"Assets/Img/Element/{self_name}.png.meta", "r") as f:
            # read as dict
            lines = f.readlines()
            for line in lines:
                if line.startswith("guid:"):
                    texture_guid = line.split(": ")[1].strip()
                    texture = f"{{fileID: 21300000, guid: {texture_guid}, type: 3}}"
                    break
    except:
        pass
    info = f"""%YAML 1.1\n%TAG !u! tag:unity3d.com,2011:\n--- !u!114 &11400000\nMonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{ fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: 0}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: 2c4c12063bda91247a9644ded558bfb5, type: 3}}
  m_Name: {self_name}
  m_EditorClassIdentifier: 
  Texture: {texture}
  Name: {self_name}
  Combinations:
    m_keys: {com_list_str}
    m_values: {result_list_str}
    """
    with open(f"Assets/Items/{self_name}.asset", "w", encoding='utf-8') as f:
        f.write(info)
    

import pandas as pd

df = pd.read_csv("full.csv")

for row in df.iterrows():
    self_name = row[1]["Name"]
    com_list = []
    result_list = []
    for col in df.columns:
        if col != "Name" and pd.notnull(row[1][col]):
            another_name = col
            result_name = row[1][col]
            com_list.append(another_name)
            result_list.append(result_name)
    com_list_str = ""
    result_list_str = ""
    if len(com_list) != 0:
        com_list = [""]+com_list
        result_list = [""]+result_list
        com_list_str = "\n    - ".join(com_list)
        result_list_str = "\n    - ".join(result_list)
        print(f"{self_name} - {com_list_str} - {result_list_str}")
            
    #     print(f"{self_name} - {c} - {r}")
         
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
  Texture: {{fileID: 0}}
  Name: {self_name}
  Combinations:
    m_keys: {com_list_str}
    m_values: {result_list_str}
    """
    with open(f"Assets/Items/{self_name}.asset", "w", encoding='utf-8') as f:
        f.write(info)
    

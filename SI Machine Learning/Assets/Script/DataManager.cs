using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text;

[XmlRoot("Data")]
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string path;

    private XmlSerializer serializer = new XmlSerializer(typeof(Data));
    Encoding encoding = Encoding.GetEncoding("UTF-8");

    public void Awake()
    {
        instance = this;
        SetPath();
    }

    private void SetPath()
    {
        path = Path.Combine(Application.persistentDataPath, "Data.xml");
    }

    public void Save(List<NeuralNetwork> _nets)
    {
        StreamWriter streamWriter = new StreamWriter(path, false,encoding);
        Data data = new Data {nets = _nets};

        serializer.Serialize(streamWriter, data);
    }

    public Data Load()
    {
        if (File.Exists(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);

            return serializer.Deserialize(fileStream) as Data;
        }

        return null;
    }
}

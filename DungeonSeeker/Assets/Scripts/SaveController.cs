using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


// �ڵ� ��ó ��α� : https://dyunace.tistory.com/29

public class SaveController : MonoBehaviour
{
    private static string saveFolderPath;
    private static string saveFilePath = saveFolderPath;

    /// <summary>
    /// ���� ��� ����. ��� ���� ���̺� ���� �̸� ���� �־ ���.
    /// ���� ��θ� ����� ��� �� �Լ��� ������� �ʰ�, ���� ���� ���� �Է��Ͽ� ��� ����
    /// </summary>
    /// <param name="_path">���� ���</param>
    /// <param name="_fileName">���� �̸�. ������ ���̴°� ���� �� ��. => (###.json)</param>
    public static void SetFilePath(string _path, string _fileName)
    {
        saveFolderPath = _path;
        saveFilePath = saveFolderPath + "/" + _fileName;
    }

    /// <summary>
    /// ���̺� �ý���. 
    /// </summary>
    /// <typeparam name="Tclass">Generic Ÿ�� ��. �Է����� �ʾƵ� ��� ����</typeparam>
    /// <param name="data">������ ������ Ÿ��</param>
    public static void Save<Tclass>(Tclass data)
    {
        CheckFile();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }

    /// <summary>
    /// �ε� �ý���
    /// </summary>
    /// <typeparam name="Tclass">Generic Ÿ�� ��. ��ȯ ���� Ÿ���� �Է��� ��</typeparam>
    /// <returns>Generic Ÿ���� ������ ����� ���</returns>
    public static Tclass Load<Tclass>()
    {
        CheckFile();

        string json = File.ReadAllText(saveFilePath);
        Tclass target = JsonUtility.FromJson<Tclass>(json);

        return target;
    }

    /// <summary>
    /// ������ ������ �ִ��� �˻� �� ����
    /// </summary>
    /// <returns>������ �����Ǿ� �־��ٸ� false, ������ �ִٸ� true</returns>
    public static bool CheckFile()
    {
        if (!Directory.Exists(saveFolderPath))
            Directory.CreateDirectory(saveFolderPath);

        if (!File.Exists(saveFilePath))
        {
            FileStream file = File.Create(saveFilePath);
            file.Close();

            return false;
        }

        return true;
    }
}

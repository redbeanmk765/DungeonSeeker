using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


// 코드 출처 블로그 : https://dyunace.tistory.com/29

public class SaveController : MonoBehaviour
{
    private static string saveFolderPath;
    private static string saveFilePath = saveFolderPath;

    /// <summary>
    /// 파일 경로 설정. 경로 따로 세이브 폴더 이름 따로 넣어서 사용.
    /// 고정 경로를 사용할 경우 이 함수를 사용하지 않고, 변수 값을 직접 입력하여 사용 가능
    /// </summary>
    /// <param name="_path">폴더 경로</param>
    /// <param name="_fileName">파일 이름. 형식자 붙이는거 잊지 말 것. => (###.json)</param>
    public static void SetFilePath(string _path, string _fileName)
    {
        saveFolderPath = _path;
        saveFilePath = saveFolderPath + "/" + _fileName;
    }

    /// <summary>
    /// 세이브 시스템. 
    /// </summary>
    /// <typeparam name="Tclass">Generic 타입 값. 입력하지 않아도 사용 가능</typeparam>
    /// <param name="data">저장할 데이터 타입</param>
    public static void Save<Tclass>(Tclass data)
    {
        CheckFile();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }

    /// <summary>
    /// 로드 시스템
    /// </summary>
    /// <typeparam name="Tclass">Generic 타입 값. 반환 받을 타입을 입력할 것</typeparam>
    /// <returns>Generic 타입을 가지는 결과물 출력</returns>
    public static Tclass Load<Tclass>()
    {
        CheckFile();

        string json = File.ReadAllText(saveFilePath);
        Tclass target = JsonUtility.FromJson<Tclass>(json);

        return target;
    }

    /// <summary>
    /// 누락된 파일이 있는지 검사 및 생성
    /// </summary>
    /// <returns>파일이 누락되어 있었다면 false, 파일이 있다면 true</returns>
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

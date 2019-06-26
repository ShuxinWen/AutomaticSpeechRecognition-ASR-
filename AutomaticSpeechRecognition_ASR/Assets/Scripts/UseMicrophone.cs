using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 语音识别音频可视化
/// </summary>
public class UseMicrophone : MonoBehaviour
{
    public GameObject[] obj;
    public static float volume;
    private AudioClip micRecord;
    string device;
    void Start()
    {

        device = Microphone.devices[0];//获取设备麦克风
        micRecord = Microphone.Start(device, true, 999, 44100);//44100音频采样率   固定格式
    }
    void Update()
    {
        volume = GetMaxVolume();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    /// <summary>
    /// 每一振处理那一帧接收的音频文件
    /// </summary>
    /// <returns></returns>
    float GetMaxVolume()
    {
        float maxVolume = 0f;
        //剪切音频
        float[] volumeData = new float[128];
        int offset = Microphone.GetPosition(device) - 128 + 1;
        if (offset < 0)
        {
            return 0;
        }
        micRecord.GetData(volumeData, offset);

        for (int i = 0; i < 128; i++)
        {
            float tempMax = volumeData[i];//修改音量的敏感值
            //这个if是用来取记录的音频的一部分   和你所加的物体有关
            if (i % 4 == 0)
            {
                int f = i / 4;
                obj[f].gameObject.transform.localScale = new Vector3(0.3f, volumeData[i] * 10 + 0.2f, 0.1f);//将可视化的物体和音波相关联
            }
        }
        return maxVolume;
    }
}
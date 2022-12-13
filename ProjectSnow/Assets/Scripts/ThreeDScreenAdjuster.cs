using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using static UnityEngine.GraphicsBuffer;

public class ThreeDScreenAdjuster : MonoBehaviour
{
    [Header("ScreenCorners")]
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject bottomLeft; 
    public GameObject bottomRight;

    //Todo: Implement non square aspect ratios
    [Header("Screen")]
    public GameObject pixel;
    private GameObject pixelList;
    public int screenSize = 128;
    public int pixelSize = 1; //Size of each pixel, change this accordingly!

    [Header("Video")]
    public VideoPlayer videoPlayer;

    private float cubeSizeX;
    private float cubeSizeY;


    void Start()
    {
        cubeSizeX = topLeft.transform.localScale.x;
        cubeSizeY = topLeft.transform.localScale.y;

        //We need uniform sizing
        if (cubeSizeX != cubeSizeY)
        {
            cubeSizeX = 1; //Default
            cubeSizeY = 1; //Default
        }

        topLeft.SetActive(false);
        topRight.SetActive(false);
        bottomLeft.SetActive(false);
        bottomRight.SetActive(false);

        //Video Player Stuff
        videoPlayer.Stop();
        videoPlayer.renderMode = VideoRenderMode.APIOnly; //APIOnly because we're not gonna play the video
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.sendFrameReadyEvents = true;
        videoPlayer.frameReady += FrameReady;
        videoPlayer.Prepare();
        

    }

    void Prepared(VideoPlayer videoPlayer) => videoPlayer.Pause();

    void FrameReady(VideoPlayer videoPlayer, long frameIndex)
    {
        var textureToCopy = videoPlayer.texture;

        //perform texture copy here;

        videoPlayer.frame = frameIndex + 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateScreen()
    {
        topLeft.transform.localPosition = new Vector3(0, 0, 0);
        topRight.transform.localPosition = new Vector3((screenSize- pixelSize), 0, 0);
        bottomLeft.transform.localPosition = new Vector3(0, -(screenSize - pixelSize), 0);
        bottomRight.transform.localPosition = new Vector3((screenSize - pixelSize), -(screenSize - pixelSize), 0);

        //Clear our pixel list
        if(pixelList != null)
        {
            DeleteScreen();
        }


        pixelList = new GameObject("PixelList");
        pixelList.transform.parent = transform;
        
        //Can take awhile, complexity O(n) n=screenSize^2
        for (int i = 0; i < screenSize; i++)
        {
            for(int j = 0; j < screenSize; j++)
            {
                GameObject newPixel = Instantiate(pixel, new Vector3(transform.position.x + i, transform.position.y - j, transform.position.z), Quaternion.identity, pixelList.transform);
                newPixel.gameObject.SetActive(false);
            }
        }
    }

    public void DeleteScreen()
    {
        if (Application.isPlaying)
        {
            if (pixelList != null)
            {
                Destroy(pixelList.transform.gameObject);
            }
        }

        else if (Application.isEditor)
        {
            if (pixelList != null)
            {
                DestroyImmediate(pixelList.transform.gameObject);
            }
        }
    }
}

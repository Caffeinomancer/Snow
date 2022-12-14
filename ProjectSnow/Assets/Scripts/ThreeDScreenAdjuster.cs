using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private GameObject[,] pixelListRefs; //2D array of pixels

    [Header("Video")]
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    private Texture2D currentVideoFrame;

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
        //audioSource = videoPlayer.GetTargetAudioSource();


        pixelListRefs = new GameObject[screenSize, screenSize];

        GenerateScreen();

        //StartCoroutine(UpdateScreen());

    }

    void Prepared(VideoPlayer videoPlayer) => videoPlayer.Pause();

    void FrameReady(VideoPlayer videoPlayer, long frameIndex)
    {
        Texture texToCopy = videoPlayer.texture;
        RenderTexture renTexTmp = RenderTexture.GetTemporary(screenSize, screenSize, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);

        Graphics.Blit(texToCopy, renTexTmp); // Blitting pixels from texture to render texture
        RenderTexture previous = RenderTexture.active; //Backup current RenderTexture
        RenderTexture.active = renTexTmp; //Set current RenderTexture to temporary one we created

        currentVideoFrame = new Texture2D(screenSize, screenSize);
        currentVideoFrame.ReadPixels(new Rect(0, 0, screenSize, screenSize), 0, 0);
        currentVideoFrame.Apply();

        RenderTexture.active = previous; //Reset active RenderTexture

        RenderTexture.ReleaseTemporary(renTexTmp); //Release the temporary RenderTexture


        Color pixCol;

        for (int i = 0; i < screenSize; i++)
        {
            for (int j = 0; j < screenSize; j++)
            {
                if (currentVideoFrame != null)
                {
                    pixCol = currentVideoFrame.GetPixel(i, j);

                    if (pixCol == Color.black)
                    {
                        pixelListRefs[i, j].SetActive(true);
                    }
                    else
                    {
                        pixelListRefs[i, j].SetActive(false);
                    }
                }

                else
                {
                    Debug.Log("i: " + i + " j: " + j);
                }

            }
        }

        //videoPlayer.frame = frameIndex;
        Debug.Log("Frame");
        //videoPlayer.frameReady -= FrameReady;
    }

    private IEnumerator UpdateScreen()
    {
        Color pixCol;
        if(videoPlayer.isPlaying)
        {
            for (int i = 0; i < screenSize; i++)
            {
                for (int j = 0; j < screenSize; j++)
                {
                    if (currentVideoFrame != null)
                    {
                        pixCol = currentVideoFrame.GetPixel(i, j);

                        if (pixCol == Color.black)
                        {
                            pixelListRefs[i, j].SetActive(true);
                        }
                        else
                        {
                            pixelListRefs[i, j].SetActive(false);
                        }
                    }

                    else
                    {
                        Debug.Log("i: " + i + " j: " + j);
                    }

                }

                yield return null;
            }
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
                audioSource.Play();
            }
        }
        //240x160
        //Debug.Log("Curr Frame " + currentVideoFrame.GetPixel(0,0));
        //Debug.Log("First Pix " + pixelListRefs[0,0]);
        //Debug.Log("Random Pix " + pixelListRefs[31, 31]);

        /*if(currentVideoFrame.GetPixel(0,0) == Color.black)
        {
            Debug.Log("Black");
        }
        
        else
        {
            Debug.Log("White");
        }*/
        //UpdatePixels();


    }

    private void FixedUpdate()
    {

    }

    public void UpdatePixels()
    {
        /*Color pixCol;

        for(int i = 0; i < screenSize; i++)
        {
            for(int j = 0; j < screenSize; j++)
            {
                if(currentVideoFrame != null)
                {
                    pixCol = currentVideoFrame.GetPixel(i, j);

                    if (pixCol == Color.black)
                    {
                        pixelListRefs[i, j].SetActive(false);
                    }
                    else
                    {
                        pixelListRefs[i, j].SetActive(true);
                    }
                }
               
                else
                {
                    Debug.Log("i: " + i + " j: " + j);
                }

            }
        }*/
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


        int iter = 0; //Var to help with assigning objects to the 2D array
        //Can take awhile, complexity O(n) n=screenSize^2 
        for (int i = screenSize; i > 0; i--)//Reverse order because 2D textures track pixels from bottom up
        {
           
            for(int j = 0; j < screenSize; j++)
            {
                pixelListRefs[iter, j] = Instantiate(pixel, new Vector3(transform.position.x + iter, (transform.position.y - screenSize) + j, transform.position.z), Quaternion.identity, pixelList.transform);
                pixelListRefs[iter, j].gameObject.SetActive(false);
            }

            iter++;
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

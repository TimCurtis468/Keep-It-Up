using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject Button;
    public GameObject Title;
    public GameObject VV;
    public GameObject background;

    private float widthFactor;
    private float heightFactor;

    public void Awake()
    {
        GameObject obj;
        Transform trans;
        Transform childTrans;

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        widthFactor = Screen.width / 1920.0f;
        heightFactor = Screen.height / 1080.0f;

        // Button
        Resize(Button);

        // background
        ResizeSpriteRendered(background);

        // Button text
        trans = Button.transform;
        childTrans = trans.Find("Text");
        obj = childTrans.gameObject;
        Resize(obj);
        ResizeText(obj);

        VV.transform.localScale *= heightFactor;
        VV.transform.localPosition = new Vector3(VV.transform.localPosition.x * widthFactor, VV.transform.localPosition.y, VV.transform.localPosition.z);
        Title.transform.localScale *= heightFactor;
        Title.transform.localPosition = new Vector3(Title.transform.localPosition.x * widthFactor, Title.transform.localPosition.y, Title.transform.localPosition.z);
        PositionText(Title);
        PositionText(VV);
    }

    public void ChangeMenuScene(string sceneName)
   {
       SceneManager.LoadScene(sceneName);
   }

    private void Resize(GameObject gameObject)
    {
        float objectWidth;
        float objectHeight;
        RectTransform rt;

        rt = gameObject.GetComponent<RectTransform>();
        objectWidth = rt.rect.width;
        objectHeight = rt.rect.height;

        objectWidth = objectWidth * widthFactor;
        objectHeight = objectHeight * heightFactor;

        rt.sizeDelta = new Vector2(objectWidth, objectHeight);
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x * widthFactor, rt.anchoredPosition.y * heightFactor);
    }

    private void ResizeText(GameObject gameObject)
    {
        Text t = gameObject.GetComponent<Text>();
        t.fontSize = (t.fontSize * Screen.width) / 1920;
    }

    private void ResizeSpriteRendered(GameObject gameObject)
    {
        float objectWidth;
        float objectHeight;
        SpriteRenderer sr;

        sr = gameObject.GetComponent<SpriteRenderer>();
        objectWidth = sr.transform.localScale.x;
        objectHeight = sr.transform.localScale.y;

        objectWidth = objectWidth * widthFactor;
        objectHeight = objectHeight * heightFactor;

        sr.transform.localScale = new Vector2(objectWidth, objectHeight);
    }

    private void PositionText(GameObject gameObject)
    {
        float objectPosx;
        float objectPosy;
        RectTransform rt;

        rt = gameObject.GetComponent<RectTransform>();
        objectPosx = rt.localPosition.x;
        objectPosy = rt.localPosition.y;

        objectPosx = objectPosx * widthFactor;
        objectPosy = objectPosy * heightFactor;

        rt.localPosition = new Vector3(objectPosx, objectPosy, 0);
    }
}
